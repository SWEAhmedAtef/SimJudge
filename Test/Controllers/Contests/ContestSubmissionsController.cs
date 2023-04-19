using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Test.Models;
using Test.ViewModels;

namespace Test.Controllers
{
	public class ContestSubmissionsController : Controller
	{
		private ApplicationDbContext _context;

		public ContestSubmissionsController()
		{
			_context = new ApplicationDbContext();
		}


		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		[Route("Contest/{id}/Submissions")]
		public ActionResult ViewSubmissions(int id)
		{
			var contest = _context.Contests.SingleOrDefault(c => c.Id == id);
			if (contest.StartDate > DateTime.Now)
			{
				return RedirectToAction("Index", "Contests");
			}
			var contestSubmissions = _context.ContestSubmissions
				.Where(s => s.ContestId == id)
				.Include(s => s.Contest)
				.Include(s => s.Problem)
				.Include(s => s.ApplicationUser)
				.Include(s => s.Language)
				.ToList();

			var contestSubmissionsViewModel = new ContestSubmissionsViewModel
			{
				ContestSubmissions = contestSubmissions,
				ContestName = contest.Name,
			};
			return View(contestSubmissionsViewModel);
		}

		[Route("Contest/{contestId}/Submission/{id}")]
		public ActionResult SubmissionDetails(int id, int contestId)
		{
			var submission = _context.ContestSubmissions
				.Include(s => s.Problem)
				.Include(s => s.ApplicationUser)
				.Include(s => s.Language)
				.SingleOrDefault(s => s.Id == id);

			if (submission == null)
				return HttpNotFound();

			return View(submission);
		}

		[Route("Contest/{contestId}/Submit/{Id}")]
		public ActionResult Submit(int id, int contestId)
		{
			var contest = _context.Contests.Single(c => c.Id == contestId);
			if (contest.StartDate > DateTime.Now)
			{
				return RedirectToAction("Index", "Contests");
			}
			var problem = _context.Problems
				.Include(p => p.SourceWebsite)
				.Include(p => p.CodeForcesDetails)
				.Include(p => p.SimJudgeDetails)
				.SingleOrDefault(p => p.Id == id);

			if (problem == null)
				return HttpNotFound();

			var userId = User.Identity.GetUserId();
			
			var languages = _context.Languages
				.Where(l => l.SourceWebsiteId == problem.SourceWebsiteId)
				.ToList();

			var submissionsViewModel = new ContestSubmissionViewModel()
			{
				Languages = languages,
				ApplicationUserId = userId,
				ProblemId = problem.Id,
				ProblemName = problem.Name,
				SourceProblemId = problem.SourceWebsite.Name + "-" + problem.SourceProblemId,
				ContestId = contestId
			};
			return View(submissionsViewModel);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<ActionResult> SaveSubmissionAsync(ContestSubmission contestSubmission)
		{
			var problem = _context.Problems
				.Include(p => p.SourceWebsite)
				.Include(p => p.CodeForcesDetails)
				.Include(p => p.SimJudgeDetails)
				.SingleOrDefault(p => p.Id == contestSubmission.ProblemId);

			if (!ModelState.IsValid)
			{
				var languages = _context.Languages
					.Where(l => l.SourceWebsiteId == problem.SourceWebsiteId)
					.ToList();
				
				var submissionsViewModel = new ContestSubmissionViewModel
				{
					Languages = languages,
					ApplicationUserId = contestSubmission.ApplicationUserId,
					ProblemId = problem.Id,
					ProblemName = problem.Name,
					SourceProblemId = problem.SourceWebsite.Name + "-" + problem.SourceProblemId,
					ContestId = contestSubmission.ContestId
				};
				return View("Submit", submissionsViewModel);
			}
			
			contestSubmission.Code = Server.HtmlEncode(contestSubmission.Code);

			var language = _context.Languages
				.Where(l => l.SourceWebsiteId == problem.SourceWebsiteId)
				.SingleOrDefault(l => l.Id == contestSubmission.LanguageId);

			contestSubmission = await ContestSubmission.ProcessSubmissionAsync(contestSubmission, problem, language);

			_context.ContestSubmissions.Add(contestSubmission);

			var WrongAnswer = _context.ContestUserStatistics
				.Where(C => C.ContestId == contestSubmission.ContestId)
				.Where(c => c.ApplicationUserId == contestSubmission.ApplicationUserId)
				.SingleOrDefault(c => c.ProblemId == contestSubmission.ProblemId);

			// Count solved problems
			if (contestSubmission.Result == "Accepted")
			{
				var Solved = _context.ContestSubmissions
					.Where(c => c.ContestId == contestSubmission.ContestId)
					.Where(c => c.ApplicationUserId == contestSubmission.ApplicationUserId)
					.Where(c => c.ProblemId == contestSubmission.ProblemId)
					.FirstOrDefault(c => c.Result == "Accepted");
				var contestUser = _context.ContestUsers
					.Where(c => c.ContestId == contestSubmission.ContestId)
					.SingleOrDefault(s => s.ApplicationUserId == contestSubmission.ApplicationUserId);
				var contest = _context.Contests.SingleOrDefault(c => c.Id == contestSubmission.ContestId);


				if (Solved == null)
				{
					contestUser.SolvedProblems += 1;
					TimeSpan span = DateTime.Now.Subtract(contest.StartDate);
					contestUser.Penalty += (WrongAnswer.WrongAnswers * 20) + Convert.ToInt32(span.TotalMinutes);
				}
			}
			if (contestSubmission.Result != "Accepted" && contestSubmission.Result != "Compilation error" && contestSubmission.Result != "Duplicate Code on Source")
			{
				WrongAnswer.WrongAnswers += 1;
			}

			//Standing record
			var contestUserStatistics = _context.ContestUserStatistics
				.Where(c => c.ContestId == contestSubmission.ContestId)
				.Where(c => c.ApplicationUserId == contestSubmission.ApplicationUserId)
				.FirstOrDefault(c => c.ProblemId == contestSubmission.ProblemId);

			if (contestUserStatistics.ProblemStatus != "Accepted")
			{
				contestUserStatistics.ProblemStatus = contestSubmission.Result;
			}

			_context.SaveChanges();
			return RedirectToAction("ViewSubmissions", new { id = contestSubmission.ContestId });
		}
	}
}
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Test.Models;
using Test.ViewModels;

namespace Test.Controllers
{
	public class SubmissionsController : Controller
	{
		private ApplicationDbContext _context;

		public SubmissionsController()
		{
			_context = new ApplicationDbContext();
		}


		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		[AllowAnonymous]
		public ActionResult Index()
		{
			var submissions = _context.Submissions
				.Include(s => s.Problem)
				.Include(s => s.ApplicationUser)
				.Include(s => s.Language)
				.ToList();

			return View(submissions);
		}

		public ActionResult UserSubmissions()
		{
			var userId = User.Identity.GetUserId();
			var submissions = _context.Submissions
				.Where(s => s.ApplicationUserId == userId)
				.Include(s => s.Problem)
				.Include(s => s.ApplicationUser)
				.Include(s => s.Language)
				.ToList();

			return View("Index", submissions);
		}

		public ActionResult Details(int id)
		{
			var submission = _context.Submissions
				.Include(s => s.Problem)
				.Include(s => s.ApplicationUser)
				.Include(s => s.Language)
				.SingleOrDefault(s => s.Id == id);

			if (submission == null)
				return HttpNotFound();

			return View(submission);
		}

		[Route("Submissions/Submit/{Id}")]
		public ActionResult Submit(int id)
		{
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

			var submissionsViewModel = new SubmissionsViewModel
			{
				Languages = languages,
				ApplicationUserId = userId,
				ProblemId = problem.Id,
				ProblemName = problem.Name,
				SourceProblemId = problem.SourceWebsite.Name + "-" + problem.SourceProblemId
			};
			return View(submissionsViewModel);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<ActionResult> SaveProblemSubmissionAsync(Submission submission)
		{
			var problem = _context.Problems
				.Include(p => p.SourceWebsite)
				.Include(p => p.CodeForcesDetails)
				.Include(p => p.SimJudgeDetails)
				.SingleOrDefault(p => p.Id == submission.ProblemId);

			if (!ModelState.IsValid)
			{
				var languages = _context.Languages
					.Where(l => l.SourceWebsiteId == problem.SourceWebsiteId)
					.ToList();

				var submissionsViewModel = new SubmissionsViewModel
				{
					Languages = languages,
					ApplicationUserId = submission.ApplicationUserId,
					ProblemId = problem.Id,
					ProblemName = problem.Name,
					SourceProblemId = problem.SourceWebsite.Name + "-" + problem.SourceProblemId
				};
				return View(submissionsViewModel);
			}

			submission.Code = Server.HtmlEncode(submission.Code);
			
			var language = _context.Languages
				.Where(l => l.SourceWebsiteId == problem.SourceWebsiteId)
				.SingleOrDefault(l => l.Id == submission.LanguageId);

			submission = await Submission.ProcessSubmissionAsync(submission, problem, language);
			_context.Submissions.Add(submission);

			_context.SaveChanges();
			return RedirectToAction("UserSubmissions");
		}
	}
}
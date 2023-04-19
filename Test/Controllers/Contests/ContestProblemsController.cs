using System;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Test.Models;
using Test.ViewModels;

namespace Test.Controllers
{
	public class ContestProblemsController : Controller
	{
		private ApplicationDbContext _context;
		public ContestProblemsController()
		{
			_context = new ApplicationDbContext();
		}


		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		[Route("Contests/{id:int}")]
		public ActionResult ContestProblems(int id)
		{
            var contest = _context.Contests.Single(c => c.Id == id);
            if (contest.StartDate > DateTime.Now)
            {
                return RedirectToAction("Index", "Contests");
            }
			var userId = User.Identity.GetUserId();
			var userInDb = _context.ContestUsers
				.Where(c => c.ContestId == id)
				.SingleOrDefault(c => c.ApplicationUserId == userId);
			
			bool isEnrolled = userInDb != null;
			
			var contestProblems = _context.ContestProblems
				.Where(c => c.ContestId == id)
				.Include(c => c.Contest)
				.Include(c => c.Problem)
				.ToList();

			
			var problemViewModel = new ContestProblemsViewModel()
			{
				ContestProblems = contestProblems,
				IsEnrolled = isEnrolled,
				ContestId = id,
				Contest = contest
			};
			return View("Index", problemViewModel);
		}

		[Authorize(Roles = "IsAdmin")]
		[Route("Contests/{id:int}/AddProblem")]
		public ActionResult AddProblem(int id)
		{
			var contest = _context.Contests.SingleOrDefault(c => c.Id == id);
			return View(contest);
		}

		[Route("Contests/{ContestId:int}/{ProblemId:int}")]
		public ActionResult ViewProblem(int ContestId, int ProblemId)
		{
            var contest = _context.Contests.Single(c => c.Id == ContestId);
            if (contest.StartDate > DateTime.Now)
            {
                return RedirectToAction("Index", "Contests");
            }

            var problem = _context.Problems
				.Include(p => p.SourceWebsite)
				.Include(p => p.ProblemDetails)
				.Include(p => p.SimJudgeDetails)
				.Include(p => p.CodeForcesDetails)
				.SingleOrDefault(c => c.Id == ProblemId);

			var problemView = new ProblemViewModel()
			{
				Problem = problem,
				ContestId = ContestId
			};
			
            var viewName = problem.SourceWebsite.Name + "Problem";
			return View(viewName, problemView);
		}
	}
}
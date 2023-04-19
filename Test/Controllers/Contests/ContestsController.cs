using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Test.Models;
using Test.ViewModels;

namespace Test.Controllers
{
	public class ContestsController : Controller
	{
		private ApplicationDbContext _context;
		public ContestsController()
		{
			_context = new ApplicationDbContext();
		}


		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Contests
		[AllowAnonymous]
		public ActionResult Index()
		{
			var contest = _context.Contests.ToList();

			return View("Index", contest);
		}

		[Authorize(Roles = RoleName.Admin)]
		[Route("Contests/Create")]
		public ActionResult Create()
		{
			var contest = new Contest();

			return View("ContestCreation", contest);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = RoleName.Admin)]
		[Route("Contests/Save")]
		public ActionResult Save(Contest contest)
		{
			if (!ModelState.IsValid)
			{
				return View("ContestCreation", contest);
			}

			contest.CreatedAt = DateTime.Now;
			_context.Contests.Add(contest);
			_context.SaveChanges();

			return RedirectToAction("AddProblem", "ContestProblems", contest);
		}

		[Route("Contests/Enroll/{id:int}")]
		public ActionResult Enroll(int id)
		{
			var userId = User.Identity.GetUserId();
			var userExists = _context.ContestUsers
				.Where(c => c.ContestId == id)
				.SingleOrDefault(c => c.ApplicationUserId == userId);

			if (!ModelState.IsValid || userExists != null)
			{
				return RedirectToAction("Index", "Contests");
			}

			var contestUserDb = new ContestUser()
			{
				ApplicationUserId = userId,
				ContestId = id
			};
			_context.ContestUsers.Add(contestUserDb);


			// create records between each user and problem in the contest
			var contestProblems = _context.ContestProblems
				.Where(c => c.ContestId == id)
				.Include(c => c.Problem)
				.ToList();

			foreach (var problem in contestProblems)
			{
				var stat = new ContestUserStatistics()
				{
					ApplicationUserId = userId,
					ContestId = id,
					ProblemId = problem.Problem.Id
				};
				_context.ContestUserStatistics.Add(stat);
			}

			_context.SaveChanges();
			return RedirectToAction("ShowStatistics", "Contests");
		}

		[Route("Contests/ShowStatistics/{id}")]
		public ActionResult ShowStatistics(int id)
		{
            var contest = _context.Contests.SingleOrDefault(c => c.Id == id);
            if (contest.StartDate > DateTime.Now)
            {
                return RedirectToAction("Index", "Contests");
            }
			var problemsInContest = _context.ContestProblems
				.Where(c => c.ContestId == id)
				.Include(c => c.Problem)
				.ToList();

			var contestUserStatistics = _context.ContestUserStatistics
				.Where(c => c.ContestId == id)
				.Include(c => c.Problem)
				.ToList();

			var usersInContest = _context.ContestUsers
				.Where(c => c.ContestId == id)
				.Include(c => c.ApplicationUser)
				.ToList();

			var contestUserStatisticViewModel = new ContestUserStatisticsViewModel()
			{
				ContestUserStatistics = contestUserStatistics,
				ContestUsers = usersInContest,
				ContestProblems = problemsInContest
			};
			return View(contestUserStatisticViewModel);
		}
	}
}
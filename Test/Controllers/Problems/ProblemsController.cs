using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
	public class ProblemsController : Controller
	{
		private ApplicationDbContext _context;

		public ProblemsController()
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
			return View();
		}

		[AllowAnonymous]
		[Route("Problems/{Id}")]
		public ActionResult ViewProblem(int Id)
		{
			var problem = _context.Problems
				.Include(p => p.SourceWebsite)
				.Include(p => p.ProblemDetails)
				.Include(p => p.SimJudgeDetails)
				.Include(p => p.CodeForcesDetails)
				.SingleOrDefault(p => p.Id == Id);

			if (problem == null)
				return HttpNotFound();

			var viewName = problem.SourceWebsite.Name + "Problem";
			return View(viewName, problem);
		}

		[Authorize(Roles = RoleName.Admin)]
		[Route("Problems/CreateLocalProblem")]
		public ActionResult CreateLocalProblem()
		{
			var problem = new Problem();
			return View("SimJudgeProblemForm", problem);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("Problems/SaveLocalProblem")]
		public ActionResult SaveLocalProblem(Problem Problem)
		{
			if (!ModelState.IsValid)
			{
				return View("SimJudgeProblemForm", Problem);
			}
			
			Problem.SimJudgeDetails.TestCaseInput = Server.HtmlEncode(Problem.SimJudgeDetails.TestCaseInput);
			Problem.SimJudgeDetails.TestCaseOutput = Server.HtmlEncode(Problem.SimJudgeDetails.TestCaseOutput);

			Problem.SourceWebsiteId = Problem.LocalProblemId;

			_context.Problems.Add(Problem);
			_context.SaveChanges();

			Problem.SourceProblemId = Problem.Id.ToString();
			_context.SaveChanges();

			return RedirectToAction("ViewProblem", new { Id = Problem.Id });
		}
	}
}
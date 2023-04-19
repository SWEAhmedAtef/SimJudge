using System.Threading.Tasks;
using System.Web.Mvc;
using Test.Models;
using Test.ViewModels;

namespace Test.Controllers
{
	public class CompilersController : Controller
	{
		private ApplicationDbContext _context;

		public CompilersController()
		{
			_context = new ApplicationDbContext();
		}


		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		public ActionResult Index()
		{
			var compilerViewModel = new CompilerViewModel();
			return View(compilerViewModel);
		}
		/*
		 * Under Progress
		[HttpPost]
		[Route("Compilers/Compile")]
		public async Task<ActionResult> CompileAsync(string code, string input)
		{
			string url = "http://127.0.0.1:5000/combo-box";
			var result = await Api.CallAsync(url, new
			{
				code = Server.HtmlEncode(code),
				input = Server.HtmlEncode(input),
			});

			var compilerViewModel = new CompilerViewModel()
			{
				Code = code,
				Result = result
			};
			return View("Compile", compilerViewModel);
		}
		*/
	}
}
using System.Web.Mvc;

namespace Test.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
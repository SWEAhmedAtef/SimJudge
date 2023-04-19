using System.Web.Mvc;

namespace Test.Controllers
{
	[Authorize(Roles = "IsAdmin")]
	public class AdminController : Controller
	{
		// GET: Admin
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult advanced()
		{
			return View();
		}
		public ActionResult blank()
		{
			return View();
		}
		public ActionResult boxed()
		{
			return View();
		}
		public ActionResult calendar()
		{
			return View();
		}
		public ActionResult buttons()
		{
			return View();
		}
		public ActionResult chartjs()
		{
			return View();
		}
		public ActionResult collapsed_sidebar()
		{
			return View();
		}
		public ActionResult compose()
		{
			return View();
		}
		public ActionResult data()
		{
			return View();
		}
		public ActionResult editors()
		{
			return View();
		}
		public ActionResult dixed()
		{
			return View();
		}
		public ActionResult flot()
		{
			return View();
		}
		public ActionResult general()
		{
			return View();
		}
		public ActionResult icons()
		{
			return View();
		}
		public ActionResult forms_general()
		{
			return View();
		}
		public ActionResult inline()
		{
			return View();
		}
		public ActionResult invoice()
		{
			return View();
		}
		public ActionResult invoice_print()
		{
			return View();
		}
		public ActionResult lockscreen()
		{
			return View();
		}
		public ActionResult login()
		{
			return View();
		}
		public ActionResult mailbox()
		{
			return View();
		}
		public ActionResult modals()
		{
			return View();
		}
		public ActionResult morris()
		{
			return View();
		}
		public ActionResult pace()
		{
			return View();
		}
		public ActionResult profile()
		{
			return View();
		}
		public ActionResult read_mail()
		{
			return View();
		}
		public ActionResult register()
		{
			return View();
		}
		public ActionResult simple()
		{
			return View();
		}
		public ActionResult sliders()
		{
			return View();
		}
		public ActionResult timeline()
		{
			return View();
		}
		public ActionResult top_nav()
		{
			return View();
		}
		public ActionResult widgets()
		{
			return View();
		}

	}
}
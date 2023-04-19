using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
	public class LayoutController : Controller
	{
		private ApplicationDbContext _context;

		public LayoutController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		[AllowAnonymous]
		public bool GetDarkMode()
		{

			var userId = User.Identity.GetUserId();
			if (userId == null)
				return false;

			var userInDb = _context.Users.FirstOrDefault(u => u.Id == userId);

			var darkmode = userInDb.DarkMode;
			return darkmode;
		}

		public ActionResult ChangeDarkMode(string url)
		{
			var userId = User.Identity.GetUserId();
			var user = _context.Users.Single(c => c.Id == userId);

			user.DarkMode = !user.DarkMode;
			_context.SaveChanges();

			return Redirect(url);
		}
	}
}
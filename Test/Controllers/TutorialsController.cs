using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Test.Models;
using Test.ViewModels;

namespace Test.Controllers
{
	public class TutorialsController : Controller
	{
		private ApplicationDbContext _context;

		public TutorialsController()
		{
			_context = new ApplicationDbContext();
		}


		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		[AllowAnonymous]
		public ViewResult Index()
		{
			var tutorials = _context.Tutorials
				.Include(t => t.ApplicationUser)
				.ToList();

			return View(tutorials);
		}

		[AllowAnonymous]
		public ActionResult Details(int Id)
		{
			var tutorial = _context.Tutorials
				.Include(t => t.ApplicationUser)
				.Include(t => t.TutorialDetails)
				.Include(t => t.TutorialDetails.RelatedProblem)
				.SingleOrDefault(t => t.Id == Id);

			if (tutorial == null)
				return HttpNotFound();

			var tutorialComments = _context.TutorialComments
				.Where(c => c.TutorialId == Id)
				.Include(c => c.ApplicationUser)
				.ToList();

			var userId = User.Identity.GetUserId();
			var tutorialComment = new TutorialComment()
			{
				ApplicationUserId = userId,
				TutorialId = tutorial.Id
			};

			var tutorialViewModel = new TutorialViewModel()
			{
				TutorialComments = tutorialComments,
				Tutorial = tutorial,
				TutorialComment = tutorialComment
			};

			return View(tutorialViewModel);
		}

		[Authorize(Roles = RoleName.Admin)]
		public ActionResult Create()
		{
			var userId = User.Identity.GetUserId();

			var tutorial = new Tutorial()
			{
				ApplicationUserId = userId
			};
			return View("TutorialForm", tutorial);
		}

		[Authorize(Roles = RoleName.Admin)]
		public ActionResult AddRelatedProblem(int Id)
		{
			var tutorial = _context.Tutorials.SingleOrDefault(t => t.Id == Id);

			return View(tutorial);
		}

		[Authorize(Roles = RoleName.Admin)]
		public ActionResult Edit(int Id)
		{
			var tutorial = _context.Tutorials
				.Include(t => t.TutorialDetails)
				.SingleOrDefault(t => t.Id == Id);

			if (tutorial == null)
				return HttpNotFound();

			return View("TutorialForm", tutorial);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = RoleName.Admin)]
		public ActionResult Save(Tutorial tutorial)
		{
			if (!ModelState.IsValid)
			{
				return View("TutorialForm", tutorial);
			}

			if (tutorial.Id == 0)
			{
				tutorial.CreatedAt = DateTime.Now;

				if (!string.IsNullOrEmpty(tutorial.TutorialDetails.VideoUrl))
					tutorial.TutorialDetails.VideoUrl = GetYoutubeEmbedUrl(tutorial.TutorialDetails.VideoUrl);

				_context.Tutorials.Add(tutorial);
			}
			else
			{
				var TutorialInDb = _context.Tutorials
					.Include(t => t.TutorialDetails)
					.Single(t => t.Id == tutorial.Id);

				TutorialInDb.Title = tutorial.Title;
				TutorialInDb.TutorialDetails.Description = tutorial.TutorialDetails.Description;
				TutorialInDb.TutorialDetails.Tags = tutorial.TutorialDetails.Tags;
				TutorialInDb.TutorialDetails.RelatedProblemId = tutorial.TutorialDetails.RelatedProblemId;
				TutorialInDb.TutorialDetails.VideoUrl = tutorial.TutorialDetails.VideoUrl;
			}
			_context.SaveChanges();

			return RedirectToAction("AddRelatedProblem", "Tutorials", new { Id = tutorial.Id });
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult AddComment(TutorialComment tutorialComment)
		{
			if (!ModelState.IsValid)
			{
				var tutorial = _context.Tutorials
				.Include(t => t.ApplicationUser)
				.Include(t => t.TutorialDetails)
				.Include(t => t.TutorialDetails.RelatedProblem)
				.SingleOrDefault(t => t.Id == tutorialComment.TutorialId);

				var tutorialComments = _context.TutorialComments
					.Where(c => c.TutorialId == tutorialComment.TutorialId)
					.Include(c => c.ApplicationUser)
					.ToList();

				var tutorialViewModel = new TutorialViewModel()
				{
					TutorialComments = tutorialComments,
					Tutorial = tutorial,
					TutorialComment = tutorialComment
				};
				return View("Details", tutorialViewModel);
			}

			tutorialComment.CreatedAt = DateTime.Now;
			_context.TutorialComments.Add(tutorialComment);
			_context.SaveChanges();

			return RedirectToAction("Details", "Tutorials", new { Id = tutorialComment.TutorialId });
		}

		private string GetYoutubeEmbedUrl(string videoUrl)
		{
			var splitUrl = videoUrl.Split(new[] { "?v=" }, StringSplitOptions.None);

			if (splitUrl.Count() < 2)
				splitUrl = videoUrl.Split(new[] { ".be/" }, StringSplitOptions.None);

			if (splitUrl.Count() < 2)
				return videoUrl;

			var embedLink = "https://www.youtube.com/embed/" + splitUrl[1];

			return embedLink;
		}
	}
}
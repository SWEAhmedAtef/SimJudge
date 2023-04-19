using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Test.Models;
using Test.ViewModels;

namespace Test.Controllers
{
	public class BlogsController : Controller
	{
		private ApplicationDbContext _context;

		public BlogsController()
		{
			_context = new ApplicationDbContext();
		}


		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		public ViewResult Index()
		{
			var blogs = _context.Blogs
				.Include(b => b.ApplicationUser)
				.Include(c => c.BlogDetails)
				.ToList();

			return View(blogs);
		}

		public ActionResult New()
		{
			var userId = User.Identity.GetUserId();
			var blog = new Blog()
			{
				ApplicationUserId = userId
			};
			return View("BlogForm", blog);
		}

		public ActionResult Details(int Id)
		{
			var blog = _context.Blogs
				.Include(b => b.ApplicationUser)
				.Include(c => c.BlogDetails)
				.SingleOrDefault(b => b.Id == Id);

			if (blog == null)
				return HttpNotFound();

			var blogComments = _context.BlogComments
				.Where(c => c.BlogId == Id)
				.Include(c => c.ApplicationUser)
				.ToList();

			var userId = User.Identity.GetUserId();
			var blogComment = new BlogComment()
			{
				ApplicationUserId = userId,
				BlogId = blog.Id
			};

			var blogViewModel = new BlogViewModel()
			{
				BlogComments = blogComments,
				Blog = blog,
				BlogComment = blogComment
			};
			return View(blogViewModel);
		}

		public ActionResult Edit(int Id)
		{
			var blog = _context.Blogs
				.Include(c => c.BlogDetails)
				.Include(c => c.ApplicationUser)
				.SingleOrDefault(c => c.Id == Id);

			if (blog == null)
				return HttpNotFound();

			var userId = User.Identity.GetUserId();
			if (blog.ApplicationUserId != userId)
				return HttpNotFound();

			return View("BlogForm", blog);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Save(Blog blog)
		{
			if (blog.Id == 0)
			{
				blog.CreatedAt = DateTime.Now;
				_context.Blogs.Add(blog);
			}
			else
			{
				var BlogInDb = _context.Blogs
					.Include(c => c.BlogDetails)
					.Single(c => c.Id == blog.Id);

				BlogInDb.Title = blog.Title;
				BlogInDb.BlogDetails.Description = blog.BlogDetails.Description;
				BlogInDb.BlogDetails.Image = blog.BlogDetails.Image;
				BlogInDb.BlogDetails.Link = blog.BlogDetails.Link;
			}
			_context.SaveChanges();
			return RedirectToAction("Index", "Blogs");
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult AddComment(BlogComment blogComment)
		{
			if (!ModelState.IsValid)
			{
				var blog = _context.Blogs
				.Include(b => b.ApplicationUser)
				.Include(c => c.BlogDetails)
				.SingleOrDefault(b => b.Id == blogComment.BlogId);

				var blogComments = _context.BlogComments
					.Where(c => c.BlogId == blogComment.BlogId)
					.Include(c => c.ApplicationUser)
					.ToList();

				var blogViewModel = new BlogViewModel()
				{
					BlogComments = blogComments,
					Blog = blog,
					BlogComment = blogComment
				};
				return View("Details", blogViewModel);
			}

			blogComment.CreatedAt = DateTime.Now;
			_context.BlogComments.Add(blogComment);
			_context.SaveChanges();

			return RedirectToAction("Details", "Blogs", new { Id = blogComment.BlogId });
		}
	}
}
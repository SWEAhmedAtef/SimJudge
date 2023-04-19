using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
	public class Blog
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public DateTime CreatedAt { get; set; }

		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		public BlogDetails BlogDetails { get; set; }

		public int BlogDetailsId { get; set; }
	}
}
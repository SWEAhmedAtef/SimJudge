using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
	public class BlogComment
	{
		[Key]
		public int Id { get; set; }

		public Blog Blog { get; set; }

		[Required]
		public int BlogId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }

		public string ApplicationUserId { get; set; }

		[Display(Name = "Add Comment")]
		[Required]
		public string Comment { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
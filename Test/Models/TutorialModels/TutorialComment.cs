using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
	public class TutorialComment
	{
		[Key]
		public int Id { get; set; }

		public Tutorial Tutorial { get; set; }

		[Required]
		public int TutorialId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }

		public string ApplicationUserId { get; set; }

		[Display(Name = "Add Comment")]
		[Required]
		public string Comment { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
	public class Tutorial
	{
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		public DateTime CreatedAt { get; set; }

		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		public TutorialDetails TutorialDetails { get; set; }

		public int TutorialDetailsId { get; set; }
	}
}
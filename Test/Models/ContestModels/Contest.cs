using System;
using System.ComponentModel.DataAnnotations;


namespace Test.Models
{

	public class Contest
	{
		public Contest()
		{
			StartDate = DateTime.Now;
			EndDate = DateTime.Now;
			CreatedAt = DateTime.Now;
		}

		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public DateTime CreatedAt { get; set; }

		[ValidDate]
		public DateTime StartDate { get; set; }
		
		[ValidDate]
		public DateTime EndDate { get; set; }

		public ApplicationUser ApplicationUser { get; set; }

		[Display(Name = "Created By")]
		public string ApplicationUserId { get; set; }
	}

}
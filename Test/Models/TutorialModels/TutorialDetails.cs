using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
	public class TutorialDetails
	{
		public int Id { get; set; }

		[Required]
		public string Description { get; set; }

		[Url]
		public string VideoUrl { get; set; }

		public string Tags { get; set; }

		public Problem RelatedProblem { get; set; }

		public int? RelatedProblemId { get; set; }
	}
}
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
	public class ProblemDetails
	{
		public int Id { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public string Input { get; set; }

		[Required]
		public string Output { get; set; }

		public string Notes { get; set; }

		public string TimeLimit { get; set; }

		public string MemoryLimit { get; set; }
	}
}
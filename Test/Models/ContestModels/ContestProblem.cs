using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
	public class ContestProblem
	{
		public int Id { get; set; }

		public Contest Contest { get; set; }
		
		public int ContestId { get; set; }

		public Problem Problem { get; set; }

		[Required]
		[MaxLength(1)]
		public string ProblemIndex { get; set; }

		public string ProblemNickName { get; set; }
	}
}
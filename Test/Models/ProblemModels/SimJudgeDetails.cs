using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
	public class SimJudgeDetails
	{
		public int Id { get; set; }

		public string TestCaseInput { get; set; }

		public string TestCaseOutput { get; set; }
	}
}
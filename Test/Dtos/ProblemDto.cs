using System.ComponentModel.DataAnnotations;
using Test.Models;

namespace Test.Dtos
{
	public class ProblemDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string SourceProblemId { get; set; }

		public SourceWebsite SourceWebsite { get; set; }

		public int SourceWebsiteId { get; set; }

		public ProblemDetails ProblemDetails { get; set; }

		public int ProblemDetailsId { get; set; }

		public CodeForcesDetails CodeForcesDetails { get; set; }

		public int? CodeForcesDetailsId { get; set; }

		public SimJudgeDetails SimJudgeDetails { get; set; }

		public int? SimJudgeDetailsId { get; set; }
	}
}
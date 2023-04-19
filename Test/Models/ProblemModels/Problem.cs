using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
	public class Problem
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

		public const string LocalProblem = "SimJudge";

		public const int LocalProblemId = 1;

		public static string GetContestIdFromProblemId(string problem_id)
		{
			int contest_id_length = 0;
			for (int i = 0; i < problem_id.Length; ++i)
			{
				if (char.IsLetter(problem_id[i]))
				{
					contest_id_length = i;
					break;
				}
			}
			return problem_id.Substring(0, contest_id_length);
		}
	}
}
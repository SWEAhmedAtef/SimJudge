using System.ComponentModel.DataAnnotations;

namespace SimJudge.Domain.Entities
{
    public class Problem : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string SourceProblemId { get; set; } = string.Empty;

        [Required]
        public int SourceWebsiteId { get; set; }
        public SourceWebsite SourceWebsite { get; set; } = null!;

        [Required]
        public int ProblemDetailsId { get; set; }
        public ProblemDetails ProblemDetails { get; set; } = null!;

        public int? CodeForcesDetailsId { get; set; }
        public CodeForcesDetails? CodeForcesDetails { get; set; }

        public int? SimJudgeDetailsId { get; set; }
        public SimJudgeDetails? SimJudgeDetails { get; set; }

        public bool IsActive { get; set; } = true;

        public const string LocalProblem = "SimJudge";
        public const int LocalProblemId = 1;

        public static string GetContestIdFromProblemId(string problemId)
        {
            int contestIdLength = 0;
            for (int i = 0; i < problemId.Length; ++i)
            {
                if (char.IsLetter(problemId[i]))
                {
                    contestIdLength = i;
                    break;
                }
            }
            return problemId.Substring(0, contestIdLength);
        }
    }
}

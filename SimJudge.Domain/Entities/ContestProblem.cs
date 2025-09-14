using System.ComponentModel.DataAnnotations;

namespace SimJudge.Domain.Entities
{
    public class ContestProblem : BaseEntity
    {
        [Required]
        public int ContestId { get; set; }
        public Contest Contest { get; set; } = null!;

        [Required]
        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Index { get; set; } = string.Empty; // A, B, C, etc.

        public int Points { get; set; } = 100;

        public int Order { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

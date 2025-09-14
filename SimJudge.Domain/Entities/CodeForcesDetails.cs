using System.ComponentModel.DataAnnotations;

namespace SimJudge.Domain.Entities
{
    public class CodeForcesDetails : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string ContestId { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string Index { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? ProblemUrl { get; set; }

        public int? Rating { get; set; }

        [MaxLength(1000)]
        public string? Tags { get; set; }

        public bool IsSolved { get; set; } = false;
    }
}

using System.ComponentModel.DataAnnotations;

namespace SimJudge.Domain.Entities
{
    public class SimJudgeDetails : BaseEntity
    {
        [Required]
        [MaxLength(10000)]
        public string TestCaseInput { get; set; } = string.Empty;

        [Required]
        [MaxLength(10000)]
        public string TestCaseOutput { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? AdditionalTestCases { get; set; }

        public int TimeLimit { get; set; } = 1000; // in milliseconds

        public int MemoryLimit { get; set; } = 256; // in MB

        [MaxLength(50)]
        public string? Difficulty { get; set; }

        [MaxLength(1000)]
        public string? Tags { get; set; }
    }
}

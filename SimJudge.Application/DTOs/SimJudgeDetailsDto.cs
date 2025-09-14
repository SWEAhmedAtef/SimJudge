using System.ComponentModel.DataAnnotations;

namespace SimJudge.Application.DTOs
{
    public class SimJudgeDetailsDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(10000)]
        public string TestCaseInput { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(10000)]
        public string TestCaseOutput { get; set; } = string.Empty;
        
        [MaxLength(1000)]
        public string? AdditionalTestCases { get; set; }
        
        public int TimeLimit { get; set; }
        public int MemoryLimit { get; set; }
        
        [MaxLength(50)]
        public string? Difficulty { get; set; }
        
        [MaxLength(1000)]
        public string? Tags { get; set; }
    }
}

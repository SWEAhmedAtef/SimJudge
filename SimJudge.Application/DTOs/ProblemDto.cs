using System.ComponentModel.DataAnnotations;

namespace SimJudge.Application.DTOs
{
    public class ProblemDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string SourceProblemId { get; set; } = string.Empty;
        
        public int SourceWebsiteId { get; set; }
        public string SourceWebsiteName { get; set; } = string.Empty;
        
        public int ProblemDetailsId { get; set; }
        public ProblemDetailsDto? ProblemDetails { get; set; }
        
        public int? CodeForcesDetailsId { get; set; }
        public CodeForcesDetailsDto? CodeForcesDetails { get; set; }
        
        public int? SimJudgeDetailsId { get; set; }
        public SimJudgeDetailsDto? SimJudgeDetails { get; set; }
        
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

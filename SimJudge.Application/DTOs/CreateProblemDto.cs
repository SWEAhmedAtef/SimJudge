using System.ComponentModel.DataAnnotations;

namespace SimJudge.Application.DTOs
{
    public class CreateProblemDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string SourceProblemId { get; set; } = string.Empty;
        
        [Required]
        public int SourceWebsiteId { get; set; }
        
        [Required]
        public int ProblemDetailsId { get; set; }
        
        public int? CodeForcesDetailsId { get; set; }
        public int? SimJudgeDetailsId { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}

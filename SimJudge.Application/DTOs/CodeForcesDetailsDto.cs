using System.ComponentModel.DataAnnotations;

namespace SimJudge.Application.DTOs
{
    public class CodeForcesDetailsDto
    {
        public int Id { get; set; }
        
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
        
        public bool IsSolved { get; set; }
    }
}

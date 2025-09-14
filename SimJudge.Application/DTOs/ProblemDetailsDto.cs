using System.ComponentModel.DataAnnotations;

namespace SimJudge.Application.DTOs
{
    public class ProblemDetailsDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(1000)]
        public string Title { get; set; } = string.Empty;
        
        [MaxLength(10000)]
        public string? Description { get; set; }
        
        [MaxLength(10000)]
        public string? InputSpecification { get; set; }
        
        [MaxLength(10000)]
        public string? OutputSpecification { get; set; }
        
        [MaxLength(1000)]
        public string? SampleInput { get; set; }
        
        [MaxLength(1000)]
        public string? SampleOutput { get; set; }
        
        [MaxLength(1000)]
        public string? Note { get; set; }
        
        public int? TimeLimit { get; set; }
        public int? MemoryLimit { get; set; }
        
        [MaxLength(50)]
        public string? Difficulty { get; set; }
        
        [MaxLength(1000)]
        public string? Tags { get; set; }
    }
}

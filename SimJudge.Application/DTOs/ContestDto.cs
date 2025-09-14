using System.ComponentModel.DataAnnotations;

namespace SimJudge.Application.DTOs
{
    public class ContestDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(1000)]
        public string? Description { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        public int CreatorId { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }
        
        [MaxLength(50)]
        public string? Password { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}

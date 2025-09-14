using System.ComponentModel.DataAnnotations;

namespace SimJudge.Application.DTOs
{
    public class CreateContestDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(1000)]
        public string? Description { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [Required]
        public int CreatorId { get; set; }
        
        public bool IsActive { get; set; } = true;
        public bool IsPublic { get; set; } = true;
        
        [MaxLength(50)]
        public string? Password { get; set; }
    }
}

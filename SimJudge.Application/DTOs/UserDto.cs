using System.ComponentModel.DataAnnotations;

namespace SimJudge.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(256)]
        public string NickName { get; set; } = string.Empty;
        
        public int Rank { get; set; }
        public bool DarkMode { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SimJudge.Application.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(256)]
        public string NickName { get; set; } = string.Empty;
        
        public int Rank { get; set; } = 0;
        public bool DarkMode { get; set; } = false;
        
        [MaxLength(256)]
        public string? PasswordHash { get; set; }
        
        public bool EmailConfirmed { get; set; } = false;
        public bool LockoutEnabled { get; set; } = false;
        public DateTimeOffset? LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; } = 0;
    }
}

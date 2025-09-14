using System.ComponentModel.DataAnnotations;

namespace SimJudge.Domain.Entities
{
    public class User : BaseEntity
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

        public int Rank { get; set; }

        public bool DarkMode { get; set; }

        [MaxLength(256)]
        public string? PasswordHash { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public int AccessFailedCount { get; set; }
    }
}

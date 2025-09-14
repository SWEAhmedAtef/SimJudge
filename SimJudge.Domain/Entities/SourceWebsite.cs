using System.ComponentModel.DataAnnotations;

namespace SimJudge.Domain.Entities
{
    public class SourceWebsite : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? BaseUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

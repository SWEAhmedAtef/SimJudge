using System.ComponentModel.DataAnnotations;

namespace SimJudge.Domain.Entities
{
    public class Language : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Value { get; set; } = string.Empty;

        public int? SourceWebsiteId { get; set; }
        public SourceWebsite? SourceWebsite { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

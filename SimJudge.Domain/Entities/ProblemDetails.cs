using System.ComponentModel.DataAnnotations;

namespace SimJudge.Domain.Entities
{
    public class ProblemDetails : BaseEntity
    {
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

        public int? TimeLimit { get; set; } // in milliseconds

        public int? MemoryLimit { get; set; } // in KB

        [MaxLength(50)]
        public string? Difficulty { get; set; }

        [MaxLength(1000)]
        public string? Tags { get; set; }
    }
}

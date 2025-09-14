using System.ComponentModel.DataAnnotations;

namespace SimJudge.Domain.Entities
{
    public class Submission : BaseEntity
    {
        [MaxLength(50)]
        public string? SubmissionId { get; set; }

        [Required]
        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = null!;

        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

        public DateTime SubmissionTime { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string Result { get; set; } = "Pending";

        [MaxLength(50)]
        public string Time { get; set; } = "0 ms";

        [MaxLength(50)]
        public string Memory { get; set; } = "0 KB";

        [Required]
        [MaxLength(100000)]
        public string Code { get; set; } = string.Empty;

        public int? ContestId { get; set; }
        public Contest? Contest { get; set; }
    }
}

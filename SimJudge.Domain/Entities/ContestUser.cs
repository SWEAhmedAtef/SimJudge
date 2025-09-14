using System.ComponentModel.DataAnnotations;

namespace SimJudge.Domain.Entities
{
    public class ContestUser : BaseEntity
    {
        [Required]
        public int ContestId { get; set; }
        public Contest Contest { get; set; } = null!;

        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public int TotalScore { get; set; } = 0;

        public int SolvedProblems { get; set; } = 0;

        public int TotalTime { get; set; } = 0; // in minutes

        public bool IsActive { get; set; } = true;
    }
}

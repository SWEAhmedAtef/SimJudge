using System.ComponentModel.DataAnnotations;

namespace SimJudge.Domain.Entities
{
    public class Contest : BaseEntity
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
        public User Creator { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public bool IsPublic { get; set; } = true;

        [MaxLength(50)]
        public string? Password { get; set; }

        public ICollection<ContestProblem> ContestProblems { get; set; } = new List<ContestProblem>();
        public ICollection<ContestUser> ContestUsers { get; set; } = new List<ContestUser>();
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}

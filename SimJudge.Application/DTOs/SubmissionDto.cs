using System.ComponentModel.DataAnnotations;

namespace SimJudge.Application.DTOs
{
    public class SubmissionDto
    {
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string? SubmissionId { get; set; }
        
        [Required]
        public int ProblemId { get; set; }
        public string ProblemName { get; set; } = string.Empty;
        
        [Required]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        public int LanguageId { get; set; }
        public string LanguageName { get; set; } = string.Empty;
        
        public DateTime SubmissionTime { get; set; }
        
        [MaxLength(100)]
        public string Result { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string Time { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string Memory { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100000)]
        public string Code { get; set; } = string.Empty;
        
        public int? ContestId { get; set; }
        public string? ContestName { get; set; }
    }
}

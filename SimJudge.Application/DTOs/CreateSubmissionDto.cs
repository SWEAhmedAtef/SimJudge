using System.ComponentModel.DataAnnotations;

namespace SimJudge.Application.DTOs
{
    public class CreateSubmissionDto
    {
        [MaxLength(50)]
        public string? SubmissionId { get; set; }
        
        [Required]
        public int ProblemId { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int LanguageId { get; set; }
        
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
    }
}

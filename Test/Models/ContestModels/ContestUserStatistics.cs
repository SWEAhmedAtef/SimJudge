using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class ContestUserStatistics
    {
        [Key]
        public int Id { get; set; }

        public Contest Contest { get; set; }
        public int ContestId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        public Problem Problem { get; set; }
        public int ProblemId { get; set; }

        public string ProblemStatus { get; set; }
        public int WrongAnswers { get; set; }
    }
}
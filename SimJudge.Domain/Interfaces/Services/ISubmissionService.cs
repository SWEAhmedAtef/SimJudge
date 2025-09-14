using SimJudge.Domain.Entities;

namespace SimJudge.Domain.Interfaces.Services
{
    public interface ISubmissionService
    {
        Task<IEnumerable<Submission>> GetSubmissionsAsync();
        Task<Submission?> GetSubmissionByIdAsync(int id);
        Task<Submission> CreateSubmissionAsync(Submission submission);
        Task<Submission> UpdateSubmissionAsync(Submission submission);
        Task DeleteSubmissionAsync(int id);
        Task<IEnumerable<Submission>> GetUserSubmissionsAsync(int userId);
        Task<IEnumerable<Submission>> GetProblemSubmissionsAsync(int problemId);
        Task<Submission> ProcessSubmissionAsync(Submission submission, Problem problem, Language language);
    }
}

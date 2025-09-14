using SimJudge.Domain.Entities;

namespace SimJudge.Domain.Interfaces.Services
{
    public interface IProblemService
    {
        Task<IEnumerable<Problem>> GetProblemsAsync(string? query = null);
        Task<Problem?> GetProblemByIdAsync(int id);
        Task<Problem> CreateProblemAsync(Problem problem);
        Task<Problem> UpdateProblemAsync(Problem problem);
        Task DeleteProblemAsync(int id);
        Task<bool> ProblemExistsAsync(int id);
    }
}

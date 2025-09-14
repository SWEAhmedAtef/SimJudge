using SimJudge.Domain.Entities;

namespace SimJudge.Domain.Interfaces.Services
{
    public interface IContestService
    {
        Task<IEnumerable<Contest>> GetContestsAsync();
        Task<Contest?> GetContestByIdAsync(int id);
        Task<Contest> CreateContestAsync(Contest contest);
        Task<Contest> UpdateContestAsync(Contest contest);
        Task DeleteContestAsync(int id);
        Task<bool> ContestExistsAsync(int id);
        Task<IEnumerable<Contest>> GetUserContestsAsync(int userId);
    }
}

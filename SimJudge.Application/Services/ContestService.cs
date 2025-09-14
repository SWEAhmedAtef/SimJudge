using AutoMapper;
using SimJudge.Application.DTOs;
using SimJudge.Domain.Entities;
using SimJudge.Domain.Interfaces;
using SimJudge.Domain.Interfaces.Services;

namespace SimJudge.Application.Services
{
    public class ContestService : IContestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Contest>> GetContestsAsync()
        {
            return await _unitOfWork.Contests.GetAllAsync();
        }

        public async Task<Contest?> GetContestByIdAsync(int id)
        {
            return await _unitOfWork.Contests.GetByIdAsync(id);
        }

        public async Task<Contest> CreateContestAsync(Contest contest)
        {
            var createdContest = await _unitOfWork.Contests.AddAsync(contest);
            await _unitOfWork.SaveChangesAsync();
            return createdContest;
        }

        public async Task<Contest> UpdateContestAsync(Contest contest)
        {
            await _unitOfWork.Contests.UpdateAsync(contest);
            await _unitOfWork.SaveChangesAsync();
            return contest;
        }

        public async Task DeleteContestAsync(int id)
        {
            var contest = await _unitOfWork.Contests.GetByIdAsync(id);
            if (contest != null)
            {
                await _unitOfWork.Contests.DeleteAsync(contest);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<bool> ContestExistsAsync(int id)
        {
            return await _unitOfWork.Contests.ExistsAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Contest>> GetUserContestsAsync(int userId)
        {
            return await _unitOfWork.Contests.FindAsync(c => c.CreatorId == userId);
        }
    }
}

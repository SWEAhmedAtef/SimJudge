using AutoMapper;
using SimJudge.Application.DTOs;
using SimJudge.Domain.Entities;
using SimJudge.Domain.Interfaces;
using SimJudge.Domain.Interfaces.Services;

namespace SimJudge.Application.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubmissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Submission>> GetSubmissionsAsync()
        {
            return await _unitOfWork.Submissions.GetAllAsync();
        }

        public async Task<Submission?> GetSubmissionByIdAsync(int id)
        {
            return await _unitOfWork.Submissions.GetByIdAsync(id);
        }

        public async Task<Submission> CreateSubmissionAsync(Submission submission)
        {
            var createdSubmission = await _unitOfWork.Submissions.AddAsync(submission);
            await _unitOfWork.SaveChangesAsync();
            return createdSubmission;
        }

        public async Task<Submission> UpdateSubmissionAsync(Submission submission)
        {
            await _unitOfWork.Submissions.UpdateAsync(submission);
            await _unitOfWork.SaveChangesAsync();
            return submission;
        }

        public async Task DeleteSubmissionAsync(int id)
        {
            var submission = await _unitOfWork.Submissions.GetByIdAsync(id);
            if (submission != null)
            {
                await _unitOfWork.Submissions.DeleteAsync(submission);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Submission>> GetUserSubmissionsAsync(int userId)
        {
            return await _unitOfWork.Submissions.FindAsync(s => s.UserId == userId);
        }

        public async Task<IEnumerable<Submission>> GetProblemSubmissionsAsync(int problemId)
        {
            return await _unitOfWork.Submissions.FindAsync(s => s.ProblemId == problemId);
        }

        public async Task<Submission> ProcessSubmissionAsync(Submission submission, Problem problem, Language language)
        {
            // This would contain the logic from the original Submission.ProcessSubmissionAsync method
            // For now, we'll implement a simplified version
            submission.SubmissionTime = DateTime.UtcNow;
            submission.Result = "Pending";
            
            // TODO: Implement actual submission processing logic
            // This would involve calling external APIs for CodeForces or local processing for SimJudge problems
            
            return submission;
        }
    }
}

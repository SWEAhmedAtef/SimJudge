using AutoMapper;
using SimJudge.Application.Common.Exceptions;
using SimJudge.Application.DTOs;
using SimJudge.Domain.Entities;
using SimJudge.Domain.Interfaces;
using SimJudge.Domain.Interfaces.Services;

namespace SimJudge.Application.Services
{
    public class ProblemService : IProblemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProblemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Problem>> GetProblemsAsync(string? query = null)
        {
            var problems = await _unitOfWork.Problems.GetAllAsync();
            
            if (!string.IsNullOrWhiteSpace(query))
            {
                problems = problems.Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
            }

            return problems;
        }

        public async Task<Problem?> GetProblemByIdAsync(int id)
        {
            var problem = await _unitOfWork.Problems.GetByIdAsync(id);
            if (problem == null)
                throw new NotFoundException(nameof(Problem), id);
            return problem;
        }

        public async Task<Problem> CreateProblemAsync(Problem problem)
        {
            var createdProblem = await _unitOfWork.Problems.AddAsync(problem);
            await _unitOfWork.SaveChangesAsync();
            return createdProblem;
        }

        public async Task<Problem> UpdateProblemAsync(Problem problem)
        {
            if (!await ProblemExistsAsync(problem.Id))
                throw new NotFoundException(nameof(Problem), problem.Id);

            await _unitOfWork.Problems.UpdateAsync(problem);
            await _unitOfWork.SaveChangesAsync();
            return problem;
        }

        public async Task DeleteProblemAsync(int id)
        {
            var problem = await _unitOfWork.Problems.GetByIdAsync(id);
            if (problem == null)
                throw new NotFoundException(nameof(Problem), id);

            await _unitOfWork.Problems.DeleteAsync(problem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> ProblemExistsAsync(int id)
        {
            return await _unitOfWork.Problems.ExistsAsync(p => p.Id == id);
        }
    }
}

using Microsoft.EntityFrameworkCore.Storage;
using SimJudge.Domain.Entities;
using SimJudge.Domain.Interfaces;
using SimJudge.Infrastructure.Data;

namespace SimJudge.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SimJudgeDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(SimJudgeDbContext context)
        {
            _context = context;
            Users = new Repository<User>(_context);
            Problems = new Repository<Problem>(_context);
            Contests = new Repository<Contest>(_context);
            Submissions = new Repository<Submission>(_context);
            Languages = new Repository<Language>(_context);
            SourceWebsites = new Repository<SourceWebsite>(_context);
            ProblemDetails = new Repository<ProblemDetails>(_context);
            CodeForcesDetails = new Repository<CodeForcesDetails>(_context);
            SimJudgeDetails = new Repository<SimJudgeDetails>(_context);
            ContestProblems = new Repository<ContestProblem>(_context);
            ContestUsers = new Repository<ContestUser>(_context);
        }

        public IRepository<User> Users { get; }
        public IRepository<Problem> Problems { get; }
        public IRepository<Contest> Contests { get; }
        public IRepository<Submission> Submissions { get; }
        public IRepository<Language> Languages { get; }
        public IRepository<SourceWebsite> SourceWebsites { get; }
        public IRepository<ProblemDetails> ProblemDetails { get; }
        public IRepository<CodeForcesDetails> CodeForcesDetails { get; }
        public IRepository<SimJudgeDetails> SimJudgeDetails { get; }
        public IRepository<ContestProblem> ContestProblems { get; }
        public IRepository<ContestUser> ContestUsers { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}

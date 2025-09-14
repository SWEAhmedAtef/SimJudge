using SimJudge.Domain.Entities;

namespace SimJudge.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Problem> Problems { get; }
        IRepository<Contest> Contests { get; }
        IRepository<Submission> Submissions { get; }
        IRepository<Language> Languages { get; }
        IRepository<SourceWebsite> SourceWebsites { get; }
        IRepository<ProblemDetails> ProblemDetails { get; }
        IRepository<CodeForcesDetails> CodeForcesDetails { get; }
        IRepository<SimJudgeDetails> SimJudgeDetails { get; }
        IRepository<ContestProblem> ContestProblems { get; }
        IRepository<ContestUser> ContestUsers { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}

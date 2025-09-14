using Microsoft.EntityFrameworkCore;
using SimJudge.Domain.Entities;

namespace SimJudge.Infrastructure.Data
{
    public class SimJudgeDbContext : DbContext
    {
        public SimJudgeDbContext(DbContextOptions<SimJudgeDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<ProblemDetails> ProblemDetails { get; set; }
        public DbSet<CodeForcesDetails> CodeForcesDetails { get; set; }
        public DbSet<SimJudgeDetails> SimJudgeDetails { get; set; }
        public DbSet<SourceWebsite> SourceWebsites { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<ContestProblem> ContestProblems { get; set; }
        public DbSet<ContestUser> ContestUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
                entity.Property(e => e.NickName).IsRequired().HasMaxLength(256);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UserName).IsUnique();
            });

            // Problem configuration
            modelBuilder.Entity<Problem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.SourceProblemId).IsRequired().HasMaxLength(50);
                
                entity.HasOne(e => e.SourceWebsite)
                    .WithMany()
                    .HasForeignKey(e => e.SourceWebsiteId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ProblemDetails)
                    .WithMany()
                    .HasForeignKey(e => e.ProblemDetailsId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.CodeForcesDetails)
                    .WithMany()
                    .HasForeignKey(e => e.CodeForcesDetailsId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.SimJudgeDetails)
                    .WithMany()
                    .HasForeignKey(e => e.SimJudgeDetailsId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Contest configuration
            modelBuilder.Entity<Contest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Password).HasMaxLength(50);

                entity.HasOne(e => e.Creator)
                    .WithMany()
                    .HasForeignKey(e => e.CreatorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Submission configuration
            modelBuilder.Entity<Submission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(100000);
                entity.Property(e => e.Result).HasMaxLength(100);
                entity.Property(e => e.Time).HasMaxLength(50);
                entity.Property(e => e.Memory).HasMaxLength(50);

                entity.HasOne(e => e.Problem)
                    .WithMany()
                    .HasForeignKey(e => e.ProblemId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Language)
                    .WithMany()
                    .HasForeignKey(e => e.LanguageId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Contest)
                    .WithMany(c => c.Submissions)
                    .HasForeignKey(e => e.ContestId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // ContestProblem configuration
            modelBuilder.Entity<ContestProblem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Index).IsRequired().HasMaxLength(10);

                entity.HasOne(e => e.Contest)
                    .WithMany(c => c.ContestProblems)
                    .HasForeignKey(e => e.ContestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Problem)
                    .WithMany()
                    .HasForeignKey(e => e.ProblemId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.ContestId, e.Index }).IsUnique();
            });

            // ContestUser configuration
            modelBuilder.Entity<ContestUser>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Contest)
                    .WithMany(c => c.ContestUsers)
                    .HasForeignKey(e => e.ContestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.ContestId, e.UserId }).IsUnique();
            });

            // SourceWebsite configuration
            modelBuilder.Entity<SourceWebsite>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.BaseUrl).HasMaxLength(500);
            });

            // Language configuration
            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Value).IsRequired().HasMaxLength(20);

                entity.HasOne(e => e.SourceWebsite)
                    .WithMany()
                    .HasForeignKey(e => e.SourceWebsiteId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // ProblemDetails configuration
            modelBuilder.Entity<ProblemDetails>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.Description).HasMaxLength(10000);
                entity.Property(e => e.InputSpecification).HasMaxLength(10000);
                entity.Property(e => e.OutputSpecification).HasMaxLength(10000);
                entity.Property(e => e.SampleInput).HasMaxLength(1000);
                entity.Property(e => e.SampleOutput).HasMaxLength(1000);
                entity.Property(e => e.Note).HasMaxLength(1000);
                entity.Property(e => e.Difficulty).HasMaxLength(50);
                entity.Property(e => e.Tags).HasMaxLength(1000);
            });

            // CodeForcesDetails configuration
            modelBuilder.Entity<CodeForcesDetails>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ContestId).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Index).IsRequired().HasMaxLength(10);
                entity.Property(e => e.ProblemUrl).HasMaxLength(1000);
                entity.Property(e => e.Tags).HasMaxLength(1000);
            });

            // SimJudgeDetails configuration
            modelBuilder.Entity<SimJudgeDetails>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TestCaseInput).IsRequired().HasMaxLength(10000);
                entity.Property(e => e.TestCaseOutput).IsRequired().HasMaxLength(10000);
                entity.Property(e => e.AdditionalTestCases).HasMaxLength(1000);
                entity.Property(e => e.Difficulty).HasMaxLength(50);
                entity.Property(e => e.Tags).HasMaxLength(1000);
            });
        }
    }
}

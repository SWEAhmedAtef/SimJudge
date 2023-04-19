using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Test.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Problem> Problems { get; set; }

		public DbSet<ProblemDetails> ProblemDetails { get; set; }

		public DbSet<CodeForcesDetails> CodeForcesDetails { get; set; }

		public DbSet<SimJudgeDetails> SimJudgeDetails { get; set; }

		public DbSet<SourceWebsite> SourceWebsites { get; set; }

		public DbSet<Language> Languages { get; set; }

		public DbSet<Submission> Submissions { get; set; }

		public DbSet<Tutorial> Tutorials { get; set; }

		public DbSet<TutorialDetails> TurorialDetails { get; set; }

		public DbSet<TutorialComment> TutorialComments { get; set; }

		public DbSet<Blog> Blogs { get; set; }

		public DbSet<BlogDetails> BlogDetails { get; set; }

		public DbSet<BlogComment> BlogComments { get; set; }

		public DbSet<Contest> Contests { get; set; }

		public DbSet<ContestProblem> ContestProblems { get; set; }

		public DbSet<ContestUser> ContestUsers { get; set; }

		public DbSet<ContestSubmission> ContestSubmissions { get; set; }

		public DbSet<ContestUserStatistics> ContestUserStatistics { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
          
			base.OnModelCreating(modelBuilder);
			//modelBuilder.Entity().HasMany()

        }

		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}
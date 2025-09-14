using SimJudge.Domain.Entities;

namespace SimJudge.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(SimJudgeDbContext context)
        {
            if (!context.SourceWebsites.Any())
            {
                var sourceWebsites = new List<SourceWebsite>
                {
                    new SourceWebsite { Name = "SimJudge", BaseUrl = "https://simjudge.com", IsActive = true },
                    new SourceWebsite { Name = "CodeForces", BaseUrl = "https://codeforces.com", IsActive = true },
                    new SourceWebsite { Name = "AtCoder", BaseUrl = "https://atcoder.jp", IsActive = true }
                };

                context.SourceWebsites.AddRange(sourceWebsites);
                await context.SaveChangesAsync();
            }

            if (!context.Languages.Any())
            {
                var languages = new List<Language>
                {
                    new Language { Name = "C++", Value = "cpp", IsActive = true },
                    new Language { Name = "Java", Value = "java", IsActive = true },
                    new Language { Name = "Python", Value = "python", IsActive = true },
                    new Language { Name = "C#", Value = "csharp", IsActive = true }
                };

                context.Languages.AddRange(languages);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User 
                    { 
                        UserName = "admin", 
                        Email = "admin@simjudge.com", 
                        NickName = "Administrator",
                        Rank = 1,
                        DarkMode = false,
                        EmailConfirmed = true
                    },
                    new User 
                    { 
                        UserName = "testuser", 
                        Email = "test@simjudge.com", 
                        NickName = "Test User",
                        Rank = 100,
                        DarkMode = false,
                        EmailConfirmed = true
                    }
                };

                context.Users.AddRange(users);
                await context.SaveChangesAsync();
            }

            if (!context.Problems.Any())
            {
                var simJudgeSource = context.SourceWebsites.First(s => s.Name == "SimJudge");
                var adminUser = context.Users.First(u => u.UserName == "admin");

                var problemDetails = new ProblemDetails
                {
                    Title = "Hello World",
                    Description = "Print 'Hello World' to the console.",
                    InputSpecification = "No input required.",
                    OutputSpecification = "Output should be 'Hello World'.",
                    SampleInput = "",
                    SampleOutput = "Hello World",
                    TimeLimit = 1000,
                    MemoryLimit = 256,
                    Difficulty = "Easy"
                };

                var simJudgeDetails = new SimJudgeDetails
                {
                    TestCaseInput = "",
                    TestCaseOutput = "Hello World",
                    TimeLimit = 1000,
                    MemoryLimit = 256,
                    Difficulty = "Easy"
                };

                context.ProblemDetails.Add(problemDetails);
                context.SimJudgeDetails.Add(simJudgeDetails);
                await context.SaveChangesAsync();

                var problem = new Problem
                {
                    Name = "Hello World",
                    SourceProblemId = "HW001",
                    SourceWebsiteId = simJudgeSource.Id,
                    ProblemDetailsId = problemDetails.Id,
                    SimJudgeDetailsId = simJudgeDetails.Id,
                    IsActive = true
                };

                context.Problems.Add(problem);
                await context.SaveChangesAsync();
            }

            if (!context.Contests.Any())
            {
                var adminUser = context.Users.First(u => u.UserName == "admin");

                var contest = new Contest
                {
                    Name = "Welcome Contest",
                    Description = "A beginner-friendly contest to get started with SimJudge",
                    StartDate = DateTime.UtcNow.AddDays(-1),
                    EndDate = DateTime.UtcNow.AddDays(7),
                    CreatorId = adminUser.Id,
                    IsActive = true,
                    IsPublic = true
                };

                context.Contests.Add(contest);
                await context.SaveChangesAsync();
            }
        }
    }
}

namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        Comment = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NickName = c.String(nullable: false),
                        Rank = c.Int(nullable: false),
                        DarkMode = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        BlogDetailsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.BlogDetails", t => t.BlogDetailsId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.BlogDetailsId);
            
            CreateTable(
                "dbo.BlogDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Image = c.String(),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CodeForcesDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SourceProblemId = c.String(),
                        SourceUrl = c.String(),
                        ContestSource = c.String(),
                        Examples = c.String(nullable: false),
                        Tags = c.String(),
                        InputFile = c.String(),
                        OutputFile = c.String(),
                        Editorial = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContestProblems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContestId = c.Int(nullable: false),
                        ProblemIndex = c.String(nullable: false, maxLength: 1),
                        ProblemNickName = c.String(),
                        Problem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contests", t => t.ContestId, cascadeDelete: true)
                .ForeignKey("dbo.Problems", t => t.Problem_Id)
                .Index(t => t.ContestId)
                .Index(t => t.Problem_Id);
            
            CreateTable(
                "dbo.Contests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Problems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SourceWebsite = c.String(),
                        ProblemDetailsId = c.Int(nullable: false),
                        CodeForcesDetailsId = c.Int(),
                        SimJudgeDetailsId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CodeForcesDetails", t => t.CodeForcesDetailsId)
                .ForeignKey("dbo.ProblemDetails", t => t.ProblemDetailsId, cascadeDelete: true)
                .ForeignKey("dbo.SimJudgeDetails", t => t.SimJudgeDetailsId)
                .Index(t => t.ProblemDetailsId)
                .Index(t => t.CodeForcesDetailsId)
                .Index(t => t.SimJudgeDetailsId);
            
            CreateTable(
                "dbo.ProblemDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Input = c.String(nullable: false),
                        Output = c.String(nullable: false),
                        Notes = c.String(),
                        TimeLimit = c.String(),
                        MemoryLimit = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SimJudgeDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestCaseInput = c.String(),
                        TestCaseOutput = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContestSubmissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContestId = c.Int(nullable: false),
                        ProblemId = c.Int(nullable: false),
                        SubmissionId = c.String(),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        LanguageId = c.Int(nullable: false),
                        SubmissionTime = c.DateTime(nullable: false),
                        Result = c.String(maxLength: 100),
                        Time = c.String(),
                        Memory = c.String(),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Contests", t => t.ContestId, cascadeDelete: true)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.Problems", t => t.ProblemId, cascadeDelete: true)
                .Index(t => t.ContestId)
                .Index(t => t.ProblemId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContestUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContestId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        Penalty = c.Int(nullable: false),
                        SolvedProblems = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Contests", t => t.ContestId, cascadeDelete: true)
                .Index(t => t.ContestId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.ContestUserStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContestId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        ProblemId = c.Int(nullable: false),
                        ProblemStatus = c.String(),
                        WrongAnswers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Contests", t => t.ContestId, cascadeDelete: true)
                .ForeignKey("dbo.Problems", t => t.ProblemId, cascadeDelete: true)
                .Index(t => t.ContestId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ProblemId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Submissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubmissionId = c.String(),
                        ProblemId = c.Int(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        LanguageId = c.Int(nullable: false),
                        SubmissionTime = c.DateTime(nullable: false),
                        Result = c.String(maxLength: 100),
                        Time = c.String(),
                        Memory = c.String(),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.Problems", t => t.ProblemId, cascadeDelete: true)
                .Index(t => t.ProblemId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.TutorialDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        VideoUrl = c.String(),
                        Tags = c.String(),
                        RelatedProblemId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Problems", t => t.RelatedProblemId)
                .Index(t => t.RelatedProblemId);
            
            CreateTable(
                "dbo.TutorialComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TutorialId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        Comment = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Tutorials", t => t.TutorialId, cascadeDelete: true)
                .Index(t => t.TutorialId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Tutorials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        TutorialDetailsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.TutorialDetails", t => t.TutorialDetailsId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.TutorialDetailsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TutorialComments", "TutorialId", "dbo.Tutorials");
            DropForeignKey("dbo.Tutorials", "TutorialDetailsId", "dbo.TutorialDetails");
            DropForeignKey("dbo.Tutorials", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TutorialComments", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TutorialDetails", "RelatedProblemId", "dbo.Problems");
            DropForeignKey("dbo.Submissions", "ProblemId", "dbo.Problems");
            DropForeignKey("dbo.Submissions", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.Submissions", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ContestUserStatistics", "ProblemId", "dbo.Problems");
            DropForeignKey("dbo.ContestUserStatistics", "ContestId", "dbo.Contests");
            DropForeignKey("dbo.ContestUserStatistics", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestUsers", "ContestId", "dbo.Contests");
            DropForeignKey("dbo.ContestUsers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestSubmissions", "ProblemId", "dbo.Problems");
            DropForeignKey("dbo.ContestSubmissions", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.ContestSubmissions", "ContestId", "dbo.Contests");
            DropForeignKey("dbo.ContestSubmissions", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestProblems", "Problem_Id", "dbo.Problems");
            DropForeignKey("dbo.Problems", "SimJudgeDetailsId", "dbo.SimJudgeDetails");
            DropForeignKey("dbo.Problems", "ProblemDetailsId", "dbo.ProblemDetails");
            DropForeignKey("dbo.Problems", "CodeForcesDetailsId", "dbo.CodeForcesDetails");
            DropForeignKey("dbo.ContestProblems", "ContestId", "dbo.Contests");
            DropForeignKey("dbo.Contests", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogComments", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.Blogs", "BlogDetailsId", "dbo.BlogDetails");
            DropForeignKey("dbo.Blogs", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogComments", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Tutorials", new[] { "TutorialDetailsId" });
            DropIndex("dbo.Tutorials", new[] { "ApplicationUserId" });
            DropIndex("dbo.TutorialComments", new[] { "ApplicationUserId" });
            DropIndex("dbo.TutorialComments", new[] { "TutorialId" });
            DropIndex("dbo.TutorialDetails", new[] { "RelatedProblemId" });
            DropIndex("dbo.Submissions", new[] { "LanguageId" });
            DropIndex("dbo.Submissions", new[] { "ApplicationUserId" });
            DropIndex("dbo.Submissions", new[] { "ProblemId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ContestUserStatistics", new[] { "ProblemId" });
            DropIndex("dbo.ContestUserStatistics", new[] { "ApplicationUserId" });
            DropIndex("dbo.ContestUserStatistics", new[] { "ContestId" });
            DropIndex("dbo.ContestUsers", new[] { "ApplicationUserId" });
            DropIndex("dbo.ContestUsers", new[] { "ContestId" });
            DropIndex("dbo.ContestSubmissions", new[] { "LanguageId" });
            DropIndex("dbo.ContestSubmissions", new[] { "ApplicationUserId" });
            DropIndex("dbo.ContestSubmissions", new[] { "ProblemId" });
            DropIndex("dbo.ContestSubmissions", new[] { "ContestId" });
            DropIndex("dbo.Problems", new[] { "SimJudgeDetailsId" });
            DropIndex("dbo.Problems", new[] { "CodeForcesDetailsId" });
            DropIndex("dbo.Problems", new[] { "ProblemDetailsId" });
            DropIndex("dbo.Contests", new[] { "ApplicationUserId" });
            DropIndex("dbo.ContestProblems", new[] { "Problem_Id" });
            DropIndex("dbo.ContestProblems", new[] { "ContestId" });
            DropIndex("dbo.Blogs", new[] { "BlogDetailsId" });
            DropIndex("dbo.Blogs", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BlogComments", new[] { "ApplicationUserId" });
            DropIndex("dbo.BlogComments", new[] { "BlogId" });
            DropTable("dbo.Tutorials");
            DropTable("dbo.TutorialComments");
            DropTable("dbo.TutorialDetails");
            DropTable("dbo.Submissions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ContestUserStatistics");
            DropTable("dbo.ContestUsers");
            DropTable("dbo.Languages");
            DropTable("dbo.ContestSubmissions");
            DropTable("dbo.SimJudgeDetails");
            DropTable("dbo.ProblemDetails");
            DropTable("dbo.Problems");
            DropTable("dbo.Contests");
            DropTable("dbo.ContestProblems");
            DropTable("dbo.CodeForcesDetails");
            DropTable("dbo.BlogDetails");
            DropTable("dbo.Blogs");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BlogComments");
        }
    }
}

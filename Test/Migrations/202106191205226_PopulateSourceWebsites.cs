namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSourceWebsites : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[SourceWebsites] ([Name]) VALUES (N'SimJudge')");
            Sql("INSERT INTO [dbo].[SourceWebsites] ([Name]) VALUES (N'CodeForces')");
        }
        
        public override void Down()
        {
        }
    }
}

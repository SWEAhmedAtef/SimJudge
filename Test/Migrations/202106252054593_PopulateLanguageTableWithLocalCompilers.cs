namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateLanguageTableWithLocalCompilers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO[dbo].[Languages]([Name], [Value], [SourceWebsiteId]) VALUES(N'C++', 1, 1)");
            Sql("INSERT INTO[dbo].[Languages]([Name], [Value], [SourceWebsiteId]) VALUES(N'C', 2, 1)");
        }
        
        public override void Down()
        {
        }
    }
}

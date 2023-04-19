namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovedSourceProblemIdChangedSourceWebsiteToModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SourceWebsites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Problems", "SourceProblemId", c => c.String());
            AddColumn("dbo.Problems", "SourceWebsiteId", c => c.Int(nullable: false));
            CreateIndex("dbo.Problems", "SourceWebsiteId");
            AddForeignKey("dbo.Problems", "SourceWebsiteId", "dbo.SourceWebsites", "Id", cascadeDelete: true);
            DropColumn("dbo.CodeForcesDetails", "SourceProblemId");
            DropColumn("dbo.Problems", "SourceWebsite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Problems", "SourceWebsite", c => c.String());
            AddColumn("dbo.CodeForcesDetails", "SourceProblemId", c => c.String());
            DropForeignKey("dbo.Problems", "SourceWebsiteId", "dbo.SourceWebsites");
            DropIndex("dbo.Problems", new[] { "SourceWebsiteId" });
            DropColumn("dbo.Problems", "SourceWebsiteId");
            DropColumn("dbo.Problems", "SourceProblemId");
            DropTable("dbo.SourceWebsites");
        }
    }
}

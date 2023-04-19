namespace Test.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class AddSourceWebsiteToLanguageModel : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Languages", "SourceWebsiteId", c => c.Int());
			CreateIndex("dbo.Languages", "SourceWebsiteId");
			AddForeignKey("dbo.Languages", "SourceWebsiteId", "dbo.SourceWebsites", "Id");
		}

		public override void Down()
		{
			DropForeignKey("dbo.Languages", "SourceWebsiteId", "dbo.SourceWebsites");
			DropIndex("dbo.Languages", new[] { "SourceWebsiteId" });
			DropColumn("dbo.Languages", "SourceWebsiteId");
		}
	}
}

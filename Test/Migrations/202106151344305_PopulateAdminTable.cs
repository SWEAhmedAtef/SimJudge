namespace Test.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class PopulateAdminTable : DbMigration
	{
		public override void Up()
		{
			Sql(@"
				INSERT INTO [dbo].[AspNetUsers] ([Id], [NickName], [Rank], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DarkMode]) VALUES (N'199db8ad-b85d-430b-ad12-9be89fb9dfdf', N'admin', 0, N'admin@simjudge.com', 0, N'AEXoaipyQSPtBz4QtZubR6TWfyERoVKg0ez0E1NjZm6XUtFlssnirJYj03UkQ7BDmw==', N'4b151500-5aaa-466b-b900-813c3c26ed06', NULL, 0, 0, NULL, 1, 0, N'admin', 0)
                INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'c4c6387f-9be7-4561-a2db-abea59c2accf', N'IsAdmin') 
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'199db8ad-b85d-430b-ad12-9be89fb9dfdf', N'c4c6387f-9be7-4561-a2db-abea59c2accf')
                "
			);
		}

		public override void Down()
		{
		}
	}
}

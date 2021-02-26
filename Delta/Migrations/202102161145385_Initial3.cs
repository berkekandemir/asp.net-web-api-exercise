namespace Delta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Team_Id", "dbo.Teams");
            DropIndex("dbo.Users", new[] { "Team_Id" });
            DropColumn("dbo.Users", "Team_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Team_Id", c => c.Int());
            CreateIndex("dbo.Users", "Team_Id");
            AddForeignKey("dbo.Users", "Team_Id", "dbo.Teams", "Id");
        }
    }
}

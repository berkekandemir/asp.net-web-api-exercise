namespace Delta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "TeamId", "dbo.Teams");
            DropIndex("dbo.Users", new[] { "TeamId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Users", "TeamId");
            AddForeignKey("dbo.Users", "TeamId", "dbo.Teams", "Id");
        }
    }
}

namespace Delta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Users", "TeamId");
            AddForeignKey("dbo.Users", "TeamId", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "TeamId", "dbo.Teams");
            DropIndex("dbo.Users", new[] { "TeamId" });
        }
    }
}

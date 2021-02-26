namespace Delta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PresidentId = c.Int(nullable: false),
                        CoachId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CoachId)
                .ForeignKey("dbo.Users", t => t.PresidentId, cascadeDelete: true)
                .Index(t => t.PresidentId)
                .Index(t => t.CoachId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        TeamId = c.Int(),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .Index(t => t.TeamId)
                .Index(t => t.Team_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "PresidentId", "dbo.Users");
            DropForeignKey("dbo.Users", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Teams", "CoachId", "dbo.Users");
            DropForeignKey("dbo.Users", "TeamId", "dbo.Teams");
            DropIndex("dbo.Users", new[] { "Team_Id" });
            DropIndex("dbo.Users", new[] { "TeamId" });
            DropIndex("dbo.Teams", new[] { "CoachId" });
            DropIndex("dbo.Teams", new[] { "PresidentId" });
            DropTable("dbo.Users");
            DropTable("dbo.Teams");
        }
    }
}

namespace TeamExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coaches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PresidentId = c.Int(nullable: false),
                        CoachId = c.Int(nullable: false),
                        PlayerId1 = c.Int(nullable: false),
                        PlayerId2 = c.Int(nullable: false),
                        PlayerId3 = c.Int(nullable: false),
                        PlayerId4 = c.Int(nullable: false),
                        PlayerId5 = c.Int(nullable: false),
                        PlayerId6 = c.Int(nullable: false),
                        PlayerId7 = c.Int(nullable: false),
                        PlayerId8 = c.Int(nullable: false),
                        PlayerId9 = c.Int(nullable: false),
                        PlayerId10 = c.Int(nullable: false),
                        PlayerId11 = c.Int(nullable: false),
                        PlayerId12 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coaches", t => t.CoachId, cascadeDelete: true)
                .ForeignKey("dbo.Presidents", t => t.PresidentId, cascadeDelete: true)
                .Index(t => t.PresidentId)
                .Index(t => t.CoachId);
            
            CreateTable(
                "dbo.Presidents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SysAdmins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "PresidentId", "dbo.Presidents");
            DropForeignKey("dbo.Teams", "CoachId", "dbo.Coaches");
            DropIndex("dbo.Teams", new[] { "CoachId" });
            DropIndex("dbo.Teams", new[] { "PresidentId" });
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropTable("dbo.SysAdmins");
            DropTable("dbo.Presidents");
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.Coaches");
        }
    }
}

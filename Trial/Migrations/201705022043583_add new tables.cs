namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentDesc = c.String(),
                        ProjectManagerId = c.String(maxLength: 128),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ProjectManagerId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectManagerId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EvaluatingTLId = c.String(maxLength: 128),
                        EvaluatedJDId = c.String(maxLength: 128),
                        FeedbackDesc = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.EvaluatedJDId)
                .ForeignKey("dbo.AspNetUsers", t => t.EvaluatingTLId)
                .Index(t => t.EvaluatingTLId)
                .Index(t => t.EvaluatedJDId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromUserId = c.String(maxLength: 128),
                        ToUserId = c.String(maxLength: 128),
                        ProjectId = c.Int(nullable: false),
                        Answer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FromUserId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ToUserId)
                .Index(t => t.FromUserId)
                .Index(t => t.ToUserId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Submits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PmId = c.String(maxLength: 128),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PmId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.PmId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Worked_on",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Works_on",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Works_on", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Works_on", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Worked_on", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Worked_on", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Submits", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Submits", "PmId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "ToUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Requests", "FromUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "EvaluatingTLId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "EvaluatedJDId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ProjectManagerId", "dbo.AspNetUsers");
            DropIndex("dbo.Works_on", new[] { "UserId" });
            DropIndex("dbo.Works_on", new[] { "ProjectId" });
            DropIndex("dbo.Worked_on", new[] { "UserId" });
            DropIndex("dbo.Worked_on", new[] { "ProjectId" });
            DropIndex("dbo.Submits", new[] { "ProjectId" });
            DropIndex("dbo.Submits", new[] { "PmId" });
            DropIndex("dbo.Requests", new[] { "ProjectId" });
            DropIndex("dbo.Requests", new[] { "ToUserId" });
            DropIndex("dbo.Requests", new[] { "FromUserId" });
            DropIndex("dbo.Feedbacks", new[] { "EvaluatedJDId" });
            DropIndex("dbo.Feedbacks", new[] { "EvaluatingTLId" });
            DropIndex("dbo.Comments", new[] { "ProjectId" });
            DropIndex("dbo.Comments", new[] { "ProjectManagerId" });
            DropTable("dbo.Works_on");
            DropTable("dbo.Worked_on");
            DropTable("dbo.Submits");
            DropTable("dbo.Requests");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Comments");
        }
    }
}

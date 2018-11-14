namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFeedbackmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "ProjectId", c => c.Int(nullable: false));
            AddColumn("dbo.Feedbacks", "Communication", c => c.Int(nullable: false));
            AddColumn("dbo.Feedbacks", "TeamWork", c => c.Int(nullable: false));
            AddColumn("dbo.Feedbacks", "ProblemSolving", c => c.Int(nullable: false));
            AddColumn("dbo.Feedbacks", "WorkKnowledge", c => c.Int(nullable: false));
            AddColumn("dbo.Feedbacks", "IndependantAction", c => c.Int(nullable: false));
            CreateIndex("dbo.Feedbacks", "ProjectId");
            AddForeignKey("dbo.Feedbacks", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Feedbacks", new[] { "ProjectId" });
            DropColumn("dbo.Feedbacks", "IndependantAction");
            DropColumn("dbo.Feedbacks", "WorkKnowledge");
            DropColumn("dbo.Feedbacks", "ProblemSolving");
            DropColumn("dbo.Feedbacks", "TeamWork");
            DropColumn("dbo.Feedbacks", "Communication");
            DropColumn("dbo.Feedbacks", "ProjectId");
        }
    }
}

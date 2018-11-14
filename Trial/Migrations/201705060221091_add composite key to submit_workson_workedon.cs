namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcompositekeytosubmit_workson_workedon : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Submits", "PmId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Worked_on", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Works_on", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Submits", new[] { "PmId" });
            DropIndex("dbo.Worked_on", new[] { "UserId" });
            DropIndex("dbo.Works_on", new[] { "UserId" });
            DropPrimaryKey("dbo.Submits");
            DropPrimaryKey("dbo.Worked_on");
            DropPrimaryKey("dbo.Works_on");
            AlterColumn("dbo.Submits", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Submits", "PmId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Worked_on", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Worked_on", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Works_on", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Works_on", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Submits", new[] { "Id", "PmId", "ProjectId" });
            AddPrimaryKey("dbo.Worked_on", new[] { "Id", "ProjectId", "UserId" });
            AddPrimaryKey("dbo.Works_on", new[] { "Id", "ProjectId", "UserId" });
            CreateIndex("dbo.Submits", "PmId");
            CreateIndex("dbo.Worked_on", "UserId");
            CreateIndex("dbo.Works_on", "UserId");
            AddForeignKey("dbo.Submits", "PmId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Worked_on", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Works_on", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Works_on", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Worked_on", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Submits", "PmId", "dbo.AspNetUsers");
            DropIndex("dbo.Works_on", new[] { "UserId" });
            DropIndex("dbo.Worked_on", new[] { "UserId" });
            DropIndex("dbo.Submits", new[] { "PmId" });
            DropPrimaryKey("dbo.Works_on");
            DropPrimaryKey("dbo.Worked_on");
            DropPrimaryKey("dbo.Submits");
            AlterColumn("dbo.Works_on", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Works_on", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Worked_on", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Worked_on", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Submits", "PmId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Submits", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Works_on", "Id");
            AddPrimaryKey("dbo.Worked_on", "Id");
            AddPrimaryKey("dbo.Submits", "Id");
            CreateIndex("dbo.Works_on", "UserId");
            CreateIndex("dbo.Worked_on", "UserId");
            CreateIndex("dbo.Submits", "PmId");
            AddForeignKey("dbo.Works_on", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Worked_on", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Submits", "PmId", "dbo.AspNetUsers", "Id");
        }
    }
}

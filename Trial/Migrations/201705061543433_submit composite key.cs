namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class submitcompositekey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Submits", "PmId", "dbo.AspNetUsers");
            DropIndex("dbo.Submits", new[] { "PmId" });
            DropPrimaryKey("dbo.Submits");
            AlterColumn("dbo.Submits", "PmId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Submits", new[] { "PmId", "ProjectId" });
            CreateIndex("dbo.Submits", "PmId");
            AddForeignKey("dbo.Submits", "PmId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Submits", "PmId", "dbo.AspNetUsers");
            DropIndex("dbo.Submits", new[] { "PmId" });
            DropPrimaryKey("dbo.Submits");
            AlterColumn("dbo.Submits", "PmId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Submits", "Id");
            CreateIndex("dbo.Submits", "PmId");
            AddForeignKey("dbo.Submits", "PmId", "dbo.AspNetUsers", "Id");
        }
    }
}

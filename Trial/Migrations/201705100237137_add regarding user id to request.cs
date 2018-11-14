namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addregardinguseridtorequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "RegardingUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Requests", "RegardingUserId");
            AddForeignKey("dbo.Requests", "RegardingUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "RegardingUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Requests", new[] { "RegardingUserId" });
            DropColumn("dbo.Requests", "RegardingUserId");
        }
    }
}

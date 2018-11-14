namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Fname", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.AspNetUsers", "Lname", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.AspNetUsers", "Job_Description", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.AspNetUsers", "PhoneNum", c => c.String(nullable: false, maxLength: 12));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PhoneNum");
            DropColumn("dbo.AspNetUsers", "Job_Description");
            DropColumn("dbo.AspNetUsers", "Lname");
            DropColumn("dbo.AspNetUsers", "Fname");
        }
    }
}

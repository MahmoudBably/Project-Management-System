namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfilePicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfilePicture", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ProfilePicture");
        }
    }
}

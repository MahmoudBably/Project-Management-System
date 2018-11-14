namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtypeattributetoRequests : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "Type");
        }
    }
}

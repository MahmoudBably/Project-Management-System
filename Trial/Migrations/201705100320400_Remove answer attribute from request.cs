namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removeanswerattributefromrequest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Requests", "Answer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "Answer", c => c.Boolean(nullable: false));
        }
    }
}

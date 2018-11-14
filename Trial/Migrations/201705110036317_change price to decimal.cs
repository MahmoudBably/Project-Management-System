namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changepricetodecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ProjectHistories", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProjectHistories", "Price", c => c.Short(nullable: false));
            AlterColumn("dbo.Projects", "Price", c => c.Short(nullable: false));
        }
    }
}

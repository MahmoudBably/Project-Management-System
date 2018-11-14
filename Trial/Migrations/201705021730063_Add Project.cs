namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CustomerId = c.String(maxLength: 128),
                        Price = c.Short(nullable: false),
                        Shcedule_From = c.DateTime(nullable: false),
                        Schedule_To = c.DateTime(nullable: false),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        Assigned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            DropTable("dbo.Projects");
        }
    }
}
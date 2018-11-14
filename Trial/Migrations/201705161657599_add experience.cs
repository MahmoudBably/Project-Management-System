namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addexperience : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ASPNETMVC = c.Int(nullable: false),
                        JavaScript = c.Int(nullable: false),
                        JQuery = c.Int(nullable: false),
                        HTML5 = c.Int(nullable: false),
                        PHP = c.Int(nullable: false),
                        JAVA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Experiences", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Experiences", new[] { "UserId" });
            DropTable("dbo.Experiences");
        }
    }
}

namespace Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Shcedule_From", c => c.DateTime());
            AlterColumn("dbo.Projects", "Schedule_To", c => c.DateTime());
            AlterColumn("dbo.ProjectHistories", "Shcedule_From", c => c.DateTime());
            AlterColumn("dbo.ProjectHistories", "Schedule_To", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProjectHistories", "Schedule_To", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProjectHistories", "Shcedule_From", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Projects", "Schedule_To", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Projects", "Shcedule_From", c => c.DateTime(nullable: false));
        }
    }
}

namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedFieldsInProjectsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProjects", "ProjectStartDate", c => c.DateTime());
            AddColumn("dbo.tblProjects", "ProjectEndDate", c => c.DateTime());
            AddColumn("dbo.tblProjects", "ProjectPriority", c => c.Int(nullable: false));
            DropColumn("dbo.tblProjects", "StartDate");
            DropColumn("dbo.tblProjects", "EndDate");
            DropColumn("dbo.tblProjects", "Priority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblProjects", "Priority", c => c.Int(nullable: false));
            AddColumn("dbo.tblProjects", "EndDate", c => c.DateTime());
            AddColumn("dbo.tblProjects", "StartDate", c => c.DateTime());
            DropColumn("dbo.tblProjects", "ProjectPriority");
            DropColumn("dbo.tblProjects", "ProjectEndDate");
            DropColumn("dbo.tblProjects", "ProjectStartDate");
        }
    }
}

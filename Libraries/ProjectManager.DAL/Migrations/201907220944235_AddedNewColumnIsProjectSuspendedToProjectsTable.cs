namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewColumnIsProjectSuspendedToProjectsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProjects", "IsProjectSuspended", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblProjects", "IsProjectSuspended");
        }
    }
}

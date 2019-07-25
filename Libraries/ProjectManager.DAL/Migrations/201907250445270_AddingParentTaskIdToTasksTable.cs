namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingParentTaskIdToTasksTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblTasks", "ParentTaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblTasks", "ParentTaskId");
            AddForeignKey("dbo.tblTasks", "ParentTaskId", "dbo.tblTasks", "TaskId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblTasks", "ParentTaskId", "dbo.tblTasks");
            DropIndex("dbo.tblTasks", new[] { "ParentTaskId" });
            DropColumn("dbo.tblTasks", "ParentTaskId");
        }
    }
}

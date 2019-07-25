namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingTaskPriorityFieldToNullableinTasksTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblTasks", "TaskPriority", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblTasks", "TaskPriority", c => c.Int(nullable: false));
        }
    }
}

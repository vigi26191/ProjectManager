namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingParentTaskIdToNullableFieldInTasksTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tblTasks", new[] { "ParentTaskId" });
            AlterColumn("dbo.tblTasks", "ParentTaskId", c => c.Int());
            CreateIndex("dbo.tblTasks", "ParentTaskId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tblTasks", new[] { "ParentTaskId" });
            AlterColumn("dbo.tblTasks", "ParentTaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblTasks", "ParentTaskId");
        }
    }
}

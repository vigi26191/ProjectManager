namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTasksTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblTasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false),
                        TaskPriority = c.Int(nullable: false),
                        IsParentTask = c.Boolean(),
                        TaskStartDate = c.DateTime(),
                        TaskEndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblTasks");
        }
    }
}

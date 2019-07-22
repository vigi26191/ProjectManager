namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsTaskCompleteFieldtoTaskTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblTasks", "IsTaskComplete", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblTasks", "IsTaskComplete");
        }
    }
}

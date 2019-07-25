namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingOneToManyRelationBetweenTaskProjectAndTaskUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblTasks", "ProjectId", c => c.Int(nullable: false));
            AddColumn("dbo.tblTasks", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblTasks", "ProjectId");
            CreateIndex("dbo.tblTasks", "UserId");
            AddForeignKey("dbo.tblTasks", "ProjectId", "dbo.tblProjects", "ProjectId");
            AddForeignKey("dbo.tblTasks", "UserId", "dbo.tblUsers", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblTasks", "UserId", "dbo.tblUsers");
            DropForeignKey("dbo.tblTasks", "ProjectId", "dbo.tblProjects");
            DropIndex("dbo.tblTasks", new[] { "UserId" });
            DropIndex("dbo.tblTasks", new[] { "ProjectId" });
            DropColumn("dbo.tblTasks", "UserId");
            DropColumn("dbo.tblTasks", "ProjectId");
        }
    }
}

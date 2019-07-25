namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRelationBetweenUserandTable : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.tblUsers", "ProjectId", "dbo.tblProjects");
            //DropIndex("dbo.tblUsers", new[] { "ProjectId" });
            //DropColumn("dbo.tblUsers", "ProjectId");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.tblUsers", "ProjectId", c => c.Int());
            //CreateIndex("dbo.tblUsers", "ProjectId");
            //AddForeignKey("dbo.tblUsers", "ProjectId", "dbo.tblProjects", "ProjectId");
        }
    }
}

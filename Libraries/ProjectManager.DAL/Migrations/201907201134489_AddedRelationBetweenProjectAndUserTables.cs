namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationBetweenProjectAndUserTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblUsers", "ProjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblUsers", "ProjectId");
            AddForeignKey("dbo.tblUsers", "ProjectId", "dbo.tblProjects", "ProjectId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblUsers", "ProjectId", "dbo.tblProjects");
            DropIndex("dbo.tblUsers", new[] { "ProjectId" });
            DropColumn("dbo.tblUsers", "ProjectId");
        }
    }
}

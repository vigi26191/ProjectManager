namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredRelationBetweenProjectAndUserTables : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tblUsers", new[] { "ProjectId" });
            AlterColumn("dbo.tblUsers", "ProjectId", c => c.Int());
            CreateIndex("dbo.tblUsers", "ProjectId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tblUsers", new[] { "ProjectId" });
            AlterColumn("dbo.tblUsers", "ProjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblUsers", "ProjectId");
        }
    }
}

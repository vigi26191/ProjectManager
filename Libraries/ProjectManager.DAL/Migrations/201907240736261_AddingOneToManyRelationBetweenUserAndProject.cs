namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingOneToManyRelationBetweenUserAndProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProjects", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblProjects", "UserId");
            AddForeignKey("dbo.tblProjects", "UserId", "dbo.tblUsers", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblProjects", "UserId", "dbo.tblUsers");
            DropIndex("dbo.tblProjects", new[] { "UserId" });
            DropColumn("dbo.tblProjects", "UserId");
        }
    }
}

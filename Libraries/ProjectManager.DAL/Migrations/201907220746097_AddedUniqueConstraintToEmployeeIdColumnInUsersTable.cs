namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUniqueConstraintToEmployeeIdColumnInUsersTable : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.tblUsers", "EmployeeId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.tblUsers", new[] { "EmployeeId" });
        }
    }
}

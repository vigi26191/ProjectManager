namespace ProjectManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsActiveCoumnToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblUsers", "IsActive", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblUsers", "IsActive");
        }
    }
}

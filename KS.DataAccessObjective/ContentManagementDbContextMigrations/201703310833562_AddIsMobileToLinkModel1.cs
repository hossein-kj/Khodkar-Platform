namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsMobileToLinkModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.Links", "IsMobile", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.Links", "IsMobile");
        }
    }
}

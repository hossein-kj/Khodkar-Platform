namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsMobileFromLinkModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("ContentManagement.Links", "IsMobile");
        }
        
        public override void Down()
        {
            AddColumn("ContentManagement.Links", "IsMobile", c => c.Boolean(nullable: false));
        }
    }
}

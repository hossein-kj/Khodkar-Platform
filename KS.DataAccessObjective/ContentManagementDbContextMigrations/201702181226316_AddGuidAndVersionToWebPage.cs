namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuidAndVersionToWebPage : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.WebPages", "Guid", c => c.String(maxLength: 32));
            AddColumn("ContentManagement.WebPages", "Version", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.WebPages", "Version");
            DropColumn("ContentManagement.WebPages", "Guid");
        }
    }
}

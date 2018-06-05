namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFrameWorkUrlToWebPageModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.WebPages", "FrameWorkUrl", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.WebPages", "FrameWorkUrl");
        }
    }
}

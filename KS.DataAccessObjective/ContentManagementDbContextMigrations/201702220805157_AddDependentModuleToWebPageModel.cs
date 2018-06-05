namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDependentModuleToWebPageModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.WebPages", "DependentModule", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.WebPages", "DependentModule");
        }
    }
}

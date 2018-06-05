namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameDependentModuleToWebPageModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.WebPages", "DependentModules", c => c.String(maxLength: 1024));
            DropColumn("ContentManagement.WebPages", "DependentModule");
        }
        
        public override void Down()
        {
            AddColumn("ContentManagement.WebPages", "DependentModule", c => c.String(maxLength: 1024));
            DropColumn("ContentManagement.WebPages", "DependentModules");
        }
    }
}

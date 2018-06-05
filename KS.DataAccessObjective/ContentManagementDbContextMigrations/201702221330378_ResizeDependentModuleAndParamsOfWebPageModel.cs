namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResizeDependentModuleAndParamsOfWebPageModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ContentManagement.WebPages", "DependentModules", c => c.String(maxLength: 2048));
            AlterColumn("ContentManagement.WebPages", "Params", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("ContentManagement.WebPages", "Params", c => c.String(maxLength: 255));
            AlterColumn("ContentManagement.WebPages", "DependentModules", c => c.String(maxLength: 1024));
        }
    }
}

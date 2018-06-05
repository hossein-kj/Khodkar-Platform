namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHaveScriptAndStyleToWebPageModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.WebPages", "HaveScript", c => c.Boolean(nullable: false));
            AddColumn("ContentManagement.WebPages", "HaveStyle", c => c.Boolean(nullable: false));
            DropColumn("ContentManagement.WebPages", "JavaScript");
            DropColumn("ContentManagement.WebPages", "Style");
        }
        
        public override void Down()
        {
            AddColumn("ContentManagement.WebPages", "Style", c => c.String());
            AddColumn("ContentManagement.WebPages", "JavaScript", c => c.String());
            DropColumn("ContentManagement.WebPages", "HaveStyle");
            DropColumn("ContentManagement.WebPages", "HaveScript");
        }
    }
}

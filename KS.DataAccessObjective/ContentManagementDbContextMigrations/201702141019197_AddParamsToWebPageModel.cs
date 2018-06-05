namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParamsToWebPageModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.WebPages", "Params", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.WebPages", "Params");
        }
    }
}

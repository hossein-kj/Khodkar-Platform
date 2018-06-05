namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSizeToFileAndFilePathModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.FilePaths", "Size", c => c.Single(nullable: false));
            AddColumn("ContentManagement.Files", "Size", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.Files", "Size");
            DropColumn("ContentManagement.FilePaths", "Size");
        }
    }
}

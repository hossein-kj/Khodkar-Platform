namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropublicInFilePathAndFileModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("ContentManagement.FilePaths", "Public");
            DropColumn("ContentManagement.Files", "Public");
        }
        
        public override void Down()
        {
            AddColumn("ContentManagement.Files", "Public", c => c.Boolean(nullable: false));
            AddColumn("ContentManagement.FilePaths", "Public", c => c.Boolean(nullable: false));
        }
    }
}

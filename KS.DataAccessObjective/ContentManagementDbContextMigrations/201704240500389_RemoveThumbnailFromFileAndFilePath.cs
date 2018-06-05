namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveThumbnailFromFileAndFilePath : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ContentManagement.Files", "ThumbnailId", "ContentManagement.FilePaths");
            DropForeignKey("ContentManagement.FilePaths", "ThumbnailId", "ContentManagement.FilePaths");
            DropIndex("ContentManagement.FilePaths", new[] { "ThumbnailId" });
            DropIndex("ContentManagement.Files", new[] { "ThumbnailId" });
            AddColumn("ContentManagement.FilePaths", "TypeCode", c => c.Int(nullable: false));
            AddColumn("ContentManagement.Files", "TypeCode", c => c.Int(nullable: false));
            DropColumn("ContentManagement.FilePaths", "Type");
            DropColumn("ContentManagement.FilePaths", "ThumbnailId");
            DropColumn("ContentManagement.Files", "Type");
            DropColumn("ContentManagement.Files", "ThumbnailId");
        }
        
        public override void Down()
        {
            AddColumn("ContentManagement.Files", "ThumbnailId", c => c.Int());
            AddColumn("ContentManagement.Files", "Type", c => c.Int(nullable: false));
            AddColumn("ContentManagement.FilePaths", "ThumbnailId", c => c.Int());
            AddColumn("ContentManagement.FilePaths", "Type", c => c.Int(nullable: false));
            DropColumn("ContentManagement.Files", "TypeCode");
            DropColumn("ContentManagement.FilePaths", "TypeCode");
            CreateIndex("ContentManagement.Files", "ThumbnailId");
            CreateIndex("ContentManagement.FilePaths", "ThumbnailId");
            AddForeignKey("ContentManagement.FilePaths", "ThumbnailId", "ContentManagement.FilePaths", "Id");
            AddForeignKey("ContentManagement.Files", "ThumbnailId", "ContentManagement.FilePaths", "Id");
        }
    }
}

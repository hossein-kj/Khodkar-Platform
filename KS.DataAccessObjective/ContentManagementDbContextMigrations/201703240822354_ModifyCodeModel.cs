namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCodeModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.Codes", "Description", c => c.String(maxLength: 256));
            AddColumn("ContentManagement.Codes", "Guid", c => c.String(maxLength: 32));
            AddColumn("ContentManagement.Codes", "Version", c => c.Int(nullable: false));
            CreateIndex("ContentManagement.Codes", "Guid", unique: true);
            DropColumn("ContentManagement.Codes", "Value");
        }
        
        public override void Down()
        {
            AddColumn("ContentManagement.Codes", "Value", c => c.String());
            DropIndex("ContentManagement.Codes", new[] { "Guid" });
            DropColumn("ContentManagement.Codes", "Version");
            DropColumn("ContentManagement.Codes", "Guid");
            DropColumn("ContentManagement.Codes", "Description");
        }
    }
}

namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuidIndexToWebPage : DbMigration
    {
        public override void Up()
        {
            CreateIndex("ContentManagement.WebPages", "Guid", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("ContentManagement.WebPages", new[] { "Guid" });
        }
    }
}

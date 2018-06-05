namespace KS.ObjectiveDataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEntityId : DbMigration
    {
        public override void Up()
        {
            DropColumn("ContentManagement.EntityFilePaths", "EntityId");
            DropColumn("ContentManagement.EntityFiles", "EntityId");
        }
        
        public override void Down()
        {
            AddColumn("ContentManagement.EntityFiles", "EntityId", c => c.Int(nullable: false));
            AddColumn("ContentManagement.EntityFilePaths", "EntityId", c => c.Int(nullable: false));
        }
    }
}

namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuidAndVersionAndDescriptionToMasterDataKeyValueModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.MasterDataKeyValues", "Description", c => c.String(maxLength: 256));
            AddColumn("ContentManagement.MasterDataKeyValues", "Guid", c => c.String(maxLength: 32));
            AddColumn("ContentManagement.MasterDataKeyValues", "Version", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.MasterDataKeyValues", "Version");
            DropColumn("ContentManagement.MasterDataKeyValues", "Guid");
            DropColumn("ContentManagement.MasterDataKeyValues", "Description");
        }
    }
}

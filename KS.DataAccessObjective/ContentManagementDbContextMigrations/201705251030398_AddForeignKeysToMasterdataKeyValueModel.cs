namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeysToMasterdataKeyValueModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.MasterDataKeyValues", "ForeignCode", c => c.String(maxLength: 512));
            AddColumn("ContentManagement.MasterDataKeyValues", "ForeignUrl", c => c.String(maxLength: 1024));
            AddColumn("ContentManagement.MasterDataKeyValues", "ForeignKey1", c => c.Int());
            AddColumn("ContentManagement.MasterDataKeyValues", "ForeignKey2", c => c.Int());
            AddColumn("ContentManagement.MasterDataKeyValues", "ForeignKey3", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.MasterDataKeyValues", "ForeignKey3");
            DropColumn("ContentManagement.MasterDataKeyValues", "ForeignKey2");
            DropColumn("ContentManagement.MasterDataKeyValues", "ForeignKey1");
            DropColumn("ContentManagement.MasterDataKeyValues", "ForeignUrl");
            DropColumn("ContentManagement.MasterDataKeyValues", "ForeignCode");
        }
    }
}

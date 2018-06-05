namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKeyValueToMasterDataKeyValueModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.MasterDataKeyValues", "Key", c => c.Int());
            AddColumn("ContentManagement.MasterDataKeyValues", "Value", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.MasterDataKeyValues", "Value");
            DropColumn("ContentManagement.MasterDataKeyValues", "Key");
        }
    }
}

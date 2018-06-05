namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveThirdcodeFromMasterDataKeyValueModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("ContentManagement.MasterDataKeyValues", "Thirdcode");
        }
        
        public override void Down()
        {
            AddColumn("ContentManagement.MasterDataKeyValues", "Thirdcode", c => c.String(maxLength: 512));
        }
    }
}

namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddThirdcodeToMasterDataKeyValueModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.MasterDataKeyValues", "Thirdcode", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.MasterDataKeyValues", "Thirdcode");
        }
    }
}

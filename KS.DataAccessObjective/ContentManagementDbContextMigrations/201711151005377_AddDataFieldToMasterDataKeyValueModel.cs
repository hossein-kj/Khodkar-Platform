namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataFieldToMasterDataKeyValueModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.MasterDataKeyValues", "Data", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.MasterDataKeyValues", "Data");
        }
    }
}

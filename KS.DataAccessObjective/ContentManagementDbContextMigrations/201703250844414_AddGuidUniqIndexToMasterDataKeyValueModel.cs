namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuidUniqIndexToMasterDataKeyValueModel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("ContentManagement.MasterDataKeyValues", "Guid", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("ContentManagement.MasterDataKeyValues", new[] { "Guid" });
        }
    }
}

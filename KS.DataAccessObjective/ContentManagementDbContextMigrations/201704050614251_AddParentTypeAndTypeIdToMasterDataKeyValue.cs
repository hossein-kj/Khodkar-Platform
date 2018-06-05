namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParentTypeAndTypeIdToMasterDataKeyValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.MasterDataKeyValues", "ParentTypeId", c => c.Int(nullable: false));
            AddColumn("ContentManagement.MasterDataKeyValues", "IsType", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.MasterDataKeyValues", "IsType");
            DropColumn("ContentManagement.MasterDataKeyValues", "ParentTypeId");
        }
    }
}

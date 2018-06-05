namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullAbleParentTypeId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ContentManagement.MasterDataKeyValues", "ParentTypeId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("ContentManagement.MasterDataKeyValues", "ParentTypeId", c => c.Int(nullable: false));
        }
    }
}

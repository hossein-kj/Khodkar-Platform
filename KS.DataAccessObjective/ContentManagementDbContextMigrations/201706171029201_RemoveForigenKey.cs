namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveForigenKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.MasterDataKeyValues", "SecondCode", c => c.String(maxLength: 512));
            AddColumn("ContentManagement.MasterDataKeyValues", "SecondUrl", c => c.String(maxLength: 1024));
            DropColumn("ContentManagement.MasterDataKeyValues", "ForeignCode");
            DropColumn("ContentManagement.MasterDataKeyValues", "ForeignUrl");
        }
        
        public override void Down()
        {
            AddColumn("ContentManagement.MasterDataKeyValues", "ForeignUrl", c => c.String(maxLength: 1024));
            AddColumn("ContentManagement.MasterDataKeyValues", "ForeignCode", c => c.String(maxLength: 512));
            DropColumn("ContentManagement.MasterDataKeyValues", "SecondUrl");
            DropColumn("ContentManagement.MasterDataKeyValues", "SecondCode");
        }
    }
}

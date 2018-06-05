namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUrlToPathOrUrlInMasterDataKeyValueModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn("ContentManagement.MasterDataKeyValues", "Url", "PathOrUrl");
            RenameColumn("ContentManagement.MasterDataKeyValues", "SecondUrl", "SecondPathOrUrl");
            //AddColumn("ContentManagement.MasterDataKeyValues", "PathOrUrl", c => c.String(maxLength: 1024));
            //AddColumn("ContentManagement.MasterDataKeyValues", "SecondPathOrUrl", c => c.String(maxLength: 1024));
            //DropColumn("ContentManagement.MasterDataKeyValues", "Url");
            //DropColumn("ContentManagement.MasterDataKeyValues", "SecondUrl");
        }
        
        public override void Down()
        {
            RenameColumn("ContentManagement.MasterDataKeyValues", "PathOrUrl", "Url");
            RenameColumn("ContentManagement.MasterDataKeyValues", "SecondPathOrUrl", "SecondUrl");
            //AddColumn("ContentManagement.MasterDataKeyValues", "SecondUrl", c => c.String(maxLength: 1024));
            //AddColumn("ContentManagement.MasterDataKeyValues", "Url", c => c.String(maxLength: 1024));
            //DropColumn("ContentManagement.MasterDataKeyValues", "SecondPathOrUrl");
            //DropColumn("ContentManagement.MasterDataKeyValues", "PathOrUrl");
        }
    }
}

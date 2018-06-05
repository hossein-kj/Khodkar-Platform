namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionToMasterDataLocalKeyValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.MasterDataLocalKeyValues", "Description", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("ContentManagement.MasterDataLocalKeyValues", "Description");
        }
    }
}

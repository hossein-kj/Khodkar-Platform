namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyActionInLinkModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ContentManagement.Links", "Action", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("ContentManagement.Links", "Action", c => c.String());
        }
    }
}

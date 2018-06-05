namespace KS.DataAccess.SecurityContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsGroupFromRoleModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("Security.AspNetRoles", "RoleId");
            DropColumn("Security.AspNetRoles", "IsGroup");
        }
        
        public override void Down()
        {
            AddColumn("Security.AspNetRoles", "IsGroup", c => c.Boolean(nullable: false));
            AddColumn("Security.AspNetRoles", "RoleId", c => c.Int());
        }
    }
}

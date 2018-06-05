namespace KS.DataAccess.SecurityContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRoleGroupIdToRoleIdInLocalRoleModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "Security.AspNetLocalRole", name: "RoleGroupId", newName: "RoleId");
            RenameIndex(table: "Security.AspNetLocalRole", name: "IX_RoleGroupId", newName: "IX_RoleId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "Security.AspNetLocalRole", name: "IX_RoleId", newName: "IX_RoleGroupId");
            RenameColumn(table: "Security.AspNetLocalRole", name: "RoleId", newName: "RoleGroupId");
        }
    }
}

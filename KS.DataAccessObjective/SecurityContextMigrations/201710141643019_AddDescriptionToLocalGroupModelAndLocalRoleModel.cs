namespace KS.DataAccess.SecurityContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionToLocalGroupModelAndLocalRoleModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("Security.AspNetLocalRole", "Description", c => c.String(maxLength: 256));
            AddColumn("Security.AspNetLocalGroup", "Description", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("Security.AspNetLocalGroup", "Description");
            DropColumn("Security.AspNetLocalRole", "Description");
        }
    }
}

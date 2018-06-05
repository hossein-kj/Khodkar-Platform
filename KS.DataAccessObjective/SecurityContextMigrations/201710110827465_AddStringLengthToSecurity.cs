namespace KS.DataAccess.SecurityContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStringLengthToSecurity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Security.AspNetGroups", "Name", c => c.String(maxLength: 256));
            AlterColumn("Security.AspNetGroups", "Description", c => c.String(maxLength: 256));
            AlterColumn("Security.AspNetGroups", "CreateLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.AspNetGroups", "ModifieLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.AspNetGroups", "AccessLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.AspNetRoles", "Description", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("Security.AspNetRoles", "Description", c => c.String());
            AlterColumn("Security.AspNetGroups", "AccessLocalDateTime", c => c.String());
            AlterColumn("Security.AspNetGroups", "ModifieLocalDateTime", c => c.String());
            AlterColumn("Security.AspNetGroups", "CreateLocalDateTime", c => c.String());
            AlterColumn("Security.AspNetGroups", "Description", c => c.String());
            AlterColumn("Security.AspNetGroups", "Name", c => c.String());
        }
    }
}

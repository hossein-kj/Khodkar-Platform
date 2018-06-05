namespace KS.DataAccess.SecurityContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class To24HoursFormat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Security.AspNetLocalRole", "CreateLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.AspNetLocalRole", "ModifieLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.AspNetLocalRole", "AccessLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.AspNetRoles", "CreateLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.AspNetRoles", "ModifieLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.AspNetRoles", "AccessLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.AspNetUsers", "LocalBirthDate", c => c.String(maxLength: 19));
            AlterColumn("Security.AspNetUsers", "CreateLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.AspNetUsers", "ModifieLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.AspNetUsers", "AccessLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.ApplicationQueryAuthrizes", "CreateLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.ApplicationQueryAuthrizes", "ModifieLocalDateTime", c => c.String(maxLength: 19));
            AlterColumn("Security.ApplicationQueryAuthrizes", "AccessLocalDateTime", c => c.String(maxLength: 19));
        }
        
        public override void Down()
        {
            AlterColumn("Security.ApplicationQueryAuthrizes", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("Security.ApplicationQueryAuthrizes", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("Security.ApplicationQueryAuthrizes", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("Security.AspNetUsers", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("Security.AspNetUsers", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("Security.AspNetUsers", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("Security.AspNetUsers", "LocalBirthDate", c => c.String(maxLength: 22));
            AlterColumn("Security.AspNetRoles", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("Security.AspNetRoles", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("Security.AspNetRoles", "CreateLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("Security.AspNetLocalRole", "AccessLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("Security.AspNetLocalRole", "ModifieLocalDateTime", c => c.String(maxLength: 22));
            AlterColumn("Security.AspNetLocalRole", "CreateLocalDateTime", c => c.String(maxLength: 22));
        }
    }
}

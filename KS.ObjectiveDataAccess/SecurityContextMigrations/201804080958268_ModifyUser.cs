namespace KS.ObjectiveDataAccess.SecurityContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUser : DbMigration
    {
        public override void Up()
        {
            DropIndex("Security.AspNetUsers", new[] { "UserProfileId" });
            DropColumn("Security.AspNetUsers", "UserProfileId");
        }
        
        public override void Down()
        {
            AddColumn("Security.AspNetUsers", "UserProfileId", c => c.Int(nullable: false));
            CreateIndex("Security.AspNetUsers", "UserProfileId", unique: true);
        }
    }
}

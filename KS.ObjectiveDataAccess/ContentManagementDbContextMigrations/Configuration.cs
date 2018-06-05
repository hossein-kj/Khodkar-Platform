namespace KS.ObjectiveDataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KS.ObjectiveDataAccess.Contexts.ContentManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"ContentManagementDbContextMigrations";
        }

        protected override void Seed(KS.ObjectiveDataAccess.Contexts.ContentManagementContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}

using System.Data.Entity;
using KS.Model.Security;

namespace KS.DataAccess.Contexts.Config
{
    public static class SqlSecurityModelConfiguration
    {
        public static void Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Security");
            modelBuilder.Entity<ApplicationLocalRole>().ToTable("AspNetLocalRole");
            modelBuilder.Entity<ApplicationGroup>().ToTable("AspNetGroups");
            modelBuilder.Entity<ApplicationUserGroup>().ToTable("AspNetUserGroups");
            modelBuilder.Entity<ApplicationGroupRole>().ToTable("AspNetGroupRoles");
            modelBuilder.Entity<ApplicationLocalGroup>().ToTable("AspNetLocalGroup");
        }
    }
}
using System.Data.Entity;
using KS.Model.Security;

namespace KS.DataAccess.Contexts.Config
{
    public static class OracleSecurityModelConfiguration
    {
        public static void Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SECURITY");
            //modelBuilder.Entity<ApplicationGroup>().ToTable("ASPNETGROUPS");
            //modelBuilder.Entity<ApplicationUserGroup>().ToTable("ASPNETUSERGROUPS");
            //modelBuilder.Entity<ApplicationGroupRole>().ToTable("ASPNETGROUPROLES");
            modelBuilder.Entity<ApplicationRole>().ToTable("ASPNETROLES");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("ASPNETUSERCLAIMS");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("ASPNETUSERLOGINS");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("ASPNETUSERROLES");
            modelBuilder.Entity<ApplicationUser>().ToTable("ASPNETUSERS");
            //modelBuilder.Entity<ApplicationTree>().ToTable("ASPNETTREES");
        }
    }
}
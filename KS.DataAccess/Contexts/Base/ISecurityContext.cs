using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using KS.Core.Model.ContentManagement;
using KS.Model.Base;
using KS.Model.Security;

namespace KS.DataAccess.Contexts.Base
{
    public interface ISecurityContext
    {
        DbSet<ApplicationGroup> Groups { get; set; }
        DbSet<ApplicationLocalGroup> ApplicationLocalGroups { get; set; }
        DbSet<ApplicationLocalRole> ApplicationLocalRoles { get; set; }
        DbSet<ApplicationQueryAuthrize> ApplicationQueryAuthrizes { get; set; }
        DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }
        DbSet<ApplicationGroupRole> ApplicationGroupRoles { get; set; }
        DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
        IDbSet<ApplicationRole> Roles { get; set; }
        Task<List<T>> GetTreeByAllOffspringAsync<T>(string entityTable, string where) where T : ITree<T>;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
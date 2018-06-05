 /*khodkar c# comment    namespace KS.Dynamic.DataAccess.Contexts
{
   using System.Data.Entity.Migrations;
   using System.Data.Entity;
   using KS.Dynamic.Model;
   using KS.Core.Log.Base;
   using KS.Core.Log.Elmah.Base;
   using System.Threading.Tasks;
   using KS.Dynamic.DataAccess.Contexts.Config;
   using KS.Dynamic.DataAccess.Contexts.Base;
   
    public interface IDynamicContext
    {
        DbSet<DynamicTestModel> DynamicTestModels { get; set; }
        Task<int> SaveChangesAsync();
    }
    
    [DbConfigurationType(typeof(SqlDynamicModelHistoryConfiguration))]
    public sealed class DynamicContext 
        : BaseContext<DynamicContext>, IDynamicContext
    {
        
        public DynamicContext(IErrorLogManager errorLogManager
            , IActionLogManager actionLogManager) : base(errorLogManager, actionLogManager)
        {
        }
        
         public DynamicContext()
        {

        }

        public DbSet<DynamicTestModel> DynamicTestModels { get; set; }
        
         protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             SqlDynamicModelConfiguration.Config(modelBuilder);
        }
    }
}
    khodkar c# comment*/ 
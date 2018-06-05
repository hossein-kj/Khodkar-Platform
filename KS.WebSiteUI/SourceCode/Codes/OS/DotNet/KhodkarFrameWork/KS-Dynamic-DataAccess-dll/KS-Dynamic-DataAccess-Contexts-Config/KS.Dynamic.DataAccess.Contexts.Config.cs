 /*khodkar c# comment  namespace KS.Dynamic.DataAccess.Contexts.Config
{
    using KS.Core.EntityFramework.Config;
    using System.Data.Entity;
    using KS.Dynamic.Model;
    
    public static class SqlDynamicModelConfiguration
    {
        public static void Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Dynamic");
        }
    }
    
    public sealed class SqlDynamicModelHistoryConfiguration : DbConfiguration
    {
        public SqlDynamicModelHistoryConfiguration()
        {
            this.SetHistoryContext("System.Data.SqlClient",
                (connection, defaultSchema) => new HistoryContextConfiguration(connection, "Dynamic","__MigrationHistory")); 
        }
    }
}  khodkar c# comment*/ 
using System.Data.Entity;
using KS.Core.EntityFramework.Config;
using KS.Core.EntityFramework.Config.Oracle;

namespace KS.DataAccess.Config
{
    public sealed class OracleSecurityModelConfiguration : DbConfiguration
    {
        public OracleSecurityModelConfiguration()
        {
            this.SetHistoryContext("Oracle.ManagedDataAccess.Client",
                (connection, defaultSchema) => new HistoryContextConfiguration(connection, "SECURITY", "MIGRATIONHISTORY")); 
        }
    }
}


using System.Data.Entity;
using KS.Core.EntityFramework.Config;

namespace KS.ObjectiveDataAccess.Config
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

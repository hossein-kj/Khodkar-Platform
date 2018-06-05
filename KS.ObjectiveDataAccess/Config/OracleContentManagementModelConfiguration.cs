
using System.Data.Entity;
using KS.Core.EntityFramework.Config;

namespace KS.ObjectiveDataAccess.Config
{
    public sealed class OracleContentManagementModelConfiguration : DbConfiguration
    {
        public OracleContentManagementModelConfiguration()
        {
            this.SetHistoryContext("Oracle.ManagedDataAccess.Client",
                (connection, defaultSchema) => new HistoryContextConfiguration(connection, "CONTENTMANAGEMENT", "MIGRATIONHISTORY"));
        }
    }
}

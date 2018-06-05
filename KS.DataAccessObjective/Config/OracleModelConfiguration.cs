using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Core.Data;
using KS.DataAccess.Contexts;

namespace KS.DataAccess.Config
{
    public sealed class OracleModelConfiguration : DbConfiguration
    {
        public OracleModelConfiguration()
        {
            this.SetHistoryContext("Oracle.ManagedDataAccess.Client",
                (connection, defaultSchema) => new OracleHistoryContext(connection, defaultSchema)); 
        }
    }
}

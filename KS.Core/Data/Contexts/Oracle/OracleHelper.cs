using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using KS.Core.GlobalVarioable;
using Oracle.ManagedDataAccess.Client;
using OracleCommand = Oracle.ManagedDataAccess.Client.OracleCommand;
using OracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;
using OracleDataReader = Oracle.ManagedDataAccess.Client.OracleDataReader;
using KS.Core.Data.Contexts.Oracle.Base;

namespace KS.Core.Data.Contexts.Oracle
{
    public sealed class OracleHelper: BaseOracleHelper,IOracleHelper
    {
       
    }
}

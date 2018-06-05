using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace KS.Core.SignalR.Config
{
    class CrossTabSharedConnectionFactory : IConnectionIdFactory
    {
        public string CreateConnectionId(IRequest request)
        {
            if (request.Cookies[“srconnectionid”] != null)
            {
                return request.Cookies[“srconnectionid”];
            }
            return Guid.NewGuid().ToString();
        }
    }
}

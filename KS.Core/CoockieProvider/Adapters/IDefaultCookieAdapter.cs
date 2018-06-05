using System.Collections.Generic;
using KS.Core.Model;
using KS.Core.Model.Core;
using Newtonsoft.Json.Linq;

namespace KS.Core.CoockieProvider.Adapters
{
    public interface IDefaultCookieAdapter
    {
        JObject GetAsJson(string key);
        string Get(string key);
        List<KeyValue> GetAll();
    }
}

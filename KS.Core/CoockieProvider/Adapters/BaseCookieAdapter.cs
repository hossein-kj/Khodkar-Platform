using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using KS.Core.Model;
using KS.Core.Model.Core;

namespace KS.Core.CoockieProvider.Adapters
{
    public class BaseCookieAdapter
    {
        public virtual string Get(string key)
        {
            if (HttpContext.Current?.Request.Cookies[key] != null)
                return (HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[key].Value)); 
            return null;
        }
        public virtual JObject GetAsJson(string key)
        {
            if (HttpContext.Current?.Request.Cookies[key] != null)
                return JObject.Parse((HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[key].Value)));
            return null;
        }

        public virtual List<KeyValue> GetAll()
        {
            if(HttpContext.Current != null)
            return (from string key in HttpContext.Current.Request.Cookies
                select new KeyValue()
                {
                    Key = key, Value = Get(key)
                }).ToList();
            return new List<KeyValue>();
        }

    }
}

using System;
using System.Web;
using KS.Core.Utility.Adapters;
using KS.Core.GlobalVarioable;

namespace KS.Core.Utility
{
    public static class Helper
    {
        public static void SetPropertyByName(Type type,string name,object value,object instance=null)
        {
            var propertyInfo = type.GetProperty(name);
            if (propertyInfo != null) propertyInfo.SetValue(value, instance);
        }

        public static object GetPropertyValueByName(object src, string propName)
        {
            var propertyInfo = src.GetType().GetProperty(propName);
            return propertyInfo != null ? Convert.ToString(propertyInfo.GetValue(src, null)) : null;
        }

        public static string ProperPath(string path)
        {
            if (path[0] != '~')
                path = path[0] == '/' ? "~" + path : "~/" + path;
            return path.ToLower();
        }

        public static string UrlEncode(string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        public static string RootUrl => "/";
    }
}

using System;
using KS.Core.GlobalVarioable;
using KS.Core.Utility;
using System.Collections.Generic;
using System.Linq;
using KS.Core.UI.Setting;
using KS.Core.Data.Contexts.Base;
using KS.Core.Model.Core;
using KS.Core.Utility.Adapters;
using Newtonsoft.Json;

namespace KS.Core.Localization.Adapters
{
    public abstract class BaseLanguageAdapter
    {
        protected readonly IDataBaseContextManager DataBaseContextManager;
        protected BaseLanguageAdapter(IDataBaseContextManager dataBaseContextManager)
        {
            DataBaseContextManager = dataBaseContextManager;
        }
        public virtual string ApplyLanguageAndMobileSignToAjaxRequestAsync(string languageOrUrl)
        {
            //if (languageOrUrl == null) return Setting.Language;
            //var fullUrl = (languageOrUrl[0] == Helper.RootUrl.ToCharArray()[0] ? languageOrUrl : Helper.RootUrl + languageOrUrl);
            //if (fullUrl.Length > 1)
            //    if (fullUrl[fullUrl.Length - 1] == Helper.RootUrl.ToCharArray()[0])
            //        fullUrl = fullUrl.Remove(fullUrl.Length - 1);
            ////var langAndCulter = await GetLanguagesAsync();
            //if (fullUrl == Helper.RootUrl)
            //    return Setting.Language + fullUrl;

            //var urlParts = fullUrl.Split(Helper.RootUrl.ToCharArray());
            //if (urlParts.Length == 0)
            //    return fullUrl;
            //var lang = Config.LanguageAndCultures.Where(lg => lg == urlParts[1]).ToList();
            //if (lang.Count != 0) return fullUrl;

            //return Helper.RootUrl + Setting.Language + fullUrl;




            if (languageOrUrl == null || languageOrUrl.Trim() == "")
            {
                return Settings.IsMobileMode ? Settings.Language + Config.MobileSign : Settings.Language;
            }
            else if (!Settings.IsMobileMode ||
                (Settings.IsMobileMode && (languageOrUrl.Contains(Config.MobileSign) || (languageOrUrl + "/").Contains(Config.MobileSign))))
            {
                var fullUrl = (languageOrUrl[0] == Helper.RootUrl.ToCharArray()[0] ? languageOrUrl : Helper.RootUrl + languageOrUrl)
                    .ToLower();
                if (fullUrl.Length > 1)
                    if (fullUrl[fullUrl.Length - 1] == Helper.RootUrl.ToCharArray()[0])
                        fullUrl = fullUrl.Remove(fullUrl.Length - 1);

                if (fullUrl == Helper.RootUrl)
                    return Settings.Language + fullUrl;

                var urlParts = fullUrl.Split(Helper.RootUrl.ToCharArray()[0]);


                var lang = Config.LanguageAndCultures.Where(lg => lg == urlParts[1]).ToList();
                if (lang.Count != 0) return fullUrl;

                return Helper.RootUrl + Settings.Language + fullUrl;

            }
            else
            {
                //if(Setting.IsMobileMode && !languageOrUrl.Contains(Config.MobileSign))

                var fullUrl = (languageOrUrl[0] == Helper.RootUrl.ToCharArray()[0]
                    ? languageOrUrl
                    : Helper.RootUrl + languageOrUrl).ToLower();

                if (fullUrl.Length > 1)
                    if (fullUrl[fullUrl.Length - 1] == Helper.RootUrl.ToCharArray()[0])
                        fullUrl = fullUrl.Remove(fullUrl.Length - 1);

                if (fullUrl == Helper.RootUrl)
                    return Settings.Language + Config.MobileSign;

                var urlParts = fullUrl.Split(Helper.RootUrl.ToCharArray()[0]);

                var lang = Config.LanguageAndCultures.Where(lg => lg == urlParts[1]).ToList();
                return lang.Count != 0 ?
                    fullUrl.Insert(fullUrl.IndexOf(urlParts[1], StringComparison.OrdinalIgnoreCase) + urlParts[1].Length, Config.MobileSign).Replace("//", "/")
                    : (Helper.RootUrl + Settings.Language + Config.MobileSign + fullUrl).Replace("//", "/");
            }
        }

        public virtual List<string> ApplyLanguageAndMobileSignToRequestAsync(List<string> parameter = null)
        {
            //if (parameter == null) return new List<string>() { Setting.Language };
            //var language = parameter.FirstOrDefault();
            //var lang = Config.LanguageAndCultures.Where(lg => lg == language).ToList();
            //if (lang.Count != 0) return parameter;
            //parameter.Insert(0, Setting.Language);
            //return parameter;


            if (parameter == null || parameter.Count == 0) return new List<string>() { Settings.Language };
            else if (!Settings.IsMobileMode ||
              (Settings.IsMobileMode && parameter.Contains(Config.MobileSign.Replace("/", ""))))
            {
                var language = parameter.First().ToLower();
                var lang = Config.LanguageAndCultures.Where(lg => lg == language).ToList();
                if (lang.Count != 0) return parameter;
                parameter.Insert(0, Settings.Language);
                return parameter;
            }
            else
            {
                //(Setting.IsMobileMode && !parameter.Contains(Config.MobileSign))
                var language = parameter.First().ToLower();
                var lang = Config.LanguageAndCultures.Where(lg => lg == language).ToList();

                if (lang.Count != 0)
                {
                    parameter.Insert(1, Config.MobileSign.Replace("/", ""));
                    return parameter;
                }
                parameter.Insert(0, Settings.Language);
                parameter.Insert(1, Config.MobileSign.Replace("/", ""));
                return parameter;
            }

        }

        public virtual KeyValue GetMasterDataKeyValueTranslate(int typeId, string code, string language)
        {
            return DataBaseContextManager.GetMasterDataLocalKeyValue(typeId, code, language);
        }

        public virtual string GetText(TextKey code)
        {
            return GetText(code.ToString());
        }

        public virtual string GetText(string code)
        {
            return GetMasterDataKeyValueTranslate((int)EntityIdentity.Texts, code, Settings.Language).Key;
        }

        public virtual string ToAsErrorMessage(ExceptionKey code = ExceptionKey.None, string message = null)
        {
            return ToAsErrorMessage(code == ExceptionKey.None ? null : code.ToString(), message);
        }

        public virtual string ToAsErrorMessage(string code, string message = null)
        {
            if (message == null)
                return JsonConvert.SerializeObject(new { asError = GetMasterDataKeyValueTranslate((int)EntityIdentity.KhodkarException, code, Settings.Language).Key }, Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            //"{\"asError\":\"" + GetMasterDataKeyValueTranslate((int)EntityIdentity.KhodkarException, code, Settings.Language).Key + "\"}";
            else if (code != null)
                return JsonConvert.SerializeObject(new { asError = string.Format(GetMasterDataKeyValueTranslate((int)EntityIdentity.KhodkarException, code, Settings.Language).Key, message) }, Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            //"{\"asError\":\"" + string.Format(GetMasterDataKeyValueTranslate((int)EntityIdentity.KhodkarException, code, Settings.Language).Key, message) + "\"}";
            else return message.IndexOf("asError", StringComparison.Ordinal) < 0 ?
            JsonConvert.SerializeObject(new { asError = message }, Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                : message; //"{\"asError\":\"" + message + "\"}";
        }

        public virtual string ToLocalDateTime(DateTime utcMiladyDateTime)
        {

            var tzi = TimeZoneInfo.FindSystemTimeZoneById(Config.LocalTimeZoneId);

            var miladyDateTime = utcMiladyDateTime.Add(tzi.GetUtcOffset(utcMiladyDateTime));




            var calendar = DependencyProvider.DependencyManager.Get<IDefaultCalendar>();

            var todayDate = calendar.GetYear(miladyDateTime) + "/" + $"{calendar.GetMonth(miladyDateTime):d2}"
                            + "/" + $"{calendar.GetDayOfMonth(miladyDateTime):d2}" + " " + miladyDateTime.ToString("HH:mm:ss");
            return todayDate;
        }

        public virtual string ToLocalDateTime<T>(DateTime utcMiladyDateTime) where T : ICalendar
        {
            var tzi = TimeZoneInfo.FindSystemTimeZoneById(Config.LocalTimeZoneId);
            var miladyDateTime = utcMiladyDateTime.Add(tzi.GetUtcOffset(utcMiladyDateTime));



            var calendar = DependencyProvider.DependencyManager.Get<T>();
            var todayDate = calendar.GetYear(miladyDateTime) + "/" + $"{calendar.GetMonth(miladyDateTime):d2}"
                            + "/" + $"{calendar.GetDayOfMonth(miladyDateTime):d2}" + " " + miladyDateTime.ToString("HH:mm:ss");
            return todayDate;
        }
    }
}

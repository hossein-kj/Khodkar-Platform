
using System;
using KS.Core.GlobalVarioable;
using KS.Core.Localization.Adapters;
using System.Collections.Generic;
using KS.Core.Model.Core;
using KS.Core.Utility.Adapters;

namespace KS.Core.Localization
{
    public static class LanguageManager
    {
        private static ILanguageAdapter Adapter =>  DependencyProvider.DependencyManager.Get<ILanguageAdapter>();

        public static string ToLocalDateTime(DateTime utcMiladyDateTime)
        {
            return Adapter.ToLocalDateTime(utcMiladyDateTime);
        }

        public static string ToLocalDateTime<T>(DateTime utcMiladyDateTime) where T : ICalendar
        {
            return Adapter.ToLocalDateTime<T>(utcMiladyDateTime);
        }
        public static string ApplyLanguageAndMobileSignToAjaxRequestAsync(string languageOrUrl)
        {
            return Adapter.ApplyLanguageAndMobileSignToAjaxRequestAsync(languageOrUrl);
        }

        public static List<string> ApplyLanguageAndMobileSignToRequestAsync(List<string> parameter = null)
        {
            return Adapter.ApplyLanguageAndMobileSignToRequestAsync(parameter);
        }

        public static KeyValue GetMasterDataKeyValueTranslate(int typeId, string code, string language)
        {
            return Adapter.GetMasterDataKeyValueTranslate(typeId, code, language);
        }

        public static string GetText(TextKey code)
        {
            return Adapter.GetText(code);
        }

        public static string GetText(string code)
        {
            return Adapter.GetText(code);
        }


        public static string GetException(ExceptionKey code)
        {
            return Adapter.GetException(code);
        }

        public static string GetException(string code)
        {
            return Adapter.GetException(code);
        }

        public static string ToAsErrorMessage(ExceptionKey code = ExceptionKey.None, string message = null)
        {
            return Adapter.ToAsErrorMessage(code, message);
        }

        public static string ToAsErrorMessage(string code, string message = null)
        {
            return Adapter.ToAsErrorMessage(code, message);
        }
    }
}

using System;
using KS.Core.GlobalVarioable;
using System.Collections.Generic;
using KS.Core.Model.Core;
using KS.Core.Utility.Adapters;

namespace KS.Core.Localization.Adapters
{
    public interface ILanguageAdapter
    {
        string ApplyLanguageAndMobileSignToAjaxRequestAsync(string languageOrUrl);
        List<string> ApplyLanguageAndMobileSignToRequestAsync(List<string> parameter = null);

        KeyValue GetMasterDataKeyValueTranslate(int typeId, string code, string language);

        string GetText(TextKey code);
        string GetText(string code);

        string ToAsErrorMessage(ExceptionKey code = ExceptionKey.None, string message = null);

        string ToAsErrorMessage(string code, string message = null);
        string ToLocalDateTime(DateTime utcMiladyDateTime);
        string ToLocalDateTime<T>(DateTime utcMiladyDateTime) where T : ICalendar;
    }
}
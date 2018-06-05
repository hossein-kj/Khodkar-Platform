using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace KS.Core.GlobalVarioable
{
    public static class Config
    {
        //public static double LocalTimeZone
        //   => Convert.ToDouble(WebConfigManager.GetSettingValue("LocalTimeZone"), new NumberFormatInfo() { NumberDecimalSeparator = "." });
        public static string LocalTimeZoneId { get; set; }
        public static string DefaultsLanguage { get; set; }
        public static List<string> LanguageAndCultures { get; set; }

        public static string PagesSourceCodePath { get; set; }
        public static string ServicesSourceCodePath { get; set; }
        public static string ResourcesSourceCodePath { get; set; }
        public static string ResourcesDistPath { get; set; }
        public static string UrlDelimeter { get; set; }
        public static string ScriptDebugPagesPath { get; set; }
        public static string ScriptDistPagesPath { get; set; }
        public static string ScriptDistPath { get; set; }
        public static string ScriptDebugPath { get; set; }
        public static string StyleDebugPagesPath { get; set; }
        public static string StyleDistPagesPath { get; set; }
        public static string ThumbnailPath { get; set; }
        public static string StyleDebugPath { get; set; }
        public static string StyleDistPath { get; set; }
        public static string PagesPath { get; set; }
        public static string LogOffSign { get; set; }
        public static string QueryStringSign { get; set; }
        public static string MobileSign { get; set; }
        public static bool MobileFallBack { get; set; }
        public static int LoginExpireTimeSpanInHours { get; set; }
        public static string LanguageAndCultureCoockie { get; set; }
        public static string IsDebugModeCoockie { get; set; }
        public static string IsMobileModeCoockie { get; set; }
        public static string IsAuthenticatedCoockie { get; set; }
        public static string DebugIdSign { get; set; }
        public static string BrowserCodeDependencyEngineSourcePath { get; set; }
        public static string LogPath { get; set; }
        public static string LoginUrl { get; set; }
        public static int LogBackgroundJobIntervalsInMilliseconds { get; set; }
        public static string ErrrorPagesBaseUrl { get; set; }
        public static string SourceCodeDeletedPath { get; set; }
        public static string DefaultsGetWebPagesServiceUrl { get; set; }
        public static int AspectCacheSlidingExpirationTimeInMinutes { get; set; }
        public static int GroupCacheSlidingExpirationTimeInMinutes { get; set; }
    public static bool EnableActionLog { get; set; }
    }
}

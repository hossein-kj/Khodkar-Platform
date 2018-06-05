
using KS.Core.UI.Setting.Adapters;

namespace KS.Core.UI.Setting
{
    public static class Settings
    {
        private static IDefaultSettingsAdapter Adapter => DependencyProvider.DependencyManager.Get<IDefaultSettingsAdapter>();
        public static string Language => Adapter.Language;

        public static bool IsDebugMode =>Adapter.IsDebugMode;

        public static bool IsMobileMode => Adapter.IsMobileMode;


        public static string ClientSelectedLanguage => Adapter.ClientSelectedLanguage;

        public static string Culture => Adapter.Culture;

        public static string Country => Adapter.Country;

        public static bool RightToLeft => Adapter.RightToLeft;

    }

    public static class Setting<T> where T : ISettingsAdapter
    {
        private static T Adapter => DependencyProvider.DependencyManager.Get<T>();
        public static string Language => Adapter.Language;

        public static bool IsDebugMode => Adapter.IsDebugMode;

        public static bool IsMobileMode => Adapter.IsMobileMode;


        public static string ClientSelectedLanguage => Adapter.ClientSelectedLanguage;

        public static string Culture => Adapter.Culture;

        public static string Country => Adapter.Country;

        public static bool RightToLeft => Adapter.RightToLeft;

    }
}

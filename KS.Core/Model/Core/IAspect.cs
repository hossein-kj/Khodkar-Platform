namespace KS.Core.Model.Core
{
    public interface IAspect
    {
        string Name { get; set; }
        int Permission { get; set; }
        bool EnableLog { get; set; }
        bool EnableCache { get; set; }
        bool HasMobileVersion { get; set; }
        double CacheSlidingExpirationTimeInMinutes { get; set; }
        int Status { get; set; }
        bool IsNull { get; set; }
    }
}
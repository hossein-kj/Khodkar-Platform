namespace KS.Core.Model.Core
{
    public class Aspect : IAspect
    {
        public string Name { get; set; }
        public int Permission { get; set; }
        public bool EnableLog { get; set; }
        public bool EnableCache { get; set; }
        public bool HasMobileVersion { get; set; }
        public double CacheSlidingExpirationTimeInMinutes { get; set; }
        public int Status { get; set; }
        public bool IsNull { get; set; }
    }
}

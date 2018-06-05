using System;

namespace KS.Core.Model.Develop
{
    public class DebugInfo
    {
        public DebugInfo()
        {
            
        }

        public DebugInfo(bool isAppDebugInfo,int dllVersion)
        {
            IsAppDebugInfo = isAppDebugInfo;
            DllVersion = dllVersion;
        }
        public int Id { get; set; }
        public int CodeId { get; set; }
        public string Data { get; set; }
        public bool IsOk { get; set; }
        public int? IntegerValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public DateTime? DateTime { get; set; }
        public int DllVersion { get; }
        public string CreateDateTime { get; set; }

        public bool IsAppDebugInfo { get; }
    }
}

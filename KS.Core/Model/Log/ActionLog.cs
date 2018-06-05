using System;

namespace KS.Core.Model.Log
{
    public class ActionLog : IActionLog
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string UrlReferrer { get; set; }
        public string ServiceUrl { get; set; }
        public string Parameters { get; set; }
        public string Type { get; set; }
        public string Request { get; set; }
        public string Coockies { get; set; }
        public string User { get; set; }
        public bool IsDebugMode { get; set; }
        public bool IsMobileMode { get; set; }
        public bool IsSuccessed { get; set; }
        public double ExecutionTimeInMilliseconds { get; set; }
        public string LocalDateTime
        {
            get; set;
        }
        public DateTime DateTime { get; set; }

    }
}

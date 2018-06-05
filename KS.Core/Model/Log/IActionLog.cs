using System;

namespace KS.Core.Model.Log
{
    public interface IActionLog
    {
        int Id { get; set; }
        string Ip { get; set; }
        string Name { get; set; }
        string Url { get; set; }
        string UrlReferrer { get; set; }
        string ServiceUrl { get; set; }
        string Parameters { get; set; }
        string Type { get; set; }
        string Request { get; set; }
        string Coockies { get; set; }
        string User { get; set; }
        bool IsDebugMode { get; set; }
        bool IsMobileMode { get; set; }
        bool IsSuccessed { get; set; }
        double ExecutionTimeInMilliseconds { get; set; }
        string LocalDateTime { get; set; }
        DateTime DateTime { get; set; }
    }
}
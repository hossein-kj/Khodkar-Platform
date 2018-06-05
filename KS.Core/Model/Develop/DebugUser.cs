using System;

namespace KS.Core.Model.Develop
{
    public class DebugUser
    {
        public int UserId { get; set; }
        public string Ip { get; set; }
        public string Guid { get; set; }
        public DateTime LoginDateTime { get; set; }
    }
}

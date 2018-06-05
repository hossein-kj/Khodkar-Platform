using System;

namespace KS.Core.Model.Develop
{
    public class SourceControlChange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Code { get; set; }
        public int Version { get; set; }
        public string User { get; set; }
        public string LocalDateTime { get; set; }
        public DateTime DateTime { get; set; }
    }
}

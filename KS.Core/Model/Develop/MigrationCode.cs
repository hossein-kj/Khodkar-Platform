
using System.Collections.Generic;

namespace KS.Core.Model.Develop
{
    public class MigrationCode
    {
        public string UserCode { get; set; }
        public string DesignerCode { get; set; }
        public string MigrationId { get; set; }
        public IDictionary<string,object> Resources { get; set; }
        public string Infos { get; set; }
        public string Warnings { get; set; }
        public string Verbose { get; set; }
    }
}

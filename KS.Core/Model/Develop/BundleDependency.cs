using System.Collections.Generic;
using KS.Core.Model.Core;

namespace KS.Core.Model.Develop
{
    public class BundleDependency
    {
        public string DependencyKey { get; set; }
        public string Path { get; set; }
        public List<KeyValue> Dependency { get; set; }
        public int Version { get; set; }
        public bool IsPublish { get; set; }
        public bool IsDelete { get; set; }
    }
}

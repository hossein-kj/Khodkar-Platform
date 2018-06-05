

namespace KS.Core.Model.Develop
{
    public class MigrationInfo
    {
        public string TargetName { get; set; }
        public string SourceName { get; set; }
        public string ConfigurationTypeName { get; set; }
        public string Connection { get; set; }
        public string NameSpaceQualifiedConnectionType { get; set; }
        public string ContextAssemblyName { get; set; }
        public string ContextAssemblyPath { get; set; }
        public string WebConfigPath { get; set; }
        public string AppDataPath { get; set; }
        public string ContextAssemblyRootNameSpace { get; set; }
        public GlobalVarioable.SourceType Language { get; set; }
        public bool Force { get; set; }
    }
}

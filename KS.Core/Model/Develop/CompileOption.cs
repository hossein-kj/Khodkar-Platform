

using KS.Core.GlobalVarioable;

namespace KS.Core.Model.Develop
{
    public class CompileOption
    {
        public string Code { get; set; }
        public SourceType CodeProvider { get; set; }
        public string[] LibraryRefrences { get; set; }
        public bool GenerateExecutable { get; set; }
        public bool IncludeDebugInformation { get; set; }
    }
}

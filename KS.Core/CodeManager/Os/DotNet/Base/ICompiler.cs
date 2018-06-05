
using KS.Core.Model.Develop;

namespace KS.Core.CodeManager.Os.DotNet.Base
{
    public interface ICompiler
    {
        void Compile(CompileOption compileOption, string path);
        string GenrateWcfCode(string xsdlUrl, string language);
    }
}

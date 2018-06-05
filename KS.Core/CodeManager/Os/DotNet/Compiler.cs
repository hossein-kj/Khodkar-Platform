
using KS.Core.CodeManager.Os.DotNet.Base;
using KS.Core.FileSystemProvide.Base;
using KS.Core.UI.Configuration;

namespace KS.Core.CodeManager.Os.DotNet
{
    public class Compiler:BaseCompiler,ICompiler
    {
        public Compiler(IWebConfigManager webConfigManager, IFileSystemManager fileSystemManager) 
            :base(webConfigManager, fileSystemManager)
        {

        }
    }
}

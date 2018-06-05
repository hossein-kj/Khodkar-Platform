using KS.Core.CodeManager.Os.DotNet.Base;
using KS.Core.FileSystemProvide.Base;

namespace KS.Core.CodeManager.Os.DotNet
{
    public class Debugger:BaseDebugger,IDebugger
    {
        public Debugger(IFileSystemManager fileSystemManager)
            :base(fileSystemManager)
        {
        }
    }
}

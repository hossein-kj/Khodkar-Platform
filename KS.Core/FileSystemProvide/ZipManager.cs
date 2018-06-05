
using KS.Core.FileSystemProvide.Base;

namespace KS.Core.FileSystemProvide
{
    public class ZipManager:BaseZipManager,IZipManager
    {
        public ZipManager(IFileSystemManager fileSystemManager):base(fileSystemManager)
        {
            
        }
    }
}

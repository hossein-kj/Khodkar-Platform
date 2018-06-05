using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.FileSystemProvide.Adapters
{
    public class DefaultFileSystemAdapter :BaseFileSystemAdapter,IDefaultFileSystemAdapter, IFileSystemAdapter
    { 
    }
}
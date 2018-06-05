using System.Collections.Generic;
using KS.Core.Model;
using KS.Core.Model.FileSystem;

namespace KS.Core.FileSystemProvide.Base
{
    public interface IZipManager
    {
        List<ZipInfo> OpenZip(string zipFullName, string orderBy, int skip, int take, out int count);
        bool UnZip(UnZipOprationInfo unZipOprationInfo);
        bool Zip(ZipOprationInfo zipOprationInfo);
    }
}
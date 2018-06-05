using System.Collections.Generic;
using KS.Core.Model;

namespace KS.Core.FileSystemProvide.Adapters
{
    public interface IDefaultZipAdapter
    {
        List<ZipInfo> OpenZip(string zipFullName, string orderBy, int skip, int take, out int count);
        bool UnZip(UnZipOprationInfo unZipOprationInfo);
        bool Zip(ZipOprationInfo zipOprationInfo);
    }
}
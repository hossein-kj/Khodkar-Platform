using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Hosting;
using Ionic.Zip;
using KS.Core.Model;

namespace KS.Core.FileSystemProvide.Adapters
{
    public sealed class DefaultZipAdapter:BaseZipAdapter,IDefaultZipAdapter,IZipAdapter
    {
    }
}

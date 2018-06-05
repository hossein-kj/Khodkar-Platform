
using System.Collections.Generic;
using KS.Core.CodeManager.Base;
using KS.Core.FileSystemProvide.Base;
using KS.Core.CodeManager.BrowsersCode.Base;
using KS.Core.Model.Develop;

namespace KS.Core.CodeManager
{
    public  class SourceControl : BaseSourceControl, ISourceControl
    {

        //public static List<DebugUser> DebugUsers { get; set; }
        //public static List<BrowsersCodeInfo> BrowsersCodeInfos { get; set; }
        //static SourceControl()
        //{
        //    BrowsersCodeInfos = new List<BrowsersCodeInfo>();
        //}

        public SourceControl(IFileSystemManager fileSystemManager,
            ICodeTemplate codeTemplate
            ,IZipManager zipManager)
            :base(fileSystemManager,codeTemplate,zipManager)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.ObjectiveModel.FileManagement
{
    public sealed class DiskOprationInfo
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public List<string> Files { get; set; }
        public List<string> Folders { get; set; }
        public bool OverWrite { get; set; }
    }
}

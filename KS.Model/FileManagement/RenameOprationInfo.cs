using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.FileManagement
{
    public sealed class RenameOprationInfo
    {
        public string OldPath { get; set; }
        public string NewPath { get; set; }
        public bool IsDirectory { get; set; }
    }
}

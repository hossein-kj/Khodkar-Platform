using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.ObjectiveModel.FileManagement
{
    public sealed class DeleteOprationInfo
    {
        public string Path { get; set; }
        public List<string> Files { get; set; }
        public List<string> Folders { get; set; }
    }
}

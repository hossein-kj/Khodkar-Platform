using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.FileManagement
{
    public sealed class DiskInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int? FileType { get; set; }
        public bool IsFolder { get; set; }
        public float Size { get; set; }
        public DateTime ModifieDateTime { get; set; }
        public string ModifieLocalDateTime { get; set; }
    }
}

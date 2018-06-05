using System.Collections.Generic;

namespace KS.Core.Model.FileSystem
{
    public class ZipOprationInfo
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public List<string> Folders { get; set; }
        public List<string> Files { get; set; }
        public string Encryption { get; set; }
        public string Password { get; set; }
        public string CompressionLevel { get; set; }
        public bool OverWrite { get; set; }
        public bool IsNew { get; set; }

    }
}

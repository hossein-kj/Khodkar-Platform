using System;

namespace KS.Core.Model.FileSystem
{
    public class ZipInfo
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string CompressionRatio { get; set; }
        public string UncompressedSize { get; set; }
        public string CompressedSize { get; set; }
        public bool UsesEncryption { get; set; }
        public DateTime LastModified { get; set; }
        public string LocalLastModified { get; set; }
    }
}

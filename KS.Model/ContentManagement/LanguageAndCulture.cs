using System.ComponentModel.DataAnnotations;
using KS.Model.Base;

namespace KS.Model.ContentManagement
{
    public sealed class LanguageAndCulture : BaseEntityWithoutAutoIdentity
    {
        [StringLength(8)]
        public string Culture { get; set; }
        public string Country { get; set; }
        public bool IsRightToLeft { get; set; }
        public bool IsDefaults { get; set; }
        public int? FlagId { get; set; }
        public FilePath Flag { get; set; }
        public int Version { get; set; }
    }
}

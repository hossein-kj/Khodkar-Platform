using System.ComponentModel.DataAnnotations;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.ContentManagement
{
    public sealed class LanguageAndCultureObjective : BaseEntityWithoutAutoIdentityObjective
    {
        [StringLength(8)]
        public string Culture { get; set; }
        public string Country { get; set; }
        public bool IsRightToLeft { get; set; }
        public bool IsDefaults { get; set; }
        public int? FlagId { get; set; }
        public FilePathObjective Flag { get; set; }
        public int Version { get; set; }
    }
}

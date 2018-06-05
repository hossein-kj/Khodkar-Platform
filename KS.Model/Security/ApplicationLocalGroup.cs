
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KS.Model.Base;

namespace KS.Model.Security
{
    public sealed class ApplicationLocalGroup : BaseEntityWithoutUserProfile
    {
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public ApplicationGroup Group { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
    }
}

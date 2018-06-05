using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.Base;

namespace KS.Model.ContentManagement
{
    public sealed class LocalFile: BaseEntityWithoutRoles
    {
        public int FileId { get; set; }

        [ForeignKey("FileId")]
        public File File { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Description { get; set; }
    }
}

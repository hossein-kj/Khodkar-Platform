using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.ContentManagement
{
    public sealed class LocalFilePathObjective : BaseEntityWithoutRolesObjective
    {
        public int FilePathId { get; set; }

        [ForeignKey("FilePathId")]
        public FilePathObjective FilePath { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Description { get; set; }
    }
}

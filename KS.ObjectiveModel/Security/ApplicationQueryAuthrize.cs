
using KS.Core.Security;
using KS.ObjectiveModel.Base;

namespace KS.ObjectiveModel.Security
{
    public class ApplicationQueryAuthrize: BaseEntityWithoutUserProfile,IAccessRole
    {
        public int? GroupId { get; set; }
        public int? RoleId { get; set; }

        //entityName Or EntityTypeId Or *
        public string Entity { get; set; }

        //select or where or from
        public int ResourceTypeId { get; set; }

        //name,family or name='ali',id>10 or Link or *
        public string Resource { get; set; }

        public bool Grant { get; set; }

        public int? ViewRoleId { get; set; }
        public int? ModifyRoleId { get; set; }
        public int? AccessRoleId { get; set; }
    }
}

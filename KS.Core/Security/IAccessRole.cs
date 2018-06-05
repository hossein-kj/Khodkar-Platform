namespace KS.Core.Security
{
    public interface IAccessRole
    {
         int? ViewRoleId { get; set; }
         int? ModifyRoleId { get; set; }
         int? AccessRoleId { get; set; }
    }
}

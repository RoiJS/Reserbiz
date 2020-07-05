using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IUserActionTracker
    {
        int? DeletedById { get; set; }
        Account DeletedBy { get; set; }
        int? UpdatedById { get; set; }
        Account UpdatedBy { get; set; }
        int? CreatedById { get; set; }
        Account CreatedBy { get; set; }
        int? DeactivatedById { get; set; }
        Account DeactivatedBy { get; set; }
    }
}
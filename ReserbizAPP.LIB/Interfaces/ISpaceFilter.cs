using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface ISpaceFilter : IEntityFilter
    {
        string Description { get; set; }
        UnitStatusEnum Status { get; set; }
        int UnitTypeId { get; set; }
    }
}
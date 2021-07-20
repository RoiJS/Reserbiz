using System;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class SpaceFilter : ISpaceFilter
    {
        public string Description { get; set; }
        public UnitStatusEnum Status { get; set; }
        public int UnitTypeId { get; set; }
        public SortOrderEnum? SortOrder { get; set; } = SortOrderEnum.Ascending;
    }
}
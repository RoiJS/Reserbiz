using System.Collections.Generic;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Dtos
{
    public class PenaltyPaginationListDto : IPenaltyPaginationListDto
    {
        public double TotalAmount { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int NumberOfItemsPerPage { get; set; }
        public IEnumerable<IEntityDto> Items { get; set; }
    }
}
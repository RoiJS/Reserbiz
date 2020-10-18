using System.Collections.Generic;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class AccountStatementPaginationListDto : IAccountStatementPaginationListDto
    {
        public float TotalExpectedAmount { get; set; }
        public float TotalPaidAmount { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int NumberOfItemsPerPage { get; set; }
        public IEnumerable<IEntityDto> Items { get; set; }

    }
}
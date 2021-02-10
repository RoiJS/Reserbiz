using System.Collections.Generic;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IEntityPaginationListDto
    {
        int TotalItems { get; set; }

        int Page { get; set; }

        int NumberOfItemsPerPage { get; set; }

        IEnumerable<IEntityDto> Items { get; set; }
    }
}
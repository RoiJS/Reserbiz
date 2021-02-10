using System.Collections.Generic;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IPaginationService
    {
        int NumberOfItemsPerPage { get; set; }

        IEntityPaginationListDto PaginateEntityList(IEnumerable<IEntityDto> entityDtoList, int pageNumber);

        T PaginateEntityListGeneric<T>(IEnumerable<IEntityDto> entityDtoList, int pageNumber) where T : IEntityPaginationListDto, new();
    }
}
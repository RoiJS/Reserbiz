using System.Collections.Generic;
using System.Linq;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Helpers
{
    public class PaginationService : IPaginationService
    {
        /// <summary>
        /// Number of items per page
        /// </summary>
        /// <value></value>
        public int NumberOfItemsPerPage { get; set; }

        public PaginationService()
        {
            NumberOfItemsPerPage = 10;
        }

        public IEntityPaginationListDto PaginateEntityList(IEnumerable<IEntityDto> entityDtoList, int pageNumber)
        {
            var entityPaginationListDto = new EntityPaginationListDto();
            var items = new List<IEntityDto>();
            var skip = NumberOfItemsPerPage * (pageNumber - 1);

            // Check if current page number is 0, If so
            // get all items, otherwise, apply pagination
            if (pageNumber == 0)
            {
                items = entityDtoList.ToList();
            }
            else
            {
                items = entityDtoList.Skip(skip).Take(NumberOfItemsPerPage).ToList();
            }

            entityPaginationListDto.TotalItems = entityDtoList.Count();
            entityPaginationListDto.NumberOfItemsPerPage = NumberOfItemsPerPage;
            entityPaginationListDto.Page = pageNumber;
            entityPaginationListDto.Items = items;

            return entityPaginationListDto;
        }

        public T PaginateEntityListGeneric<T>(IEnumerable<IEntityDto> entityDtoList, int pageNumber) where T : IEntityPaginationListDto, new()
        {
            var entityPaginationListDto = new T();
            var items = new List<IEntityDto>();
            var skip = NumberOfItemsPerPage * (pageNumber - 1);

            // Check if current page number is 0, If so
            // get all items, otherwise, apply pagination
            if (pageNumber == 0)
            {
                items = entityDtoList.ToList();
            }
            else
            {
                items = entityDtoList.Skip(skip).Take(NumberOfItemsPerPage).ToList();
            }

            entityPaginationListDto.TotalItems = entityDtoList.Count();
            entityPaginationListDto.NumberOfItemsPerPage = NumberOfItemsPerPage;
            entityPaginationListDto.Page = pageNumber;
            entityPaginationListDto.Items = items;

            return entityPaginationListDto;
        }
    }
}
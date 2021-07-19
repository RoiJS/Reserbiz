using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class SpaceRepository
        : BaseRepository<Space>, ISpaceRepository<Space>
    {

        public SpaceRepository(IReserbizRepository<Space> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task<IEnumerable<Space>> GetSpacesAsOptions()
        {
            return await _reserbizRepository.ClientDbContext.Spaces
                                .Includes(s => s.Contracts)
                                .OrderBy(s => s.Description)
                                .ToListAsync();
        }

        public async Task<Space> GetSpaceAsync(int spaceId)
        {
            var spaceFromRepo = await _reserbizRepository.ClientDbContext.Spaces.AsQueryable()
                                                        .Includes(
                                                            s => s.Contracts,
                                                            s => s.SpaceType
                                                        )
                                                        .Where(s => s.Id == spaceId)
                                                        .FirstOrDefaultAsync();

            return spaceFromRepo;
        }

        public async Task<IEnumerable<Space>> GetAllActiveSpaces()
        {
            var activeSpacesFromRepo = _reserbizRepository.ClientDbContext.Spaces.AsQueryable()
                                         .Includes(
                                            t => t.Contracts,
                                            s => s.SpaceType
                                        )
                                         .Where(t => !t.IsDelete)
                                        .ToListAsync();
            return await activeSpacesFromRepo;
        }

        public List<Space> GetFilteredSpaces(IList<Space> unfilteredSpaces, ISpaceFilter spaceFilter)
        {
            var filteredSpaces = unfilteredSpaces;

            // Filter by space description
            if (!String.IsNullOrEmpty(spaceFilter.Description))
            {
                filteredSpaces = filteredSpaces.Where(c => c.Description.Contains(spaceFilter.Description)).ToList();
            }

            // Filter by unit type id
            if (spaceFilter.UnitTypeId != 0)
            {
                filteredSpaces = filteredSpaces.Where(c => c.SpaceTypeId == spaceFilter.UnitTypeId).ToList();
            }

            // Filter by unit type id
            if (spaceFilter.Status != UnitStatusEnum.All)
            {
                // Get list of available units
                if (spaceFilter.Status == UnitStatusEnum.Available)
                {
                    filteredSpaces = filteredSpaces.Where(c => c.IsNotOccupied == true).ToList();
                }

                // Get list of occupied units
                if (spaceFilter.Status == UnitStatusEnum.Occupied)
                {
                    filteredSpaces = filteredSpaces.Where(c => c.IsNotOccupied == false).ToList();
                }
            }

            // Set sort order based on description
            // Sort order is ascending by default
            if (spaceFilter.SortOrder == SortOrderEnum.Ascending)
            {
                return filteredSpaces
                    .OrderBy(c => c.Description)
                    .ToList();
            }
            else
            {
                return filteredSpaces
                    .OrderByDescending(c => c.Description)
                    .ToList();
            }
        }

        public async Task<bool> DeleteMultipleSpacesAsync(List<int> spaceIds)
        {
            var selectedSpaces = await _reserbizRepository
                .ClientDbContext
                .Spaces
                .Where(t => spaceIds.Contains(t.Id)).ToListAsync();

            DeleteMultipleEntities(selectedSpaces);
            return await SaveChanges();
        }
    }
}
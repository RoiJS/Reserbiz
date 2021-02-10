using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class SpaceTypeRepository
        : BaseRepository<SpaceType>, ISpaceTypeRepository<SpaceType>
    {
        public SpaceTypeRepository(IReserbizRepository<SpaceType> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task<IEnumerable<SpaceType>> GetSpaceTypesBasedOnNameAsync(string spaceTypeName)
        {
            var allSpaceTypesFromRepo = _reserbizRepository.ClientDbContext.SpaceTypes
                                            .Includes(
                                                s => s.Terms,
                                                s => s.Spaces
                                            )
                                            .Where(s => !s.IsDelete);


            // Filter space types based on the name
            if (!string.IsNullOrEmpty(spaceTypeName))
            {
                allSpaceTypesFromRepo = allSpaceTypesFromRepo.Where(s => s.Name.Contains(spaceTypeName));
            }

            return await allSpaceTypesFromRepo
                            .OrderBy(s => s.Name)
                            .ToListAsync();
        }

        public async Task<IEnumerable<SpaceType>> GetSpaceTypesAsOptions()
        {
            return await _reserbizRepository.ClientDbContext.SpaceTypes
                                    .OrderBy(s => s.Name)
                                    .ToListAsync();
        }

        public async Task<SpaceType> GetSpaceTypeAsync(int spaceTypeId)
        {
            var spaceTypeFromRepo = await _reserbizRepository.ClientDbContext.SpaceTypes
                                            .AsQueryable()
                                            .Includes(s => s.Terms)
                                            .Where(s => s.Id == spaceTypeId)
                                            .FirstOrDefaultAsync();

            return spaceTypeFromRepo;
        }

        public async Task<bool> DeleteMultipleSpaceTypesAsync(List<int> spaceTypeIds)
        {
            var selectedSpaceTypes = await _reserbizRepository
                .ClientDbContext
                .SpaceTypes
                .Where(t => spaceTypeIds.Contains(t.Id)).ToListAsync();

            DeleteMultipleEntities(selectedSpaceTypes);
            return await SaveChanges();
        }
    }
}
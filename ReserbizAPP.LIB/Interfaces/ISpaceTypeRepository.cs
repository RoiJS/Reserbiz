using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface ISpaceTypeRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<SpaceTypeDetailDto>> GetSpaceTypesBasedOnNameAsync(string spaceTypeName);
        Task<IEnumerable<SpaceType>> GetSpaceTypesAsOptions();
        Task<SpaceType> GetSpaceTypeAsync(int spaceTypeId);
        Task<bool> DeleteMultipleSpaceTypesAsync(List<int> spaceTypeIds);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface ISpaceRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<Space> GetSpaceAsync(int spaceId);
        Task<IEnumerable<Space>> GetAllActiveSpaces();
        List<Space> GetFilteredSpaces(IList<Space> unfilteredspaces, ISpaceFilter spaceFilter);
        Task<IEnumerable<Space>> GetSpacesAsOptions();
        Task<bool> DeleteMultipleSpacesAsync(List<int> spaceIds);
        // Task<bool> CheckSpaceAvailability(int spaceId);
    }
}
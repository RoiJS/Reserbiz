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
    }
}
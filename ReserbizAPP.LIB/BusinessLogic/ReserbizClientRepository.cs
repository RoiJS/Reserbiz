using System.Threading.Tasks;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ReserbizClientRepository : IReserbizClientRepository
    {
        private readonly ReserbizRepository _reserbizRepository;
        public ReserbizClientRepository(ReserbizRepository reserbizRepository)
        {
            _reserbizRepository = reserbizRepository;
        }

        public void Add<T>(T entity)
        {
            _reserbizRepository._clientDbContext.Add(entity);
        }

        public void Delete<T>(T entity)
        {
            _reserbizRepository._clientDbContext.Add(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _reserbizRepository._clientDbContext.SaveChangesAsync() > 0;
        }
    }
}
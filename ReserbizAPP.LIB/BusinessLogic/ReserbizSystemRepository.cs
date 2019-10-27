using System.Threading.Tasks;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ReserbizSystemRepository : IReserbizSystemRepository
    {
        private readonly ReserbizRepository _reserbizRepository;
        public ReserbizSystemRepository(ReserbizRepository reserbizRepository)
        {
            _reserbizRepository = reserbizRepository;

        }
        public void Add<T>(T entity)
        {
            _reserbizRepository._systemDbContext.Add(entity);
        }

        public void Delete<T>(T entity)
        {
            _reserbizRepository._systemDbContext.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _reserbizRepository._systemDbContext.SaveChangesAsync() > 0;
        }
    }
}
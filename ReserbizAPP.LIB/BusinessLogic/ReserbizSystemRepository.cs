using System.Threading.Tasks;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ReserbizSystemRepository : IReserbizSystemRepository
    {
        private readonly ReserbizRepository _reserbizRepository;

        public ReserbizDataContext Context => _reserbizRepository.SystemDbContext;

        public ReserbizSystemRepository(ReserbizRepository reserbizRepository)
        {
            _reserbizRepository = reserbizRepository;

        }
        public void AddEntity<T>(T entity)
        {
            _reserbizRepository.SystemDbContext.Add(entity);
        }

        public void DeleteEntity<T>(T entity)
        {
            _reserbizRepository.SystemDbContext.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _reserbizRepository.SystemDbContext.SaveChangesAsync() > 0;
        }
    }
}
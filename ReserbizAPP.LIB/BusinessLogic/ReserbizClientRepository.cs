using System.Threading.Tasks;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ReserbizClientRepository : IReserbizClientRepository
    {
        private readonly ReserbizRepository _reserbizRepository;

        public ReserbizClientDataContext Context => _reserbizRepository.ClientDbContext;

        public ReserbizClientRepository(ReserbizRepository reserbizRepository)
        {
            _reserbizRepository = reserbizRepository;
        }

        public void AddEntity<T>(T entity)
        {
            _reserbizRepository.ClientDbContext.Add(entity);
        }

        public void DeleteEntity<T>(T entity)
        {
            _reserbizRepository.ClientDbContext.Add(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _reserbizRepository.ClientDbContext.SaveChangesAsync() > 0;
        }
    }
}
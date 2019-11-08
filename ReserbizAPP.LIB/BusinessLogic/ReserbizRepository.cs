using System.Threading.Tasks;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ReserbizRepository : IReserbizRepository
    {
        
        private ReserbizDataContext _systemDbContext;
        private ReserbizClientDataContext _clientDbContext;
        private ReserbizSystemRepository _systemRepository;
        private ReserbizClientRepository _clientRepository;
        
        public ReserbizRepository(ReserbizDataContext systemDbContext, ReserbizClientDataContext clientDbContext)
        {
            _systemDbContext = systemDbContext;
            _clientDbContext = clientDbContext;

            _systemRepository = new ReserbizSystemRepository(this);
            _clientRepository = new ReserbizClientRepository(this);
        }

        public ReserbizDataContext SystemDbContext => _systemDbContext;
        public ReserbizClientDataContext ClientDbContext => _clientDbContext;

        public IReserbizSystemRepository SystemRepository => _systemRepository;
        public IReserbizClientRepository ClientRepository => _clientRepository;
    }
}
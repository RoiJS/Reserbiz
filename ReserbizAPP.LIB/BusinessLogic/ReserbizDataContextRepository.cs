using ReserbizAPP.LIB.DbContexts;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ReserbizDataContextRepository
    {
        public readonly ReserbizDataContext _systemDbContext;
        public readonly ReserbizClientDataContext _clientDbContext;

        public ReserbizDataContextRepository(ReserbizDataContext systemDbContext, ReserbizClientDataContext clientDbContext)
        {
            _systemDbContext = systemDbContext;
            _clientDbContext = clientDbContext;
        }
    }
}
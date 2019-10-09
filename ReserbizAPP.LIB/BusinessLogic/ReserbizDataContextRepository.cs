using ReserbizAPP.LIB.DbContext;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ReserbizDataContextRepository
    {
        protected readonly ReserbizDataContext _systemDbContext;
        protected readonly ReserbizClientDataContext _clientDbContext;

        public ReserbizDataContextRepository(ReserbizDataContext systemDbContext, ReserbizClientDataContext clientDbContext)
        {
            _systemDbContext = systemDbContext;
            _clientDbContext = clientDbContext;
        }
    }
}
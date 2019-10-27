using System.Threading.Tasks;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ReserbizRepository : ReserbizDataContextRepository, IReserbizRepository
    {
        public ReserbizRepository(ReserbizDataContext systemDbContext, ReserbizClientDataContext clientDbContext) : base(systemDbContext, clientDbContext)
        {
            ReserbizSystemRepository = new ReserbizSystemRepository(this);
            ReserbizClient = new ReserbizClientRepository(this);
        }

        public IReserbizSystemRepository ReserbizSystemRepository { get; set; }
        public IReserbizClientRepository ReserbizClient { get; set; }
    }
}
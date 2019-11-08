using System.Threading.Tasks;
using ReserbizAPP.LIB.DbContexts;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IReserbizRepository
    {
        ReserbizDataContext SystemDbContext { get; }
        ReserbizClientDataContext ClientDbContext { get; }

        IReserbizSystemRepository SystemRepository { get; }
        IReserbizClientRepository ClientRepository { get; }
    }
}
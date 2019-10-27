using System.Threading.Tasks;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IReserbizRepository
    {
        IReserbizSystemRepository ReserbizSystemRepository { get; set; }
        IReserbizClientRepository ReserbizClient { get; set; }
    }
}
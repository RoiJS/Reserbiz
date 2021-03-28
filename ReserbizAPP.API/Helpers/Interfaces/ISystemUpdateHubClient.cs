using System.Threading.Tasks;

namespace ReserbizAPP.API.Helpers.Interfaces
{
    public interface ISystemUpdateHub
    {
        Task BroadCastSystemUpdateStatus(bool systemUpdateStatus);
    }
}
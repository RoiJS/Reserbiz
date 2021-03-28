using System.Threading.Tasks;

namespace ReserbizAPP.API.Helpers.Interfaces
{
    public interface IAuthHubClient
    {
        Task BroadCastValidateLogin(int id, string username);
    }
}
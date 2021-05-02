using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IFireBasePushNotificationService
    {
        Task Send(string title, string body, Dictionary<string, string> data);
    }
}
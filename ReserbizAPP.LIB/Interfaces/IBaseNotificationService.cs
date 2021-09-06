using System.Threading.Tasks;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IBaseNotificationService
    {
        NotificationTypeEnum notificationType { get; }
        string notificationTextFormatIdentifier { get; }
        Task<int> Register();
        Task<string> ConvertNotificationDetailsToText(IReserbizRepository<Entity> reserbizRepository, string textFormat, int notificationTypeId);
        Task<string> GenerateNotificationUrl(IReserbizRepository<Entity> reserbizRepository, int notificationTypeId);
    }
}
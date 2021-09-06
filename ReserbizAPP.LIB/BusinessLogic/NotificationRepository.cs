using System.Threading.Tasks;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class NotificationRepository
        : BaseRepository<Notification>, INotificationRepository<Notification>
    {

        public NotificationRepository(IReserbizRepository<Notification> reserbizRepository)
                    : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task<int> Register(IBaseNotificationService baseNotificationService)
        {
            var notificationTypeId = await baseNotificationService.Register();
            var notification = new Notification
            {
                NotificationType = baseNotificationService.notificationType,
                NotificationTypeId = notificationTypeId
            };

            await AddEntity(notification);

            return notification.Id;
        }
    }
}
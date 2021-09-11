using System.Threading.Tasks;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface INotificationRepository<TEntity>
         : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<int> Register(IBaseNotificationService baseNotificationService, int notificationFromId, NotificationFromTypeEnum notificationFromType);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IUserNotificationRepository<TEntity>
     : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task Register(UserNotification userNotification);
        Task<List<UserNotificationForListDto>> GetUserNotificationAsync(int userId, UserTypeEnum userType);
        List<UserNotificationForListDto> GetFilteredUserNotifications(IList<UserNotificationForListDto> unfilteredUserNotifications, IUserNotificationFilter userNotificationFilter);
        Task<int> GetUnreadNotificationsCount();
    }
}
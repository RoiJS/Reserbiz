using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class UserNotificationRepository
        : BaseRepository<UserNotification>, IUserNotificationRepository<UserNotification>
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IReserbizRepository<Entity> _genericReserbizRepository;
        public UserNotificationRepository(
            IReserbizRepository<UserNotification> reserbizRepository,
            IReserbizRepository<Entity> genericReserbizRepository,
            IStringLocalizer stringLocalizer)
                    : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {
            _genericReserbizRepository = genericReserbizRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task Register(UserNotification userNotification)
        {
            await AddEntity(userNotification);
        }

        public async Task<List<UserNotificationForListDto>> GetUserNotificationAsync(int userId, UserTypeEnum userType)
        {
            var userNotificationList = new List<UserNotificationForListDto>();

            var userNotificationsFromRepo = await (from un in _reserbizRepository.ClientDbContext.UserNotifications

                                                   join n in _reserbizRepository.ClientDbContext.Notifications
                                                   on un.NotificationId equals n.Id

                                                   where un.UserId == userId && un.UserType == userType

                                                   select new
                                                   {
                                                       UserNotification = un,
                                                       Notification = n
                                                   }).ToListAsync();


            foreach (var notificationItem in userNotificationsFromRepo)
            {
                var notificationServiceTypeName = Enum.GetName(typeof(NotificationTypeEnum), notificationItem.Notification.NotificationType);
                var className = $"ReserbizAPP.LIB.Helpers.Services.{notificationServiceTypeName}NotificationService";
                var constructedType = Type.GetType(className);

                var notificationTypeInstance = (IBaseNotificationService)Activator.CreateInstance(constructedType);

                var notificationUrl = await notificationTypeInstance.GenerateNotificationUrl(_genericReserbizRepository, notificationItem.Notification.Id);
                var notificationMessageFormat = _stringLocalizer[notificationTypeInstance.notificationTextFormatIdentifier];
                var notificationMessage = await notificationTypeInstance.ConvertNotificationDetailsToText(_genericReserbizRepository, notificationMessageFormat, notificationItem.Notification.Id);

                userNotificationList.Add(new UserNotificationForListDto
                {
                    Id = notificationItem.UserNotification.Id,
                    NotificationUrl = notificationUrl,
                    NotificationMessage = notificationMessage,
                    NotificationDateTime = notificationItem.UserNotification.DateCreated,
                    ReadStatus = notificationItem.UserNotification.ReadStatus
                });
            }

            return userNotificationList;
        }

        public List<UserNotificationForListDto> GetFilteredUserNotifications(IList<UserNotificationForListDto> unfilteredUserNotifications, IUserNotificationFilter userNotificationFilter)
        {
            var filteredUserNotifications = unfilteredUserNotifications;

            // Filter user notification where the filter NotificationDate should 
            // equal to notification date and time.
            if (userNotificationFilter.NotificationDateTime != DateTime.MinValue)
            {
                filteredUserNotifications = filteredUserNotifications.Where(c => userNotificationFilter.NotificationDateTime == c.NotificationDateTime).ToList();
            }

            // Set sort order based on notification date and time
            // Sort order is ascending by default
            if (userNotificationFilter.SortOrder == SortOrderEnum.Ascending)
            {
                return filteredUserNotifications
                    .OrderBy(c => c.NotificationDateTime)
                    .ToList();
            }
            else
            {
                return filteredUserNotifications
                    .OrderByDescending(c => c.NotificationDateTime)
                    .ToList();
            }
        }
    }
}
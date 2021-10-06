using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class UserNotificationRepository
        : BaseRepository<UserNotification>, IUserNotificationRepository<UserNotification>
    {
        private readonly IOptions<ApplicationSettings> _appSettings;
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IAccountRepository<Account> _accountRepository;
        private readonly IReserbizRepository<Entity> _genericReserbizRepository;
        public UserNotificationRepository(
            IAccountRepository<Account> accountRepository,
            IReserbizRepository<UserNotification> reserbizRepository,
            IReserbizRepository<Entity> genericReserbizRepository,
            IStringLocalizer stringLocalizer,
            IOptions<ApplicationSettings> appSettings)
                    : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {
            _appSettings = appSettings;
            _accountRepository = accountRepository;
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
                var notificationFrom = await GetNotificationFrom(notificationItem.Notification);

                var currentDateTime = DateTime.Now.ConvertToTimeZone(_appSettings.Value.GeneralSettings.TimeZone); 
                var notificationDateTime = notificationItem.UserNotification.DateCreated.ConvertToTimeZone(_appSettings.Value.GeneralSettings.TimeZone);
                var notificationDaysAgo = currentDateTime.Subtract(notificationDateTime).Days;

                userNotificationList.Add(new UserNotificationForListDto
                {
                    Id = notificationItem.UserNotification.Id,
                    NotificationUrl = notificationUrl,
                    NotificationMessage = notificationMessage,
                    NotificationDateTime = notificationItem.UserNotification.DateCreated.ConvertToTimeZone(_appSettings.Value.GeneralSettings.TimeZone),
                    NotificationDateTimeDaysAgo = notificationDaysAgo,
                    ReadStatus = notificationItem.UserNotification.ReadStatus,
                    NotificationFrom = notificationFrom,
                    NotificationItemType = NotificationItemTypeEnum.NotificationItem
                });
            }

            return userNotificationList;
        }

        public List<UserNotificationForListDto> GetFilteredUserNotifications(IList<UserNotificationForListDto> unfilteredUserNotifications, IUserNotificationFilter userNotificationFilter)
        {
            var filteredUserNotifications = unfilteredUserNotifications;

            // Filter user notification where the filter NotificationDate should 
            // be less than or equal to notification date
            if (userNotificationFilter.FromDate != DateTime.MinValue)
            {
                filteredUserNotifications = filteredUserNotifications.Where(c => c.NotificationDateTime >= userNotificationFilter.FromDate).ToList();
            }

            // Filter user notification where the filter NotificationDate should
            // be greater than or equal to notification date
            if (userNotificationFilter.ToDate != DateTime.MinValue)
            {
                filteredUserNotifications = filteredUserNotifications.Where(c => c.NotificationDateTime <= userNotificationFilter.ToDate).ToList();
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

        public async Task<int> GetUnreadNotificationsCount()
        {
            var count = await _reserbizRepository.ClientDbContext.UserNotifications
                                            .Where(u => u.ReadStatus == false)
                                            .CountAsync();

            return count;
        }

        private async Task<string> GetNotificationFrom(Notification notification)
        {
            var notificationFrom = "";

            if (notification.NotificationFromId == 0)
            {
                notificationFrom = _stringLocalizer["reserbiz_label"];
            }
            else
            {
                switch (notification.NotificationFromType)
                {
                    case NotificationFromTypeEnum.Administrator:
                        var adminAccount = await _accountRepository
                                            .GetEntity(notification.NotificationFromId)
                                            .ToObjectAsync();

                        notificationFrom = adminAccount.PersonFullName;
                        break;
                }
            }

            return notificationFrom;
        }
    }
}
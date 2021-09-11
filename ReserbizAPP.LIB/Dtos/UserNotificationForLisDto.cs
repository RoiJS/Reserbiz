using System;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Dtos
{
    public class UserNotificationForListDto : IEntityDto
    {
        public int Id { get; set; }
        public bool ReadStatus { get; set; }
        public string NotificationFrom { get; set; }
        public string NotificationMessage { get; set; }
        public string NotificationUrl { get; set; }
        public DateTime NotificationDateTime { get; set; }
        public int NotificationDateTimeDaysAgo { get; set; }
        public NotificationItemTypeEnum NotificationItemType { get; set; }
    }
}
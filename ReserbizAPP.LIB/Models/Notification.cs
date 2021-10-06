using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Models
{
    public class Notification
        : Entity
    {
        public NotificationTypeEnum NotificationType { get; set; }
        public int NotificationTypeId { get; set; }
        public int NotificationFromId { get; set; }
        public NotificationFromTypeEnum NotificationFromType { get; set; }
    }
}
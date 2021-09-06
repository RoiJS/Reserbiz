using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Models
{
    public class Notification
        : Entity
    {
        public NotificationTypeEnum NotificationType { get; set; }
        public int NotificationTypeId { get; set; }
    }
}
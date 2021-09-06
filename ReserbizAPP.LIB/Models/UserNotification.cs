using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Models
{
    public class UserNotification
        : Entity
    {
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        public UserTypeEnum UserType { get; set; }
        public int UserId { get; set; }
        public bool ReadStatus { get; set; }
    }
}
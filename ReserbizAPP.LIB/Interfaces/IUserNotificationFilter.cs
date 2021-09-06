using System;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IUserNotificationFilter : IEntityFilter
    {
        DateTime NotificationDateTime { get; set; }
    }
}
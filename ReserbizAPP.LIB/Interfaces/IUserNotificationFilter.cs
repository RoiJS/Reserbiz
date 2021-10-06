using System;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IUserNotificationFilter : IEntityFilter
    {
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
    }
}
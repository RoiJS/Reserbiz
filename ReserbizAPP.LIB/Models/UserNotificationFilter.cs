using System;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class UserNotificationFilter : IUserNotificationFilter
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public SortOrderEnum? SortOrder { get; set; }
    }
}
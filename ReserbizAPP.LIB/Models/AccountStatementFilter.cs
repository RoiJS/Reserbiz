using System;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class AccountStatementFilter : IAccountStatementFilter
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public PaymentStatusEnum PaymentStatus { get; set; } = PaymentStatusEnum.All;
        public SortOrderEnum? SortOrder { get; set; } = SortOrderEnum.Ascending;
    }
}
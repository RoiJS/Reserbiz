using System;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IAccountStatementFilter : IEntityFilter
    {
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        PaymentStatusEnum PaymentStatus { get; set; }
    }
}
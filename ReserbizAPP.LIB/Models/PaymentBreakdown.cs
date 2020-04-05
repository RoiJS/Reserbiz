using System;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class PaymentBreakdown : Entity, IUserActionTracker
    {
        public int AccountStatementId { get; set; }
        public AccountStatement AccountStatement { get; set; }
        public float Amount { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public int ReceivedById { get; set; }
        public Account ReceivedBy { get; set; }

        public int? DeletedById { get; set; }
        public Account DeletedBy { get; set; }
        public int? UpdatedById { get; set; }
        public Account UpdatedBy { get; set; }
        public int? CreatedById { get; set; }
        public Account CreatedBy { get; set; }
        public int? DeactivatedById { get; set; }
        public Account DeactivatedBy { get; set; }
    }
}
using System;

namespace ReserbizAPP.LIB.Models
{
    public class PaymentBreakdown : Entity
    {
        public int AccountStatementId { get; set; }
        public AccountStatement AccountStatement { get; set; }
        public float Amount { get; set; }

        // Received by
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public DateTime DateReceived { get; set; }
    }
}
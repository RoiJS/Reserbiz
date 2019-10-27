using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReserbizAPP.LIB.Models
{
    public class PaymentBreakdown : Entity
    {
        public int AccountStatementId { get; set; }
        public AccountStatement AccountStatement { get; set; }
        public float Amount { get; set; }

        public int ReceivedById { get; set; }
        public Account ReceivedBy { get; set; }
        
        public DateTime DateReceived { get; set; }
    }
}
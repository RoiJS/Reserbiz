using System;

namespace ReserbizAPP.LIB.Models
{
    public class PenaltyBreakdown : Entity
    {
        public int AccountStatementId { get; set; }
        public AccountStatement AccountStatement { get; set; }
        public DateTime DueDate { get; set; }
        public float Amount { get; set; }
    }
}
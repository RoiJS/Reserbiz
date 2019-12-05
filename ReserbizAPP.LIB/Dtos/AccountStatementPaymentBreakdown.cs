using System;

namespace ReserbizAPP.LIB.Dtos
{
    public class AccountStatementPaymentItemDetailsDto
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public int ReceivedById { get; set; }
        public DateTime DateReceived { get; set; }
    }
}
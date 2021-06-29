using System;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Dtos
{
    public class PaymentForCreationDto
    {
        public float Amount { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public string Notes { get; set; }
        public bool IsAmountFromDeposit { get; set; }
        public PaymentForTypeEnum PaymentForType { get; set; }
    }
}
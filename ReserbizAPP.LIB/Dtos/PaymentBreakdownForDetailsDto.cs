using System;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Dtos
{
    public class PaymentBreakdownForDetailsDto : IEntityDto
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public string ReceivedBy { get; set; }
        public string Notes { get; set; }
    }
}
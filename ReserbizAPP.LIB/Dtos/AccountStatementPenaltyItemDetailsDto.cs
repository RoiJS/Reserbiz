using System;

namespace ReserbizAPP.LIB.Dtos
{
    public class AccountStatementPenaltyItemDetailsDto
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public float Amount { get; set; }
    }
}
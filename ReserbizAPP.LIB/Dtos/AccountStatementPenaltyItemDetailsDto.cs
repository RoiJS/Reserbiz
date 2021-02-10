using System;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Dtos
{
    public class AccountStatementPenaltyItemDetailsDto : IEntityDto
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public float Amount { get; set; }
    }
}
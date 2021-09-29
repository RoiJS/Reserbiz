using System;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Dtos
{
    public class NewAccountStatementDto
    {
        public int ContractId { get; set; }
        public DateTime DueDate { get; set; }
        public float ElectricBill { get; set; }
        public float WaterBill { get; set; }
        public AccountStatementTypeEnum AccountStatementType { get; set; }
    }
}
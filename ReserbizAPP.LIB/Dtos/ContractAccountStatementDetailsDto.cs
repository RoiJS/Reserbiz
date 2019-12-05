using System;

namespace ReserbizAPP.LIB.Dtos
{
    public class ContractAccountStatementDetailsDto
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public float Rate { get; set; }
        public float ElectricBill { get; set; }
        public float WaterBill { get; set; }
        public float Penalty { get; set; }
    }
}
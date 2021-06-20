using System;

namespace ReserbizAPP.LIB.Models
{
    public class AccountStatementWaterAndElectricBillUpdateDto
    {
        public int Id { get; set; }
        public float WaterBillAmount { get; set; }
        public float ElectricBillAmount { get; set; }
        public string UtilityBillsDueDate { get; set; }
    }
}
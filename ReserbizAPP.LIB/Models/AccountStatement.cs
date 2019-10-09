using System.Collections.Generic;

namespace ReserbizAPP.LIB.Models
{
    public class AccountStatement : Entity
    {
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public float Rate { get; set; }
        public float ElectricBill { get; set; }
        public float WaterBill { get; set; }
        public float Penalty { get; set; }
        public List<AccountStatementMiscellaneous> AccountStatementMiscellaneous { get; set; }
    }
}
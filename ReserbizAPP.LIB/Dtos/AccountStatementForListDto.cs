using System;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Dtos
{
    public class AccountStatementForListDto : IEntityDto
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public DateTime DueDate { get; set; }
        public float Rate { get; set; }
        public float ElectricBill { get; set; }
        public float WaterBill { get; set; }
        public DateTime PenaltyNextDueDate { get; set; }
        public float PenaltyTotalAmount { get; set; }
        public float MiscellaneousTotalAmount { get; set; }
        public float AccountStatementTotalAmount { get; set; }
        public float CurrentAmountPaid { get; set; }
        public float CurrentBalance { get; set; }
        public bool IsFullyPaid { get; set; }
        public bool IsDeletable { get; set; }
        public string TenantName { get; set; }
    }
}
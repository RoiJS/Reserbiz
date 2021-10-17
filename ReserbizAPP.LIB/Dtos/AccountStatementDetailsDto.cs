using System;
using System.Collections.Generic;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Dtos
{
    public class AccountStatementDetailsDto
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public DateTime DueDate { get; set; }
        public float Rate { get; set; }
        public int AdvancedPaymentDurationValue { get; set; }
        public int DepositPaymentDurationValue { get; set; }
        public bool ExcludeElectricBill { get; set; }
        public float ElectricBill { get; set; }
        public bool ExcludeWaterBill { get; set; }
        public float WaterBill { get; set; }
        public DateTime PenaltyNextDueDate { get; set; }
        public float PenaltyTotalAmount { get; set; }
        public float MiscellaneousTotalAmount { get; set; }
        public float AccountStatementTotalAmount { get; set; }
        public float CurrentAmountPaid { get; set; }
        public float CurrentBalance { get; set; }
        public bool IsFullyPaid { get; set; }
        public bool isFirstAccountStatement { get; set; }
        public bool IsDeletable { get; set; }
        public float TotalPaidRentalAmount { get; set; }
        public float TotalPaidElectricBills { get; set; }
        public float TotalPaidWaterBills { get; set; }
        public float TotalPaidMiscellaneousFees { get; set; }
        public float TotalPaidPenaltyAmount { get; set; }
        public DateTime LastDateSent { get; set; }
        public AccountStatementTypeEnum AccountStatementType { get; set; }
        public MiscellaneousDueDateEnum MiscellaneousDueDate { get; set; }
        public List<AccountStatementMiscellaneousDetailsDto> AccountStatementMiscellaneous { get; set; }
        public List<AccountStatementPaymentItemDetailsDto> PaymentBreakdowns { get; set; }
        public List<AccountStatementPenaltyItemDetailsDto> PenaltyBreakdowns { get; set; }
    }
}
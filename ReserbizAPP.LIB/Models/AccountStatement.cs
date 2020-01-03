using System;
using System.Collections.Generic;
using System.Linq;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;

namespace ReserbizAPP.LIB.Models
{
    public class AccountStatement : Entity
    {
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public DateTime DueDate { get; set; }
        public float Rate { get; set; }
        public DurationEnum DurationUnit { get; set; }
        public int AdvancedPaymentDurationValue { get; set; }
        public int DepositPaymentDurationValue { get; set; }
        public float ElectricBill { get; set; }
        public float WaterBill { get; set; }
        public List<AccountStatementMiscellaneous> AccountStatementMiscellaneous { get; set; }
        public List<PaymentBreakdown> PaymentBreakdowns { get; set; }
        public float PenaltyValue { get; set; }
        public ValueTypeEnum PenaltyValueType { get; set; }
        public DurationEnum PenaltyAmountPerDurationUnit { get; set; }
        public int PenaltyEffectiveAfterDurationValue { get; set; }
        public DurationEnum PenaltyEffectiveAfterDurationUnit { get; set; }
        public List<PenaltyBreakdown> PenaltyBreakdowns { get; set; }

        private DateTime CurrentDateTime = DateTime.Now;

        // This is to set and simulate the current 
        // date time for unit testing purposes
        public void SetCurrentDateTime(DateTime dateTime)
        {
            CurrentDateTime = dateTime;
        }

        public bool IsFirstAccountStatement
        {
            get
            {
                return IsFirstAccountStatementItem();
            }
        }

        public DateTime PenaltyNextDueDate
        {
            get
            {
                return GetPenaltyNextDueDate();
            }
        }

        public bool IsDueToGeneratePenalty
        {
            get
            {
                return (CurrentDateTime >= PenaltyNextDueDate);
            }
        }

        public float PenaltyAmountValue
        {
            get
            {
                return ConvertPenaltyValue();
            }
        }

        public float PenaltyTotalAmount
        {
            get
            {
                return CalculatePenaltyTotalAmount();
            }
        }

        public float MiscellaneousTotalAmount
        {
            get
            {
                return CalculateMiscellaneousTotalAmount();
            }
        }

        public float AccountStatementTotalAmount
        {
            get
            {
                return CalculateAccountStatementTotalAmount();
            }
        }

        public float CurrentAmountPaid
        {
            get
            {
                return CalculateCurrentAmountPaid();
            }
        }

        public float CurrentBalance
        {
            get
            {
                return (AccountStatementTotalAmount - CurrentAmountPaid);
            }
        }

        public bool IsFullyPaid
        {
            get
            {
                return (CurrentAmountPaid >= AccountStatementTotalAmount);
            }
        }

        public List<AccountStatement> ContractActiveAccountStatements
        {
            get
            {
                var activeAccountStatements = Contract.AccountStatements.Where(a => a.IsActive).OrderBy(a => a.Id).ToList();
                return activeAccountStatements;
            }
        }

        public AccountStatement()
        {
            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            PaymentBreakdowns = new List<PaymentBreakdown>();
        }

        private DateTime GetPenaltyNextDueDate()
        {
            var nextDueDate = new DateTime();

            if (PenaltyBreakdowns.Count == 0)
            {
                var daysBeforePenaltyDueDate = DueDate.CalculateDaysBasedOnDuration(PenaltyEffectiveAfterDurationValue, PenaltyEffectiveAfterDurationUnit);
                var penaltyDueDate = DueDate.AddDays(daysBeforePenaltyDueDate);
                nextDueDate = penaltyDueDate;
            }
            else
            {
                var currentDueDate = PenaltyBreakdowns[PenaltyBreakdowns.Count - 1].DueDate;
                var daysBeforeNextDueDate = currentDueDate.CalculateDaysBasedOnDuration(1, PenaltyAmountPerDurationUnit);
                nextDueDate = currentDueDate.AddDays(daysBeforeNextDueDate);
            }

            return nextDueDate;
        }

        private float ConvertPenaltyValue()
        {
            var penaltyAmountValue = PenaltyValue;

            if (PenaltyValueType == ValueTypeEnum.Percentage)
            {
                penaltyAmountValue = (Rate * (PenaltyValue / 100));
            }

            return penaltyAmountValue;
        }

        private float CalculatePenaltyTotalAmount()
        {
            var totalAmount = PenaltyBreakdowns.Sum(p => p.Amount);
            return totalAmount;
        }

        private float CalculateAccountStatementTotalAmount()
        {
            var totalAmount = ElectricBill + WaterBill + MiscellaneousTotalAmount + PenaltyTotalAmount;

            // Calculate Advance and Deposit amount and add it
            //  to the total amount if the account statement is first in the list.
            if (IsFirstAccountStatement)
            {
                // Calculate the Advance Payment amount and add it to the total amount
                totalAmount += Rate * AdvancedPaymentDurationValue;

                // Calculate the Deposit Payment amount and add it to the total amount
                totalAmount += Rate * DepositPaymentDurationValue;
            }
            else
            {
                totalAmount += Rate;
            }

            return totalAmount;
        }

        private float CalculateMiscellaneousTotalAmount()
        {
            var totalAmount = AccountStatementMiscellaneous.Sum(a => a.Amount);
            return totalAmount;
        }

        private bool IsFirstAccountStatementItem()
        {
            return ContractActiveAccountStatements.Count == 1
                    || (ContractActiveAccountStatements.Count > 0 && ContractActiveAccountStatements[0].Id == Id);
        }

        private float CalculateCurrentAmountPaid()
        {
            var totalAmount = PaymentBreakdowns.Sum(p => p.Amount);
            return totalAmount;
        }
    }
}
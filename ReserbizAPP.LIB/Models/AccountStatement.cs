using System;
using System.Collections.Generic;
using System.Linq;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class AccountStatement
        : Entity, IUserActionTracker
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

        public int? DeletedById { get; set; }
        public Account DeletedBy { get; set; }
        public int? UpdatedById { get; set; }
        public Account UpdatedBy { get; set; }
        public int? CreatedById { get; set; }
        public Account CreatedBy { get; set; }
        public int? DeactivatedById { get; set; }
        public Account DeactivatedBy { get; set; }

        private DateTime CurrentDateTime = DateTime.Now;

        private DateTime NextDueDate
        {
            get
            {
                return GetNextDueDate();
            }
        }

        // This is to set and simulate the current 
        // date time for unit testing purposes
        public void SetCurrentDateTime(DateTime dateTime)
        {
            CurrentDateTime = dateTime;
        }

        public bool IsPenaltySettingActive
        {
            get
            {
                // Generating penalty entry will be based on this settings.
                // For now we will only consider the penalty value.
                // Theres no reason to generate penalty item if penalty value 
                // property is zero.
                return PenaltyValue > 0;
            }
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
                return (PenaltyNextDueDate < NextDueDate
                        && PenaltyNextDueDate <= CurrentDateTime);
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

        public bool IsValidForGeneratingPenalty
        {
            get
            {
                // Generating of penalty item will based on these 3 criteria:
                // (1) Account statement should be due for generating penalty.
                // (2) Account statement should be not yet fully paid.
                // (3) Account statement should not be the first account statement from the list.
                return (IsDueToGeneratePenalty && !IsFullyPaid && !IsFirstAccountStatement);
            }
        }

        public AccountStatement()
        {
            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            PaymentBreakdowns = new List<PaymentBreakdown>();
            PenaltyBreakdowns = new List<PenaltyBreakdown>();
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

        private DateTime GetNextDueDate()
        {
            var daysBeforeNextDue = DueDate.CalculateDaysBasedOnDuration(1, DurationUnit);
            var accountStatementNextDueDate = DueDate.AddDays(daysBeforeNextDue);
            return accountStatementNextDueDate;
        }

        private float ConvertPenaltyValue()
        {
            var penaltyAmountValue = PenaltyValue;

            if (PenaltyValueType == ValueTypeEnum.Percentage)
            {
                penaltyAmountValue = (float)Math.Round((Rate * (PenaltyValue / 100)), 2, MidpointRounding.AwayFromZero);
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
            var activeAccountStatements = Contract.AccountStatements.Where(a => a.IsActive).OrderBy(a => a.Id).ToList();

            return activeAccountStatements.Count == 1
                    || (activeAccountStatements.Count > 0 && activeAccountStatements[0].Id == Id);
        }

        private float CalculateCurrentAmountPaid()
        {
            var totalAmount = PaymentBreakdowns.Sum(p => p.Amount);
            return totalAmount;
        }
    }
}
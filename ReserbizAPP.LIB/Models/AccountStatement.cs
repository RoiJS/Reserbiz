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
        public DateTime UtilityBillsDueDate { get; set; }
        public float ElectricBill { get; set; }
        public float WaterBill { get; set; }
        public List<AccountStatementMiscellaneous> AccountStatementMiscellaneous { get; set; }
        public List<PaymentBreakdown> PaymentBreakdowns { get; set; }
        public float PenaltyValue { get; set; }
        public ValueTypeEnum PenaltyValueType { get; set; }
        public DurationEnum PenaltyAmountPerDurationUnit { get; set; }
        public int PenaltyEffectiveAfterDurationValue { get; set; }
        public DurationEnum PenaltyEffectiveAfterDurationUnit { get; set; }
        public MiscellaneousDueDateEnum MiscellaneousDueDate { get; set; }


        // This will record the last date when the sms notification has been sent.
        // We will only allow to send one sms notification once per day
        // this is because there is a charge sending sms using the SMS
        // API Service that the application is using.
        public DateTime SMSNotificationLastDateSent { get; set; }
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

        public float RentalTotalAmount
        {
            get
            {
                return CalculateTotalRentalAmount();
            }
        }

        public float UtilityBillsAmount
        {
            get
            {
                return WaterBill + ElectricBill;
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

        public float AccountStatementTotalRentalFeeAmount
        {
            get
            {
                return CalculateRentalFee();
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
                return ValidateIfFullyPaid();
            }
        }

        public bool IsRentalFeeFullyPaid
        {
            get
            {
                return (CurrentAmountPaid >= AccountStatementTotalRentalFeeAmount);
            }
        }

        public bool IsValidForGeneratingPenalty
        {
            get
            {
                // Generating of penalty item will based on these 3 criteria:
                // (1) Account statement should be due for generating penalty.
                // (2) Account statement rental fees has not been full paid yet.
                // (3) Account statement should not be the first account statement from the list.
                return (IsDueToGeneratePenalty && !IsRentalFeeFullyPaid && !IsFirstAccountStatement);
            }
        }

        public float CalculatedDepositAmount
        {
            get
            {
                return CalculateDepositPaymentAmount();
            }
        }


        public bool AllowSentSMSNotificationForTheDay
        {
            get
            {
                return (SMSNotificationLastDateSent != null && CurrentDateTime.Date >= SMSNotificationLastDateSent);
            }
        }

        public bool IsDeletable
        {
            get
            {
                return IsAccountStatementDeletable();
            }
        }

        public float TotalPaidRentalAmount
        {
            get
            {
                return CalculateTotalPaidRentalAmount();
            }
        }

        public float TotalPaidUtilityBills
        {
            get
            {
                return CalculateTotalPaidElectricBillAmount() + CalculateTotalPaidWaterBillAmount();
            }
        }

        public float TotalPaidElectricBills
        {
            get
            {
                return CalculateTotalPaidElectricBillAmount();
            }
        }

        public float TotalPaidWaterBills
        {
            get
            {
                return CalculateTotalPaidWaterBillAmount();
            }
        }

        public float TotalPaidMiscellaneousFees
        {
            get
            {
                return CalculateTotalPaidMiscellaneousBillAmount();
            }
        }

        public float TotalPaidPenaltyAmount
        {
            get
            {
                return CalculateTotalPaidPenaltyAmount();
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
            var totalAmount = PenaltyBreakdowns
                                .Where(p => p.IsActive == true && p.IsDelete == false)
                                .Sum(p => p.Amount);
            return totalAmount;
        }

        private float CalculateRentalFee()
        {
            var totalAmount = 0.0f;

            // Calculate Advance and Deposit amount and add it
            //  to the total amount if the account statement is first in the list.
            if (IsFirstAccountStatement)
            {
                totalAmount += Rate;

                // Calculate the Deposit Payment amount and add it to the total amount
                totalAmount += CalculatedDepositAmount;
            }
            else
            {
                totalAmount += Rate;
            }

            // Check if the Miscellaneous Due Date setting is set to SameWithRentalDueDate
            // then we add the miscellaneous total amount with the Rental Amount
            if (MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithRentalDueDate)
            {
                totalAmount += MiscellaneousTotalAmount;
            }

            return totalAmount;
        }

        private float CalculateAccountStatementTotalAmount()
        {
            var totalAmount = ElectricBill + WaterBill + MiscellaneousTotalAmount + PenaltyTotalAmount;

            // Calculate Advance and Deposit amount and add it
            //  to the total amount if the account statement is first in the list.
            if (IsFirstAccountStatement)
            {
                totalAmount += Rate;

                // Calculate the Deposit Payment amount and add it to the total amount
                totalAmount += CalculatedDepositAmount;
            }
            else
            {
                totalAmount += Rate;
            }

            return totalAmount;
        }

        private float CalculateTotalRentalAmount()
        {
            var totalAmount = 0.0f;

            // Calculate Advance and Deposit amount and add it
            //  to the total amount if the account statement is first in the list.
            if (IsFirstAccountStatement)
            {
                totalAmount += Rate;

                // Calculate the Deposit Payment amount and add it to the total amount
                totalAmount += CalculatedDepositAmount;
            }
            else
            {
                totalAmount += Rate;
            }

            return totalAmount;
        }

        private float CalculateTotalPaidRentalAmount()
        {
            var totalAmount = PaymentBreakdowns
                                .Where(p => p.PaymentForType == PaymentForTypeEnum.Rental)
                                .Select(p => p.Amount)
                                .Sum();

            return totalAmount;
        }

        private float CalculateTotalPaidElectricBillAmount()
        {
            var totalAmount = PaymentBreakdowns
                                .Where(p => p.PaymentForType == PaymentForTypeEnum.ElectricBill)
                                .Select(p => p.Amount)
                                .Sum();

            return totalAmount;
        }

        private float CalculateTotalPaidWaterBillAmount()
        {
            var totalAmount = PaymentBreakdowns
                                .Where(p => p.PaymentForType == PaymentForTypeEnum.WaterBill)
                                .Select(p => p.Amount)
                                .Sum();

            return totalAmount;
        }

        private float CalculateTotalPaidMiscellaneousBillAmount()
        {
            var totalAmount = PaymentBreakdowns
                                .Where(p => p.PaymentForType == PaymentForTypeEnum.MiscellaneousFee)
                                .Select(p => p.Amount)
                                .Sum();

            return totalAmount;
        }

        private float CalculateTotalPaidPenaltyAmount()
        {
            var totalAmount = PaymentBreakdowns
                                .Where(p => p.PaymentForType == PaymentForTypeEnum.Penalty)
                                .Select(p => p.Amount)
                                .Sum();

            return totalAmount;
        }

        private float CalculateMiscellaneousTotalAmount()
        {
            var totalAmount = AccountStatementMiscellaneous.Sum(a => a.Amount);
            return totalAmount;
        }

        private bool IsFirstAccountStatementItem()
        {
            var activeAccountStatements = Contract.AccountStatements
                                                .Where(a => a.IsActive && a.IsDelete == false)
                                                .OrderBy(a => a.DueDate)
                                                .ToList();

            return activeAccountStatements.Count == 1
                    || (activeAccountStatements.Count > 0 && activeAccountStatements[0].Id == Id);
        }

        private bool IsAccountStatementDeletable()
        {
            var lastAccountStatement = Contract.AccountStatements
                                                .Where(a => a.IsActive && a.IsDelete == false)
                                                .OrderByDescending(a => a.DueDate)
                                                .FirstOrDefault();

            // Last account statement is deletable
            return (lastAccountStatement != null && lastAccountStatement.Id == Id);
        }

        private float CalculateCurrentAmountPaid()
        {
            var totalAmount = PaymentBreakdowns.Sum(p => p.Amount);
            return totalAmount;
        }

        private float CalculateDepositPaymentAmount()
        {
            return Rate * DepositPaymentDurationValue;
        }

        private bool ValidateIfFullyPaid()
        {
            var isRentalFullyPaid = true;
            var isElectricBillsPaid = true;
            var isWaterBillsPaid = true;
            var isMisellaneousFeesPaid = true;
            var isPenaltyAmountPaid = true;

            if (Contract.IncludeRentalFee)
            {
                isRentalFullyPaid = TotalPaidRentalAmount >= RentalTotalAmount;
            }

            if (Contract.IncludeUtilityBills)
            {
                isElectricBillsPaid = CalculateTotalPaidElectricBillAmount() >= ElectricBill;

                isWaterBillsPaid = CalculateTotalPaidWaterBillAmount() >= WaterBill;
            }

            if (Contract.IncludeMiscellaneousFees)
            {
                isMisellaneousFeesPaid = TotalPaidMiscellaneousFees >= MiscellaneousTotalAmount;
            }

            if (Contract.IncludePenaltyAmount)
            {
                isPenaltyAmountPaid = TotalPaidPenaltyAmount >= PenaltyTotalAmount;
            }


            return isRentalFullyPaid && isElectricBillsPaid && isWaterBillsPaid && isMisellaneousFeesPaid && isPenaltyAmountPaid;
        }
    }
}
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
        public bool ExcludeElectricBill { get; set; }
        public float ElectricBill { get; set; }
        public bool ExcludeWaterBill { get; set; }
        public float WaterBill { get; set; }
        public List<AccountStatementMiscellaneous> AccountStatementMiscellaneous { get; set; }
        public List<PaymentBreakdown> PaymentBreakdowns { get; set; }
        public float PenaltyValue { get; set; }
        public ValueTypeEnum PenaltyValueType { get; set; }
        public DurationEnum PenaltyAmountPerDurationUnit { get; set; }
        public int PenaltyEffectiveAfterDurationValue { get; set; }
        public DurationEnum PenaltyEffectiveAfterDurationUnit { get; set; }
        public MiscellaneousDueDateEnum MiscellaneousDueDate { get; set; }

        // This will determine if the miscellaneous fees amount will
        // be included when checking for penalty.
        // If this setting is activated and the penalty value type is set
        // to "Percentage", the calculation for penalty value will be based from
        // Rental fees + Miscellaneous fees.
        public bool IncludeMiscellaneousCheckAndCalculateForPenalty { get; set; }


        // This will record the last date when the sms notification has been sent.
        // We will only allow to send one sms notification once per day
        // this is because there is a charge sending sms using the SMS
        // API Service that the application is using.
        public DateTime SMSNotificationLastDateSent { get; set; }

        public AccountStatementTypeEnum AccountStatementType { get; set; } = AccountStatementTypeEnum.RentalBill;

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

        public float AccountStatementTotalRentalFeeAmountForPenaltyCheck
        {
            get
            {
                return CalculateRentalFeeForPenaltyCheck();
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

        public bool IsRentalFeeFullyPaidForPenaltyCheck
        {
            get
            {
                var currentRentalPaidAmount = TotalPaidRentalAmount;

                if (IncludeMiscellaneousCheckAndCalculateForPenalty && MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithRentalDueDate)
                {
                    currentRentalPaidAmount += TotalPaidMiscellaneousFees;
                }

                // Checking for this is based on the current paid rental amount against the expected rental fee amount.
                // If IncludeMiscellaneousCheckAndCalculateForPenalty is true and miscellaneous due date is same with rental fee, 
                // then we must include the miscellaneous fees when checking this.  
                return (currentRentalPaidAmount >= AccountStatementTotalRentalFeeAmountForPenaltyCheck);
            }
        }

        public bool IsRentalFeeFullyPaid
        {
            get
            {
                var currentRentalPaidAmount = TotalPaidRentalAmount;

                if (MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithRentalDueDate)
                {
                    currentRentalPaidAmount += TotalPaidMiscellaneousFees;
                }

                // Checking for this is based on the current paid rental amount against the expected rental fee amount.
                return (currentRentalPaidAmount >= CalculateRentalFee());
            }
        }

        public bool IsUtilityFeeFullyPaid
        {
            get
            {
                var currentUtilityBillPaidAmount = TotalPaidUtilityBills;
                var expectedUtilityBillAmount = UtilityBillsAmount;

                if (MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithUtilityBillDueDate)
                {
                    currentUtilityBillPaidAmount += TotalPaidMiscellaneousFees;
                    expectedUtilityBillAmount += MiscellaneousTotalAmount;
                }

                return (currentUtilityBillPaidAmount >= expectedUtilityBillAmount);
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
                return (IsDueToGeneratePenalty && !IsRentalFeeFullyPaidForPenaltyCheck && !IsFirstAccountStatement);
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
                var basedAmount = Rate;

                // If the miscellaneous fee is set to be included when checking and calculating for 
                // penalty then calculate the penalty percentage amount based on the rental fee amount + miscellaneous fees
                if (IncludeMiscellaneousCheckAndCalculateForPenalty)
                {
                    basedAmount = Rate + MiscellaneousTotalAmount;
                }

                penaltyAmountValue = (float)Math.Round((basedAmount * (PenaltyValue / 100)), 2, MidpointRounding.AwayFromZero);
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

        private float CalculateRentalFeeForPenaltyCheck()
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

            // Check if the Miscellaneous Due Date setting is set to SameWithRentalDueDate and if 
            // the miscellaneous is set to be included on the checking of penalty, 
            // then we add the miscellaneous total amount with the Rental Amount.
            if (IncludeMiscellaneousCheckAndCalculateForPenalty && MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithRentalDueDate)
            {
                totalAmount += MiscellaneousTotalAmount;
            }

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

            // Check if the Miscellaneous Due Date setting is set to SameWithRentalDueDate and if
            // then we add the miscellaneous total amount with the Rental Amount.
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
                                                .Where(
                                                    a => a.IsActive &&
                                                    a.IsDelete == false
                                                )
                                                .OrderBy(a => a.DueDate)
                                                .ToList();

            return activeAccountStatements.Count == 1
                    || (activeAccountStatements.Count > 0 && activeAccountStatements[0].Id == Id);
        }

        private bool IsAccountStatementDeletable()
        {
            // Only get the last rental bill statement of account.
            var lastAccountStatement = Contract.AccountStatements
                                                .Where(
                                                    a => a.AccountStatementType == AccountStatementTypeEnum.RentalBill &&
                                                    a.IsActive &&
                                                    a.IsDelete == false
                                                )
                                                .OrderByDescending(a => a.DueDate)
                                                .FirstOrDefault();

            // Deletable statement of account is deletable if:
            // (1) It is a latest rental statement of account OR
            // (2) It is a utility bill statement of account
            return (AccountStatementType == AccountStatementTypeEnum.UtilityBill || 
                (AccountStatementType == AccountStatementTypeEnum.RentalBill && lastAccountStatement != null && lastAccountStatement.Id == Id));
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
            if (AccountStatementType == AccountStatementTypeEnum.RentalBill)
            {
                var isPenaltyAmountPaid = TotalPaidPenaltyAmount >= PenaltyTotalAmount;
                return IsRentalFeeFullyPaid && isPenaltyAmountPaid;
            }

            if (AccountStatementType == AccountStatementTypeEnum.UtilityBill)
            {
                return IsUtilityFeeFullyPaid;
            }

            return true;
        }
    }
}
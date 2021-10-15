using System;
using System.Collections.Generic;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class Term
        : Entity, IUserActionTracker
    {
        // User-defined term code
        public string Code { get; set; }

        // Name of the term.
        public string Name { get; set; }

        public int? TermParentId { get; set; }

        public virtual Term TermParent { get; set; }

        public List<Term> TermChildren { get; set; }


        public int SpaceTypeId { get; set; }

        public SpaceType SpaceType { get; set; }

        // Rate value for the selected Space type.
        public float Rate { get; set; }

        // Number of space occupants
        public int MaximumNumberOfOccupants { get; set; }

        // Payment duration based on DurationEnum (Daily = 0, Weekly = 1, Monthly = 2, Quarterly = 3, Yearly = 4)
        public DurationEnum DurationUnit { get; set; } = DurationEnum.Month;

        // Advanced payment value based on the selected duration (Ex. 2 Months)
        public int AdvancedPaymentDurationValue { get; set; }

        // Deposit payment value based on the selected duration (Ex. 2 Months) 
        public int DepositPaymentDurationValue { get; set; }

        // Exluding Monthly Electric Bill from the payment
        public bool ExcludeElectricBill { get; set; }

        // Amount of Electric bill
        public float ElectricBillAmount { get; set; }

        // Exluding Monthly Water Bill from the payment
        public bool ExcludeWaterBill { get; set; }

        // Amount of WaterBill
        public float WaterBillAmount { get; set; }

        // List of Miscellaneous Fees
        public List<TermMiscellaneous> TermMiscellaneous { get; set; }

        // Penalty amount value
        public float PenaltyValue { get; set; }

        // Penalty amount value type (Fixed = 0, Percentage = 1)
        public ValueTypeEnum PenaltyValueType { get; set; } = ValueTypeEnum.Fixed;

        // Penalty amount per duration
        public DurationEnum PenaltyAmountPerDurationUnit { get; set; } = DurationEnum.Day;

        // Penalty will be effective after duration value
        public int PenaltyEffectiveAfterDurationValue { get; set; }

        // Penalty will be effective after duration unit based on DurationEnum value
        public DurationEnum PenaltyEffectiveAfterDurationUnit { get; set; } = DurationEnum.Day;

        public int GenerateAccountStatementDaysBeforeValue { get; set; }
        
        public bool AutoSendNewAccountStatement { get; set; }

        public MiscellaneousDueDateEnum MiscellaneousDueDate { get; set; }

        // This will determine if the miscellaneous fees amount will
        // be included when checking for penalty.
        // If this setting is activated and the penalty value type is set
        // to "Percentage", the calculation for penalty value will be based from
        // Rental fees + Miscellaneous fees.
        // This property is also available on class AccountStatement.
        public bool IncludeMiscellaneousCheckAndCalculateForPenalty { get; set; }

        public List<Contract> Contracts { get; set; }

        // Term is tagged as deletable if it has 
        // no any children copy
        public bool IsDeletable
        {
            get
            {
                return (this.TermChildren.Count == 0);
            }
        }

        public float PenaltyAmount
        {
            get
            {
                var amount = PenaltyValue;

                // If the penalty value type is set to "Percentage",
                // We will have to compute the amount based on the rate
                if (PenaltyValueType == ValueTypeEnum.Percentage)
                {
                    amount = (float)Math.Round((Rate * (PenaltyValue / 100)), 2, MidpointRounding.AwayFromZero);
                }

                return amount;
            }
        }

        public Term()
        {
            Contracts = new List<Contract>();
            TermChildren = new List<Term>();
        }

        public int? DeletedById { get; set; }
        public Account DeletedBy { get; set; }
        public int? UpdatedById { get; set; }
        public Account UpdatedBy { get; set; }
        public int? CreatedById { get; set; }
        public Account CreatedBy { get; set; }
        public int? DeactivatedById { get; set; }
        public Account DeactivatedBy { get; set; }
    }
}
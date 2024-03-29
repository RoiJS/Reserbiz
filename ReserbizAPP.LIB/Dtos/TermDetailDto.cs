using System.Collections.Generic;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Dtos
{
    public class TermDetailDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public int TermParentId { get; set; }

        public int SpaceTypeId { get; set; }

        public bool IsDeletable { get; set; }

        public SpaceTypeTermDetailDto SpaceType { get; set; }

        // Rate value for the selected Space type.
        // Initially set to value based from the selected Space Type.
        public float Rate { get; set; }

        // Number of space occupants
        public int MaximumNumberOfOccupants { get; set; }

        // Payment duration based on DurationEnum (Daily = 0, Weekly = 1, Monthly = 2, Quarterly = 3, Yearly = 4)
        public DurationEnum DurationUnit { get; set; } = DurationEnum.Month;

        public string DurationUnitText { get; set; }

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

        // Penalty amount value
        public float PenaltyValue { get; set; }

        // Computed penalty amount
        public float PenaltyAmount { get; set; }

        // Penalty amount value type (Fixed = 0, Percentage = 1)
        public ValueTypeEnum PenaltyValueType { get; set; } = ValueTypeEnum.Fixed;

        // Penalty amount per duration
        public DurationEnum PenaltyAmountPerDurationUnit { get; set; } = DurationEnum.Day;

        public string PenaltyAmountPerDurationUnitText { get; set; }

        // Penalty will be effective after duration value
        public int PenaltyEffectiveAfterDurationValue { get; set; }

        // Penalty will be effective after duration unit based on DurationEnum value
        public DurationEnum PenaltyEffectiveAfterDurationUnit { get; set; } = DurationEnum.Day;

        public string PenaltyEffectiveAfterDurationUnitText { get; set; }

        public int GenerateAccountStatementDaysBeforeValue { get; set; }

        public bool AutoSendNewAccountStatement { get; set; }

        public MiscellaneousDueDateEnum MiscellaneousDueDate { get; set; }
        public bool IncludeMiscellaneousCheckAndCalculateForPenalty { get; set; }
        public List<TermMiscellaneousDetailDto> TermMiscellaneous { get; set; }
    }
}
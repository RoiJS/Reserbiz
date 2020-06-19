using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers.Custom_Validations;

namespace ReserbizAPP.LIB.Dtos
{
    public class TermForCreationDto
    {
        [Required]
        [MaxLength(20, ErrorMessage = "{0} must not exceed to {1}.")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SpaceTypeId { get; set; }

        // Rate value for the selected Space type.
        // Initially set to value based from the selected Space Type.
        [Required]
        public float Rate { get; set; }

        // Number of space occupants
        [Required]
        public int MaximumNumberOfOccupants { get; set; }

        // Payment duration based on DurationEnum (Daily = 0, Weekly = 1, Monthly = 2, Quarterly = 3, Yearly = 4)
        [Required]
        [EnumDataType(typeof(DurationEnum))]
        public DurationEnum DurationUnit { get; set; } = DurationEnum.Month;

        // Advanced payment value based on the selected duration (Ex. 2 Months)  
        [Required]
        [DurationValueRange("DurationUnit", ErrorMessage = "Value for duration unit of {0} shall not exceed {1}.")]
        public int AdvancedPaymentDurationValue { get; set; }

        // Deposit payment value based on the selected duration (Ex. 2 Months) 
        [Required]
        [DurationValueRange("DurationUnit", ErrorMessage = "Value for duration unit of {0} shall not exceed {1}.")]
        public int DepositPaymentDurationValue { get; set; }

        // Exluding Monthly Electric Bill from the payment
        [Required]
        public bool ExcludeElectricBill { get; set; }

        // Amount of Electric bill
        [Required]
        // [MinLength(0, ErrorMessage = "Electric Bill must not be less than zero.")]
        [BillAmountState("ExcludeElectricBill", ErrorMessage = "Value must be 0 when property {0} is set to false.")]
        public float ElectricBillAmount { get; set; }

        // Exluding Monthly Water Bill from the payment
        [Required]
        public bool ExcludeWaterBill { get; set; }

        // Amount of WaterBill
        [Required]
        // [MinLength(0, ErrorMessage = "Water Bill must not be less than zero.")]
        [BillAmountState("ExcludeWaterBill", ErrorMessage = "Value must be 0 when property {0} is set to false.")]
        public float WaterBillAmount { get; set; }

        // Penalty amount value
        [Required]
        public float PenaltyValue { get; set; }

        // Penalty amount value type (Fixed = 0, Percentage = 1)
        [Required]
        [EnumDataType(typeof(ValueTypeEnum))]
        public ValueTypeEnum PenaltyValueType { get; set; } = ValueTypeEnum.Fixed;

        // Penalty amount per duration
        [Required]
        [EnumDataType(typeof(DurationEnum))]
        public DurationEnum PenaltyAmountPerDurationUnit { get; set; } = DurationEnum.Day;

        // Penalty will be effective after duration value
        [Required]
        [DurationValueRange("PenaltyEffectiveAfterDurationUnit", ErrorMessage = "Value for duration unit of {0} shall not exceed {1}.")]
        public int PenaltyEffectiveAfterDurationValue { get; set; }

        // Penalty will be effective after duration unit based on DurationEnum value
        [Required]
        [EnumDataType(typeof(DurationEnum))]
        public DurationEnum PenaltyEffectiveAfterDurationUnit { get; set; } = DurationEnum.Day;

        public List<TermMiscellaneousForCreationDto> TermMiscellaneous { get; set; }
    }
}
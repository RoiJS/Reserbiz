using System.Collections.Generic;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class Term : Entity, ICustomerRef
    {
        // Name of the contract.
        public string Name { get; set; }

        // Rate value for the selected Space type.
        // Initially set to value based from the selected Space Type.
        public float Rate { get; set; }

        // Payment duration based on DurationEnum (Daily = 0, Weekly = 1, Monthly = 2, Quarterly = 3, Yearly = 4)
        public DurationEnum DurationUnit { get; set; } = DurationEnum.Month;

        // Advanced payment value based on the selected duration (Ex. 2 Months)  
        public int AdvancedPaymentDurationValue { get; set; }

        // Deposit payment value based on the selected duration (Ex. 2 Months) 
        public int DepositPaymentDurationValue { get; set; }

        // Exluding Monthly Electric Bill from the payment
        public bool ExcludeElectricBill { get; set; }

        // Exluding Monthly Water Bill from the payment
        public bool ExcludeWaterBill { get; set; }

        // List of Miscellaneous Fees
        public List<Miscellaneous> Miscellaneous { get; set; }

        // Penalty amount value
        public float PenaltyValue { get; set; }

        // Penalty amount value type (Fixed = 0, Percentage = 1)
        public ValueTypeEnum PenaltyValueType { get; set; } = ValueTypeEnum.Fixed;

        // Penalty amount per duration
        public DurationEnum PenaltyAmountPerDuration { get; set; } = DurationEnum.Day;

        // Penalty will be effective after duration value
        public int PenaltyEffectiveAfterDurationValue { get; set; }

        // Penalty will be effective after duration unit based on DurationEnum value
        public DurationEnum PenaltyEffectiveAfterDurationUnit { get; set; } = DurationEnum.Day;


        #region "Reference Properties"

        public int SpaceTypeId { get; set; }
        public SpaceType SpaceType { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        #endregion

    }
}
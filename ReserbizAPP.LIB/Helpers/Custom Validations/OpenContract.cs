using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Helpers.Custom_Validations
{
    public class OpenContract : ValidationAttribute
    {
        private readonly string _durationUnitPropertyName;
        private readonly string _durationValuePropertyName;

        public OpenContract(string durationUnitPropertyName, string durationValuerPropertyName)
        {
            _durationValuePropertyName = durationValuerPropertyName;
            _durationUnitPropertyName = durationUnitPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var durationUnitPropertyInfo = validationContext.ObjectType.GetProperty(_durationUnitPropertyName);
            var durationValuePropertyInfo = validationContext.ObjectType.GetProperty(_durationValuePropertyName);

            var durationUnitPropertyInfoValue = (DurationEnum)durationUnitPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            var durationValuePropertyInfoValue = (int)durationValuePropertyInfo.GetValue(validationContext.ObjectInstance, null);

            var currentValue = Convert.ToBoolean(value);

            // Validates if open contract property where this custom
            // validation should be applied is set to true, the duration unit must have the value of "None" 
            // and duration value must be 0.
            if (currentValue && (durationUnitPropertyInfoValue != DurationEnum.None || durationValuePropertyInfoValue > 0))
                return new ValidationResult(this.FormatErrorMessage());

            return null;
        }

        private string FormatErrorMessage()
        {
            return String.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, new object[] { _durationUnitPropertyName, DurationEnum.None, _durationValuePropertyName, default(int)});
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Helpers.Custom_Validations
{
    public class DurationValueRange : ValidationAttribute
    {
        private readonly string _propertyName;
        private int _maxValue;

        public DurationValueRange(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_propertyName);
            var propertyValue = (DurationEnum)property.GetValue(validationContext.ObjectInstance, null);
            var currentValue = Convert.ToInt32(value);
            var validationResult = ValidateDurationValueRange(propertyValue, currentValue);

            _maxValue = validationResult.maxValue;

            if (!validationResult.result)
                return new ValidationResult(this.FormatErrorMessage(propertyValue.ToString()));

            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, new object[] { name, _maxValue });
        }

        private ValidateDurationRangeResult ValidateDurationValueRange(DurationEnum propDurationType, int propertyValue)
        {
            var validationResult = new ValidateDurationRangeResult();

            if (propDurationType == DurationEnum.Day 
                && (propertyValue < 0 || propertyValue > SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE))
            {
                validationResult.result = false;
                validationResult.maxValue = SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE;
            }
            else if (propDurationType == DurationEnum.Month 
                && (propertyValue < 0 || propertyValue > SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE))
            {
                validationResult.result = false;
                validationResult.maxValue = SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE;
            }
            else if (propDurationType == DurationEnum.Quarter 
                && (propertyValue < 0 || propertyValue > SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE))
            {
                validationResult.result = false;
                validationResult.maxValue = SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE;
            }
            else if (propDurationType == DurationEnum.Week 
                && (propertyValue < 0 || propertyValue > SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE))
            {
                validationResult.result = false;
                validationResult.maxValue = SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE;
            }
            else if (propDurationType == DurationEnum.Year 
                && (propertyValue < 0 || propertyValue > SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE))
            {
                validationResult.result = false;
                validationResult.maxValue = SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE;
            }
            else
            {
                validationResult.result = true;
            }

            return validationResult;
        }
    }
}
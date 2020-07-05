using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ReserbizAPP.LIB.Helpers.Custom_Validations
{
    public class BillAmountState : ValidationAttribute
    {
        private readonly string _propertyName;

        public BillAmountState(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_propertyName);
            var propertyValue = Convert.ToBoolean(property.GetValue(validationContext.ObjectInstance, null));

            if (propertyValue == true && Convert.ToInt32(value) > 0)
            {
                return new ValidationResult(this.FormatErrorMessage(_propertyName));
            }
            return null;
        }
    }
}
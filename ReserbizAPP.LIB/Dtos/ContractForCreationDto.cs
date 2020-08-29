using System;
using System.ComponentModel.DataAnnotations;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers.Custom_Validations;

namespace ReserbizAPP.LIB.Dtos
{
    public class ContractForCreationDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "{0} must not exceed to {1}.")]
        public string Code { get; set; }

        [Required]
        public int TenantId { get; set; }

        [Required]
        public int TermId { get; set; }

        [Required]
        public TermForCreationDto Term { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        [Required]
        [OpenContract("DurationUnit", "DurationValue", ErrorMessage = "{0} must be set to {1} and {2} must be {3}.")]
        public bool IsOpenContract { get; set; }

        [Required]
        [EnumDataType(typeof(DurationEnum))]
        public DurationEnum DurationUnit { get; set; }

        [DurationValueRange("DurationUnit", ErrorMessage = "Value for duration unit of {0} shall be not exceed {1}.")]
        public int DurationValue { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace ReserbizAPP.LIB.Dtos
{
    public class TermMiscellaneousForCreationDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "{0} must not exceed to {1}.")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "{0} must not exceed to {1}.")]
        public string Description { get; set; }

        [Required]
        public float Amount { get; set; }
    }
}
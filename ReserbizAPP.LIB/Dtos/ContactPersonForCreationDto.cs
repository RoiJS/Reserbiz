using System.ComponentModel.DataAnnotations;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Dtos
{
    public class ContactPersonForCreationDto
    {
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public GenderEnum Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Relation { get; set; }
    }
}
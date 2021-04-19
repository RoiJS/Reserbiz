using System.ComponentModel.DataAnnotations;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Dtos
{
    public class AccountForRegisterDto
    {
        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public GenderEnum Gender { get; set; }
        
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 16 characters")]
        public string Password { get; set; }
    }
}
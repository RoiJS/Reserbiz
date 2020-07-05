using System.ComponentModel.DataAnnotations;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Dtos
{
    public class AccountForUpdateDto
    {
        [Required]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace ReserbizAPP.LIB.Dtos
{
    public class DemoForRegisterDto
    {
        [Required]
        public string Name { get; set; }
        public DateTime DateJoined { get; set; }
        public string ContactNumber { get; set; }
    }
}
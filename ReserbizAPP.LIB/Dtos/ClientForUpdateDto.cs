using System;
using System.ComponentModel.DataAnnotations;

namespace ReserbizAPP.LIB.Dtos
{
    public class ClientForUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DbName { get; set; }
        public string Description { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public DateTime DateJoined { get; set; }
    }
}
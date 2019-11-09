using System;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Dtos
{
    public class AccountForDetailDto
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public GenderEnum Gender { get; set; }

        public string Username { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
using System;

namespace ReserbizAPP.LIB.Dtos
{
    public class AccountForListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
using System.Collections.Generic;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.models;

namespace ReserbizAPP.LIB.Models
{
    public class Tenant : Person
    {
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<ContactPerson> ContactPersons { get; set; }

    }
}
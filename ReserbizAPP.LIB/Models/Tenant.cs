using System.Collections.Generic;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.models;

namespace ReserbizAPP.LIB.Models
{
    public class Tenant 
        : Person, IUserActionTracker
    {
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<ContactPerson> ContactPersons { get; set; }
        public List<Contract> Contracts { get; set; }

        public int? DeletedById { get; set; }
        public virtual Account DeletedBy { get; set; }
        public int? UpdatedById { get; set; }
        public virtual Account UpdatedBy { get; set; }
        public int? CreatedById { get; set; }
        public virtual Account CreatedBy { get; set; }
        public int? DeactivatedById { get; set; }
        public virtual Account DeactivatedBy { get; set; }
    }
}
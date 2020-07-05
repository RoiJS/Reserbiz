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


        public bool IsDeletable
        {
            get
            {
                // We can determine if tenant can be 
                // removed if it has not been bind to any contracts

                // I don't think we'll have to check if tenant has contact person
                // to identify if it can be removed or not. 
                return Contracts.Count == 0;
            }
        }

        public Tenant()
        {
            Contracts = new List<Contract>();
        }

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
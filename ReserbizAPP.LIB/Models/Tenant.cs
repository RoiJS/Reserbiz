using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.models;

namespace ReserbizAPP.LIB.Models
{
    public class Tenant : Person, ICustomerRef
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }

        #region "Reference Properties"
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        
        #endregion

    }
}
using System;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.models;

namespace ReserbizAPP.LIB.Models
{
    public class Account : Person, ICustomerRef
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }


        #region "Reference Properties"

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        #endregion

    }
}
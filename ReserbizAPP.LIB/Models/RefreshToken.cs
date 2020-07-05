using System;

namespace ReserbizAPP.LIB.Models
{
    public class RefreshToken : Entity
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int AccountId { get; set; }
        public Account User { get; set; }
    }
}
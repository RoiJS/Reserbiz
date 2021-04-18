using ReserbizAPP.LIB.models;

namespace ReserbizAPP.LIB.Models
{
    public class UserAccount : Person
    {
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
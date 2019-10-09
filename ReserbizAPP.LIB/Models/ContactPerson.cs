using ReserbizAPP.LIB.models;

namespace ReserbizAPP.LIB.Models
{
    public class ContactPerson : Person
    {
        public int TenantId { get; set; }

        public Tenant Tenant { get; set; }

        public string ContactNumber { get; set; }

    }
}
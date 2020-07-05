using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Dtos
{
    public class TenantForListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeletable { get; set; }
    }
}
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface ICustomerRef
    {
        int CustomerId { get; set; }
        Customer Customer { get; set; }

    }
}
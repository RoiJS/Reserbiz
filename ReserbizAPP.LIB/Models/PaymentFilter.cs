using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class PaymentFilter : IPaymentFilter
    {
        public PaymentForTypeEnum PaymentForType { get; set; }
        public SortOrderEnum? SortOrder { get; set; }
    }
}
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IPaymentFilter : IEntityFilter
    {
        PaymentForTypeEnum PaymentForType { get; set; }
    }
}
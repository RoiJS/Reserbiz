namespace ReserbizAPP.LIB.Interfaces
{
    public interface IPaymentPaginationListDto : IEntityPaginationListDto
    {
        double TotalAmount { get; set; }
        double TotalAmountFromDeposit { get; set; }
        double DepositedAmountBalance { get; set; }
        double SuggestedAmountForPayment { get; set; }
    }
}
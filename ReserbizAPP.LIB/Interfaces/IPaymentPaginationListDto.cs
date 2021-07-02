namespace ReserbizAPP.LIB.Interfaces
{
    public interface IPaymentPaginationListDto : IEntityPaginationListDto
    {
        double TotalAmount { get; set; }
        double TotalAmountFromDeposit { get; set; }
        double DepositedAmountBalance { get; set; }
        double SuggestedRentalAmount { get; set; }
        double SuggestedElectricBillAmount { get; set; }
        double SuggestedWaterBillAmount { get; set; }
        double SuggestedMiscelleneousAmount { get; set; }
        double SuggestedPenaltyAmount { get; set; }
    }
}
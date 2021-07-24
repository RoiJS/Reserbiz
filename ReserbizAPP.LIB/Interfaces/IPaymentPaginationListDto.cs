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

        double TotalExpectedRentalAmount { get; set; }
        double TotalExpectedElectricBillAmount { get; set; }
        double TotalExpectedWaterBillAmount { get; set; }
        double TotalExpectedMiscellaneousFeesAmount { get; set; }
        double TotalExpectedPenaltyAmount { get; set; }

        double TotalPaidRentalAmount { get; set; }
        double TotalPaidElectricBillAmount { get; set; }
        double TotalPaidWaterBillAmount { get; set; }
        double TotalPaidMiscellaneousFeesAmount { get; set; }
        double TotalPaidPenaltyAmount { get; set; }
    }
}
namespace ReserbizAPP.LIB.Interfaces
{
    public interface IAccountStatementPaginationListDto : IEntityPaginationListDto
    {
        float TotalExpectedAmount { get; set; }
        float TotalPaidAmount { get; set; }
        float TotalPaidAmountFromDeposit { get; set; }
        float TotalExpectedDepositAmount { get; set; }
    }
}
namespace ReserbizAPP.LIB.Interfaces
{
    public interface IPaymentPaginationListDto : IEntityPaginationListDto
    {
        double TotalAmount { get; set; }
    }
}
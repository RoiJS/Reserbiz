namespace ReserbizAPP.LIB.Interfaces
{
    public interface IPenaltyPaginationListDto : IEntityPaginationListDto
    {
        double TotalAmount { get; set; }
    }
}
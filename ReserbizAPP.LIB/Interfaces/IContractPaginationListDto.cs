namespace ReserbizAPP.LIB.Interfaces
{
    public interface IContractPaginationListDto : IEntityPaginationListDto
    {
        int TotalNumberOfOpenContracts { get; set; }
        int TotalNumberOfInactiveContracts { get; set; }
        int TotalNumberOfExpiredContracts { get; set; }
    }
}
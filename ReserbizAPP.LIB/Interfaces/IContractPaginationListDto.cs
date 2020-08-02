namespace ReserbizAPP.LIB.Interfaces
{
    public interface IContractPaginationListDto : IEntityPaginationListDto
    {
        int TotalNumberOfOpenContracts { get; set; }
    }
}
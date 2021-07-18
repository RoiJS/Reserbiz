using System;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IContractFilter : IEntityFilter
    {
        string Code { get; set; }
        int TenantId { get; set; }
        DateTime ActiveFrom { get; set; }
        DateTime ActiveTo { get; set; }
        DateTime NextDueDateFrom { get; set; }
        DateTime NextDueDateTo { get; set; }
        bool OpenContract { get; set; }
        bool ArchivedContractsIncluded { get; set; }
        SortOrderEnum CodeSortOrder { get; set; }
        ArchivedContractStatusEnum ArchivedContractStatus { get; set; }
    }
}
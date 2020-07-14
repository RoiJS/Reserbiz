using System;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class ContractFilter : IContractFilter
    {
        public string Code { get; set; }
        public int TenantId { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public DateTime NextDueDateFrom { get; set; }
        public DateTime NextDueDateTo { get; set; }
        public bool OpenContract { get; set; }
    }
}
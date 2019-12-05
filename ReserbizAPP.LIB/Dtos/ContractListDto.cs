using System;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Dtos
{
    public class ContractListDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public int TenantId { get; set; }

        public int TermId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool IsOpenContract { get; set; }

        public int DurationValue { get; set; }

        public DurationEnum DurationUnit { get; set; }

        public bool Status { get; set; }
        
        public DateTime ExpirationDate { get; set; }

        public bool IsExpired { get; set; }
    }
}
using System;
using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Models
{
    public class Contract : Entity
    {
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public int TermId { get; set; }
        public Term Term { get; set; }

        public DateTime EffectiveDate { get; set; }

        public int DurationValue { get; set; }
        public DurationEnum DurationUnit { get; set; }

    }
}
using System;
using System.Collections.Generic;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Dtos
{
    public class ContractDetailDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public int TenantId { get; set; }

        public string TenantName { get; set; }

        public int TermId { get; set; }
        public int? TermParentId { get; set; }

        public int SpaceId { get; set; }
        public string SpaceName { get; set; }

        public ContractTermDetailsDto Term { get; set; }

        public int SpaceTypeId { get; set; }

        public string SpaceTypeName { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool IsOpenContract { get; set; }
        public bool EncashDepositAmount { get; set; }

        public int DurationValue { get; set; }

        public DurationEnum DurationUnit { get; set; }

        public bool IncludeRentalFee { get; set; }
        public bool IncludeUtilityBills { get; set; }
        public bool IncludeMiscellaneousFees { get; set; }
        public bool IncludePenaltyAmount { get; set; }

        public bool Status { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsExpired { get; set; }

        public bool IsActive { get; set; }

        public bool IsEditable { get; set; }

        public DateTime NextDueDate { get; set; }

        public int AccountStatementsCount { get; set; }
    }
}
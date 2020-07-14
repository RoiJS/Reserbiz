using System;
using System.Collections.Generic;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Dtos
{
    public class ContractListDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public DateTime NextDueDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string TenantName { get; set; }

        public bool IsOpenContract { get; set; }

        public List<IContractDurationBeforeContractEnds> ContractDurationBeforeContractEnds { get; set; }

        public bool IsDeletable { get; set; }

        public bool IsActive { get; set; }

        public bool IsExpired { get; set; }
    }
}
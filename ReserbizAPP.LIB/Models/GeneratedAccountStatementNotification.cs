using System;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class GeneratedAccountStatementNotification
        : Entity, IBaseNotification
    {
        public DateTime AccountStatementDateTime { get; set; }
        public int AccountStatementId { get; set; }
        public AccountStatement AccountStatement { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
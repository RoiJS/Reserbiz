using System;
using System.Collections.Generic;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;

namespace ReserbizAPP.LIB.Models
{
    public class Contract : Entity
    {
        public string Code { get; set; }

        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public int TermId { get; set; }
        public Term Term { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool IsOpenContract { get; set; }

        public int DurationValue { get; set; }

        public DurationEnum DurationUnit { get; set; }

        public List<AccountStatement> AccountStatements { get; set; }

        public DateTime ExpirationDate
        {
            get
            {
                return CalculateExpirationDate();
            }
        }
        public bool IsExpired
        {
            get
            {
                return (DateTime.Now >= ExpirationDate);
            }
        }

        public DateTime NextDueDate
        {
            get
            {
                return GetNextDueDate();
            }
        }

        public bool IsDue
        {
            get
            {
                return (DateTime.Now >= NextDueDate);
            }
        }

        public bool IsDueForGeneratingAccountStatement(int daysBeforeGeneratingAccountStatement)
        {
            return !IsExpired && (EffectiveDate == NextDueDate || NextDueDate.Subtract(DateTime.Now).Days <= daysBeforeGeneratingAccountStatement);
        }

        private DateTime CalculateExpirationDate()
        {
            var daysBeforeExpiration = EffectiveDate.CalculateDaysBasedOnDuration(DurationValue, DurationUnit);
            var expirationDate = EffectiveDate.AddDays(daysBeforeExpiration);
            return expirationDate;
        }

        private DateTime GetNextDueDate()
        {
            var nextDueDate = new DateTime();

            if (AccountStatements.Count == 0)
            {
                nextDueDate = EffectiveDate;
            }
            else
            {
                var lastAccountStatement = AccountStatements[AccountStatements.Count - 1];
                var currentDueDate = lastAccountStatement.DueDate;
                var daysBeforeNextDueDate = currentDueDate.CalculateDaysBasedOnDuration(1, lastAccountStatement.DurationUnit);
                nextDueDate = currentDueDate.AddDays(daysBeforeNextDueDate);
            }

            return nextDueDate;
        }
    }
}
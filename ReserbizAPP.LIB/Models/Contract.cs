using System;
using System.Collections.Generic;
using System.Linq;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class Contract
        : Entity, IUserActionTracker
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

        public int? DeletedById { get; set; }
        public Account DeletedBy { get; set; }
        public int? UpdatedById { get; set; }
        public Account UpdatedBy { get; set; }
        public int? CreatedById { get; set; }
        public Account CreatedBy { get; set; }
        public int? DeactivatedById { get; set; }
        public Account DeactivatedBy { get; set; }

        public Contract()
        {

        }

        private DateTime CurrentDateTime = DateTime.Now;

        // This is to set and simulate the current 
        // date time for unit testing purposes
        public void SetCurrentDateTime(DateTime dateTime)
        {
            CurrentDateTime = dateTime;
        }

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
                // Contract is considered as expired based on the following criteria:
                // (1) Current Date is greater than or equal to the expiration date
                // (2) Next duedate is greater than expiration date.
                return (CurrentDateTime >= ExpirationDate || NextDueDate > ExpirationDate);
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
                return (CurrentDateTime >= NextDueDate);
            }
        }

        public List<IContractDurationBeforeContractEnds> ContractDurationBeforeContractEnds
        {
            get
            {
                return GetContractDurationBeforeContractEnds();
            }
        }

        // We can determine if contract is deletable or not
        // based on the current count of account statements
        public bool IsDeletable
        {
            get
            {
                return (this.AccountStatements.Count == 0);
            }
        }

        // We can determine if contract is archived or not
        // if contract is either expired or inactive
        public bool IsArchived
        {
            get
            {
                return (IsExpired || IsActive == false);
            }
        }

        public bool IsDueForGeneratingAccountStatement(int daysBeforeGeneratingAccountStatement)
        {
            // Generating new account statement is based on these criteria below:
            // (1) NextDuedate should be before or on the exact date of expiration date.
            // (2) EffectiveDate should be before or today.
            // (3) EffectiveDate should be equal to the next due date or days before the daysBeforeGeneratingAccountStatement setting.
            return ((NextDueDate <= ExpirationDate)
                    && (EffectiveDate <= CurrentDateTime)
                    && (EffectiveDate == NextDueDate || NextDueDate.Subtract(CurrentDateTime).Days <= daysBeforeGeneratingAccountStatement));
        }

        private DateTime CalculateExpirationDate()
        {
            // If contract is open, return datetime max value
            if (IsOpenContract) return DateTime.MaxValue;

            var daysBeforeExpiration = EffectiveDate.CalculateDaysBasedOnDuration(DurationValue, DurationUnit);
            var expirationDate = EffectiveDate.AddDays(daysBeforeExpiration);
            return expirationDate;
        }

        private DateTime GetNextDueDate()
        {
            var nextDueDate = new DateTime();

            // If the there are no account statements yet,
            // We will consider the effective date as the
            // next due date, If not, we will
            // calculate the next due date based 
            // on the duration unit
            if (AccountStatements.Count == 0)
            {
                nextDueDate = EffectiveDate;
            }
            else
            {
                // Make sure that statement of accounts are ordered by due date ascending
                var accountStatementsOrderByDueDateAscending = AccountStatements.OrderBy(a => a.DueDate).ToList();
                var lastAccountStatement = accountStatementsOrderByDueDateAscending[AccountStatements.Count - 1];
                var currentDueDate = lastAccountStatement.DueDate;
                var daysBeforeNextDueDate = currentDueDate.CalculateDaysBasedOnDuration(1, lastAccountStatement.DurationUnit);
                nextDueDate = currentDueDate.AddDays(daysBeforeNextDueDate);
            }

            return nextDueDate;
        }

        private List<IContractDurationBeforeContractEnds> GetContractDurationBeforeContractEnds()
        {
            var durationBeforeContractEnds = new List<IContractDurationBeforeContractEnds>();

            // Check if contract is open
            if (IsOpenContract || IsExpired) return durationBeforeContractEnds;

            var totalDaysBeforeContractEnds = ExpirationDate.Subtract(CurrentDateTime).TotalDays;

            ContractHelper.CalculateDurationValueBeforeContractEnds(ref durationBeforeContractEnds, totalDaysBeforeContractEnds);

            return durationBeforeContractEnds;
        }
    }
}
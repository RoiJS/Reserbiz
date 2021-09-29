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

        #region "General Settings"
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public int TermId { get; set; }
        public Term Term { get; set; }
        public int? SpaceId { get; set; }
        public Space Space { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool IsOpenContract { get; set; }
        public bool EncashDepositAmount { get; set; }
        public int? EncashedDepositAmountByAccountId { get; set; }
        public Account EncashedDepositAmountByAccount { get; set; }
        public DateTime EncashedDepositAmountDateTime { get; set; }
        public int DurationValue { get; set; }
        public DurationEnum DurationUnit { get; set; }
        #endregion


        #region "Mark Account Statement as Paid Setting"
        public bool IncludeRentalFee { get; set; }
        public bool IncludeUtilityBills { get; set; }
        public bool IncludeMiscellaneousFees { get; set; }
        public bool IncludePenaltyAmount { get; set; }
        #endregion

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
            AccountStatements = new List<AccountStatement>();
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

        public bool IsInactive
        {
            get
            {
                // Any contracts that are not yet expired and set as inactive are considered as archived inactive contract
                return (!IsExpired && !IsActive);
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

        // We can determine if some of contract details are editable or not
        // based on the current count of account statements
        public bool IsEditable
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

        public bool IsDueForGeneratingAccountStatement
        {
            get
            {
                // Generating new account statement is based on these criteria below:
                // (1) NextDuedate should be before or on the exact date of expiration date.
                // (2) EffectiveDate should be before or today.
                // (3) EffectiveDate should be equal to the next due date or days before the daysBeforeGeneratingAccountStatement setting.
                return ((NextDueDate.Date <= ExpirationDate.Date)
                        && (EffectiveDate.Date <= CurrentDateTime.Date)
                        && (EffectiveDate.Date == NextDueDate.Date || NextDueDate.Subtract(CurrentDateTime).Days <= Term.GenerateAccountStatementDaysBeforeValue));
            }

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

            // Only get statement of account with the type of Rental Bills.
            var activeRentalBillAccountStatements = AccountStatements
                    .Where(a =>
                        a.AccountStatementType == AccountStatementTypeEnum.RentalBill &&
                        a.IsDelete == false
                    )
                    .ToList();

            // If the there are no account statements yet,
            // We will consider the effective date as the
            // next due date, If not, we will
            // calculate the next due date based 
            // on the duration unit
            if (activeRentalBillAccountStatements.Count == 0)
            {
                nextDueDate = EffectiveDate;
            }
            else
            {
                // Make sure that active statement of accounts are ordered by due date ascending
                var accountStatementsOrderByDueDateAscending = activeRentalBillAccountStatements
                                                                    .OrderBy(a => a.DueDate)
                                                                    .ToList();
                var lastAccountStatement = accountStatementsOrderByDueDateAscending[activeRentalBillAccountStatements.Count - 1];
                var currentDueDate = lastAccountStatement.DueDate;
                var daysBeforeNextDueDate = currentDueDate.CalculateDaysBasedOnDuration(1, Term.DurationUnit);
                nextDueDate = currentDueDate.AddDays(daysBeforeNextDueDate);
            }

            return nextDueDate;
        }

        private List<IContractDurationBeforeContractEnds> GetContractDurationBeforeContractEnds()
        {
            var durationBeforeContractEnds = new List<IContractDurationBeforeContractEnds>();

            // Check if contract is open
            if (IsOpenContract || IsExpired) return durationBeforeContractEnds;

            var totalDaysBeforeContractEnds = ExpirationDate.Subtract(CurrentDateTime).Days;

            ContractHelper.CalculateDurationValueBeforeContractEnds(ref durationBeforeContractEnds, totalDaysBeforeContractEnds);

            return durationBeforeContractEnds;
        }
    }
}
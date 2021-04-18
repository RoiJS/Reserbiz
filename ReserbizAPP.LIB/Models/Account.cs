using System.Collections.Generic;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.models;

namespace ReserbizAPP.LIB.Models
{
    public class Account 
        : Person, IUserActionTracker
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string EmailAddress { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
        public List<ErrorLog> ErrorLogs { get; set; }

        public int? DeletedById { get; set; }
        public virtual Account DeletedBy { get; set; }
        public int? UpdatedById { get; set; }
        public virtual Account UpdatedBy { get; set; }
        public int? CreatedById { get; set; }
        public virtual Account CreatedBy { get; set; }
        public int? DeactivatedById { get; set; }
        public virtual Account DeactivatedBy { get; set; }

        #region Tenants Tracker
        public List<Tenant> DeletedTenants { get; set; }
        public List<Tenant> UpdatedTenants { get; set; }
        public List<Tenant> CreatedTenants { get; set; }
        public List<Tenant> DeactivatedTenants { get; set; }
        #endregion

        #region Accounts Tracker
        public List<Account> DeletedAccounts { get; set; }
        public List<Account> UpdatedAccounts { get; set; }
        public List<Account> CreatedAccounts { get; set; }
        public List<Account> DeactivatedAccounts { get; set; }
        #endregion
        
        #region Contact Person Tracker
        public List<ContactPerson> DeletedContactPersons { get; set; }
        public List<ContactPerson> UpdatedContactPersons { get; set; }
        public List<ContactPerson> CreatedContactPersons { get; set; }
        public List<ContactPerson> DeactivatedContactPersons { get; set; }
        #endregion
        
        #region Space Types Tracker
        public List<SpaceType> DeletedSpaceTypes { get; set; }
        public List<SpaceType> UpdatedSpaceTypes { get; set; }
        public List<SpaceType> CreatedSpaceTypes { get; set; }
        public List<SpaceType> DeactivatedSpaceTypes { get; set; }
        #endregion

        #region Spaces
        public List<Space> DeletedSpaces { get; set; }
        public List<Space> UpdatedSpaces { get; set; }
        public List<Space> CreatedSpaces { get; set; }
        public List<Space> DeactivatedSpaces { get; set; }
        #endregion
        
        #region Term Tracker
        public List<Term> DeletedTerms { get; set; }
        public List<Term> UpdatedTerms { get; set; }
        public List<Term> CreatedTerms { get; set; }
        public List<Term> DeactivatedTerms { get; set; }
        #endregion
        
        #region Term Miscellaneous Tracker
        public List<TermMiscellaneous> DeletedTermMiscellaneous { get; set; }
        public List<TermMiscellaneous> UpdatedTermMiscellaneous { get; set; }
        public List<TermMiscellaneous> CreatedTermMiscellaneous { get; set; }
        public List<TermMiscellaneous> DeactivatedTermMiscellaneous { get; set; }
        #endregion
        
        #region Contracts Tracker
        public List<Contract> DeletedContracts { get; set; }
        public List<Contract> UpdatedContracts { get; set; }
        public List<Contract> CreatedContracts { get; set; }
        public List<Contract> DeactivatedContracts { get; set; }
        #endregion
        
        #region Account Statements Tracker
        public List<AccountStatement> DeletedAccountStatements { get; set; }
        public List<AccountStatement> UpdatedAccountStatements { get; set; }
        public List<AccountStatement> CreatedAccountStatements { get; set; }
        public List<AccountStatement> DeactivatedAccountStatements { get; set; }
        #endregion
        
        #region Settings Tracker
        public List<ClientSettings> DeletedClientSettings { get; set; }
        public List<ClientSettings> UpdatedClientSettings { get; set; }
        public List<ClientSettings> CreatedClientSettings { get; set; }
        public List<ClientSettings> DeactivatedClientSettings { get; set; }
        #endregion
       
        #region Payment Breakdowns Tracker
        public List<PaymentBreakdown> DeletedPaymentBreakdowns { get; set; }
        public List<PaymentBreakdown> UpdatedPaymentBreakdowns { get; set; }
        public List<PaymentBreakdown> CreatedPaymentBreakdowns { get; set; }
        public List<PaymentBreakdown> DeactivatedPaymentBreakdowns { get; set; }
        #endregion
    }
}
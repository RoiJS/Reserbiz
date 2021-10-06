using System.Collections.Generic;
using System.Linq;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Helpers.Services.StatementOfAccounts
{
    public abstract class BaseStatementOfAccountService
        : IStatementOfAccountService
    {
        public List<AccountStatementMiscellaneous> GenerateAccountStatementMiscellaneous(List<TermMiscellaneous> termMiscellaneous)
        {
            var accountTermMiscellaneous = new List<AccountStatementMiscellaneous>();

            // Get only active term miscellaneous
            foreach (var item in termMiscellaneous.Where(t => t.IsActive == true && t.IsDelete == false))
            {
                var newTermMiscellaneous = new AccountStatementMiscellaneous();
                newTermMiscellaneous.Name = item.Name;
                newTermMiscellaneous.Description = item.Description;
                newTermMiscellaneous.Amount = item.Amount;
                accountTermMiscellaneous.Add(newTermMiscellaneous);
            }
            return accountTermMiscellaneous;
        }

        public virtual AccountStatement GenerateStatementOfAccount(Contract contract, AccountStatement accountStatement)
        {
            throw new System.NotImplementedException();
        }
    }
}
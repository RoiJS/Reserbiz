using System.Collections.Generic;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IStatementOfAccountService
    {
        AccountStatement GenerateStatementOfAccount(Contract contract, AccountStatement accountStatement);

        List<AccountStatementMiscellaneous> GenerateAccountStatementMiscellaneous(List<TermMiscellaneous> termMiscellaneous);
    }
}
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Helpers.Services.StatementOfAccounts
{
    public class UtilityBillStatementOfAccountService
        :  BaseStatementOfAccountService
    {
        public override AccountStatement GenerateStatementOfAccount(Contract contract, AccountStatement accountStatement)
        {
            var contractTerm = contract.Term;

            var newAccountStatement = new AccountStatement
            {
                DueDate = accountStatement.DueDate.ToLocalTimeZone(),
                ElectricBill = accountStatement.ElectricBill,
                WaterBill = accountStatement.WaterBill,
                ExcludeElectricBill = contractTerm.ExcludeElectricBill,
                ExcludeWaterBill = contractTerm.ExcludeWaterBill,
                MiscellaneousDueDate = contractTerm.MiscellaneousDueDate,
                AccountStatementType = AccountStatementTypeEnum.UtilityBill
            };

            if (contractTerm.MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithUtilityBillDueDate)
            {
                newAccountStatement.AccountStatementMiscellaneous.AddRange(GenerateAccountStatementMiscellaneous(contractTerm.TermMiscellaneous));
            }

            return newAccountStatement;
        }
    }
}
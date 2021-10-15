using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Helpers.Services.StatementOfAccounts
{
    public class RentalBillStatementOfAccountService
        : BaseStatementOfAccountService
    {
        public override AccountStatement GenerateStatementOfAccount(Contract contract, AccountStatement accountStatement)
        {
            var contractTerm = contract.Term;

            var newAccountStatement = new AccountStatement
            {
                DueDate = contract.NextDueDate,
                Rate = contractTerm.Rate,
                DurationUnit = contractTerm.DurationUnit,
                PenaltyValue = contractTerm.PenaltyValue,
                AdvancedPaymentDurationValue = contractTerm.AdvancedPaymentDurationValue,
                DepositPaymentDurationValue = contractTerm.DepositPaymentDurationValue,
                PenaltyValueType = contractTerm.PenaltyValueType,
                PenaltyAmountPerDurationUnit = contractTerm.PenaltyAmountPerDurationUnit,
                PenaltyEffectiveAfterDurationValue = contractTerm.PenaltyEffectiveAfterDurationValue,
                PenaltyEffectiveAfterDurationUnit = contractTerm.PenaltyEffectiveAfterDurationUnit,
                MiscellaneousDueDate = contractTerm.MiscellaneousDueDate,
                AccountStatementType = AccountStatementTypeEnum.RentalBill,
                AutoSendNewAccountStatement = contractTerm.AutoSendNewAccountStatement
            };

            if (contractTerm.MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithRentalDueDate)
            {
                newAccountStatement.AccountStatementMiscellaneous.AddRange(GenerateAccountStatementMiscellaneous(contractTerm.TermMiscellaneous));
            }

            return newAccountStatement;
        }
    }
}
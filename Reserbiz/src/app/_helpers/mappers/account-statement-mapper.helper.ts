import { IBaseEntityMapper } from '../../_interfaces/mappers/ibase-entity-mapper.interface';
import { AccountStatement } from '../../_models/account-statement.model';
import { AccountStatementMiscellaneous } from '../../_models/account-statement-miscellaneous.model';

export class AccountStatementMapper
  implements IBaseEntityMapper<AccountStatement>
{
  mapEntity(as: AccountStatement): AccountStatement {
    const accountStatement = new AccountStatement();
    if (as) {
      accountStatement.contractId = as.contractId;
      accountStatement.id = as.id;
      accountStatement.accountStatementType = as.accountStatementType;
      accountStatement.dueDate = as.dueDate;
      accountStatement.rate = as.rate;
      accountStatement.depositPaymentDurationValue =
        as.depositPaymentDurationValue;
      accountStatement.advancedPaymentDurationValue =
        as.advancedPaymentDurationValue;
      accountStatement.excludeElectricBill = as.excludeElectricBill;
      accountStatement.electricBill = as.electricBill;
      accountStatement.excludeWaterBill = as.excludeWaterBill;
      accountStatement.waterBill = as.waterBill;
      accountStatement.penaltyNextDueDate = as.penaltyNextDueDate;
      accountStatement.penaltyTotalAmount = as.penaltyTotalAmount;
      accountStatement.accountStatementTotalAmount =
        as.accountStatementTotalAmount;
      accountStatement.currentAmountPaid = as.currentAmountPaid;
      accountStatement.miscellaneousTotalAmount = as.miscellaneousTotalAmount;
      accountStatement.currentBalance = as.currentBalance;
      accountStatement.isFullyPaid = as.isFullyPaid;
      accountStatement.tenantName = as.tenantName;
      accountStatement.isFirstAccountStatement = as.isFirstAccountStatement;
      accountStatement.isDeletable = as.isDeletable;
      accountStatement.miscellaneousDueDate = as.miscellaneousDueDate;
      accountStatement.totalPaidRentalAmount = as.totalPaidRentalAmount;
      accountStatement.totalPaidWaterBills = as.totalPaidWaterBills;
      accountStatement.totalPaidElectricBills = as.totalPaidElectricBills;
      accountStatement.totalPaidMiscellaneousFees =
        as.totalPaidMiscellaneousFees;
      accountStatement.totalPaidPenaltyAmount = as.totalPaidPenaltyAmount;
      accountStatement.lastDateSent = as.lastDateSent;

      if (
        as.accountStatementMiscellaneous &&
        as.accountStatementMiscellaneous.length > 0
      ) {
        accountStatement.accountStatementMiscellaneous =
          as.accountStatementMiscellaneous.map(
            (
              accountStatementMiscellaneousObject: AccountStatementMiscellaneous
            ) => {
              const accountStatmentMiscellaneous =
                new AccountStatementMiscellaneous();

              accountStatmentMiscellaneous.name =
                accountStatementMiscellaneousObject.name;
              accountStatmentMiscellaneous.description =
                accountStatementMiscellaneousObject.description;
              accountStatmentMiscellaneous.amount =
                accountStatementMiscellaneousObject.amount;
              return accountStatmentMiscellaneous;
            }
          );
      }
    }

    return accountStatement;
  }
}

import { IBaseEntityMapper } from '../_interfaces/ibase-entity-mapper.interface';
import { AccountStatement } from '../_models/account-statement.model';
import { IBaseDtoEntityMapper } from '../_interfaces/ibase-dto-entity-mapper.interface';
import { AccountStatementFormSource } from '../_models/account-statement-form.model';
import { AccountStatementDto } from '../_dtos/account-statement.dto';
import { AccountStatementMiscellaneous } from '../_models/account-statement-miscellaneous.model';

export class AccountStatementMapper
  implements
    IBaseEntityMapper<AccountStatement>,
    IBaseDtoEntityMapper<
      AccountStatement,
      AccountStatementFormSource,
      AccountStatementDto
    > {
  mapEntity(as: AccountStatement): AccountStatement {
    const accountStatement = new AccountStatement();
    accountStatement.contractId = as.contractId;
    accountStatement.id = as.id;
    accountStatement.dueDate = as.dueDate;
    accountStatement.rate = as.rate;
    accountStatement.depositPaymentDurationValue =
      as.depositPaymentDurationValue;
    accountStatement.advancedPaymentDurationValue =
      as.advancedPaymentDurationValue;
    accountStatement.electricBill = as.electricBill;
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

    if (
      as.accountStatementMiscellaneous &&
      as.accountStatementMiscellaneous.length > 0
    ) {
      accountStatement.accountStatementMiscellaneous = as.accountStatementMiscellaneous.map(
        (
          accountStatementMiscellaneousObject: AccountStatementMiscellaneous
        ) => {
          const accountStatmentMiscellaneous = new AccountStatementMiscellaneous();

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

    return accountStatement;
  }

  initFormSource(): AccountStatementFormSource {
    const accountStatementFormSource = new AccountStatementFormSource(0.0, 0.0);

    return accountStatementFormSource;
  }

  mapFormSourceToDto(afs: AccountStatementFormSource): AccountStatementDto {
    const accountStatementDto = new AccountStatementDto(
      afs.electricBill,
      afs.waterBill
    );
    return accountStatementDto;
  }

  mapEntityToFormSource(
    accountStatement: AccountStatement
  ): AccountStatementFormSource {
    const accountStatementFormSource = new AccountStatementFormSource(
      accountStatement.electricBill,
      accountStatement.waterBill
    );
    return accountStatementFormSource;
  }

  mapFormSourceToEntity(
    formSource: AccountStatementFormSource
  ): AccountStatement {
    throw new Error('Not implemented');
  }
}

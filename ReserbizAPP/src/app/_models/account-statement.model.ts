import { DateFormatter } from '../_helpers/formatters/date-formatter.helper';
import { NumberFormatter } from '../_helpers/formatters/number-formatter.helper';

import { AccountStatementMiscellaneous } from './account-statement-miscellaneous.model';
import { Entity } from './entity.model';

export class AccountStatement extends Entity {
  public contractId: number;
  public dueDate: Date;
  public rate: number;
  public advancedPaymentDurationValue: number;
  public depositPaymentDurationValue: number;
  public electricBill: number;
  public waterBill: number;
  public penaltyNextDueDate: Date;
  public penaltyTotalAmount: number;
  public miscellaneousTotalAmount: number;
  public accountStatementTotalAmount: number;
  public currentAmountPaid: number;
  public currentBalance: number;
  public isFullyPaid: boolean;
  public isFirstAccountStatement: boolean;
  public tenantName: string;
  public accountStatementMiscellaneous: AccountStatementMiscellaneous[];

  constructor() {
    super();
    this.contractId = 0;
    this.dueDate = null;
    this.rate = 0;
    this.electricBill = 0;
    this.waterBill = 0;
    this.penaltyNextDueDate = null;
    this.penaltyTotalAmount = 0;
    this.miscellaneousTotalAmount = 0;
    this.accountStatementTotalAmount = 0;
    this.currentAmountPaid = 0;
    this.isFullyPaid = false;
    this.accountStatementMiscellaneous = [];
  }

  get dueDateFormatted(): string {
    return DateFormatter.format(this.dueDate, 'MMM DD, YYYY');
  }

  get rentIncome(): number {
    let rentIncome = this.rate;

    // Check if the account statement is the first then calculate the rent income based on the
    // rate and deposit payment duration values
    if (this.isFirstAccountStatement) {
      rentIncome += this.rate * this.depositPaymentDurationValue;
    }

    return rentIncome;
  }

  get accountStatementTotalAmountFormatted(): string {
    return NumberFormatter.formatCurrency(this.accountStatementTotalAmount);
  }

  get currentAmountPaidFormatted(): string {
    return NumberFormatter.formatCurrency(this.currentAmountPaid);
  }

  get miscellaneousTotalAmountFormatted(): string {
    return NumberFormatter.formatCurrency(this.miscellaneousTotalAmount);
  }

  get waterBillFormatted(): string {
    return NumberFormatter.formatCurrency(this.waterBill);
  }

  get electricBillFormatted(): string {
    return NumberFormatter.formatCurrency(this.electricBill);
  }

  get penaltyTotalAmountFormatted(): string {
    return NumberFormatter.formatCurrency(this.penaltyTotalAmount);
  }

  get rentIncomeFormatted(): string {
    return NumberFormatter.formatCurrency(this.rentIncome);
  }

  get rateFormatted(): string {
    return NumberFormatter.formatCurrency(this.rate);
  }
}

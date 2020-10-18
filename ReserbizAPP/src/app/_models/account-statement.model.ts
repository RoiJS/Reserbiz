import { DateFormatter } from '../_helpers/date-formatter.helper';
import { NumberFormatter } from '../_helpers/number-formatter.helper';

import { AccountStatementMiscellaneous } from './account-statement-miscellaneous.model';
import { Entity } from './entity.model';

export class AccountStatement extends Entity {
  public contractId: number;
  public dueDate: Date;
  public rate: number;
  public electricBill: number;
  public waterBill: number;
  public penaltyNextDueDate: Date;
  public penaltyTotalAmount: number;
  public miscellaneousTotalAmount: number;
  public accountStatementTotalAmount: number;
  public currentAmountPaid: number;
  public currentBalance: number;
  public isFullyPaid: boolean;
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
    return DateFormatter.format(this.dueDate, 'MMM DD YYYY');
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
    const rentIncome = this.rate + this.penaltyTotalAmount;
    return NumberFormatter.formatCurrency(rentIncome);
  }

  get rateFormatted(): string {
    return NumberFormatter.formatCurrency(this.rate);
  }
}

import { AccountStatementTypeEnum } from '../_enum/account-statement-type.enum';
import { MiscellaneousDueDateEnum } from '../_enum/miscellaneous-due-date.enum';

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
  public excludeElectricBill: boolean;
  public electricBill: number;
  public excludeWaterBill: boolean;
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
  public accountStatementType: AccountStatementTypeEnum;
  public miscellaneousDueDate: MiscellaneousDueDateEnum;
  public accountStatementMiscellaneous: AccountStatementMiscellaneous[];

  public totalPaidRentalAmount: number;
  public totalPaidElectricBills: number;
  public totalPaidWaterBills: number;
  public totalPaidMiscellaneousFees: number;
  public totalPaidPenaltyAmount: number;

  constructor() {
    super();
    this.contractId = 0;
    this.dueDate = null;
    this.rate = 0;
    this.excludeElectricBill = true;
    this.electricBill = 0;
    this.excludeWaterBill = true;
    this.waterBill = 0;
    this.penaltyNextDueDate = null;
    this.penaltyTotalAmount = 0;
    this.miscellaneousTotalAmount = 0;
    this.accountStatementTotalAmount = 0;
    this.currentAmountPaid = 0;
    this.isFullyPaid = false;
    this.miscellaneousDueDate = MiscellaneousDueDateEnum.SameWithRentalDueDate;
    this.accountStatementMiscellaneous = [];
    this.totalPaidRentalAmount = 0;
    this.totalPaidElectricBills = 0;
    this.totalPaidWaterBills = 0;
    this.totalPaidMiscellaneousFees = 0;
    this.totalPaidPenaltyAmount = 0;
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

  get totalPaidRentalAmountFormatted(): string {
    return NumberFormatter.formatCurrency(this.totalPaidRentalAmount);
  }

  get totalPaidElectricBillsAmountFormatted(): string {
    return NumberFormatter.formatCurrency(this.totalPaidElectricBills);
  }

  get totalPaidWaterBillsAmountFormatted(): string {
    return NumberFormatter.formatCurrency(this.totalPaidWaterBills);
  }

  get totalPaidMiscellaneousFeesFormatted(): string {
    return NumberFormatter.formatCurrency(this.totalPaidMiscellaneousFees);
  }

  get totalPaidPenaltyFeesFormatted(): string {
    return NumberFormatter.formatCurrency(this.totalPaidPenaltyAmount);
  }
}

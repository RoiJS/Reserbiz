import { NumberFormatter } from '../_helpers/formatters/number-formatter.helper';

export class AccountStatementSummaryChart {
  public name: string;
  public value: number;

  formattedValue(): string {
    return NumberFormatter.formatCurrency(this.value);
  }
}

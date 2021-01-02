import { DateFormatter } from '../_helpers/date-formatter.helper';
import { NumberFormatter } from '../_helpers/number-formatter.helper';

import { Entity } from './entity.model';

export class Penalty extends Entity {
  public dueDate: Date;
  public amount: number;

  get dueDateReceivedFormatted(): string {
    return DateFormatter.format(this.dueDate, 'MMM DD, YYYY');
  }

  get amountFormatted(): string {
    return NumberFormatter.formatCurrency(this.amount);
  }
}

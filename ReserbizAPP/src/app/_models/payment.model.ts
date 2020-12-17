import { DateFormatter } from '../_helpers/date-formatter.helper';
import { NumberFormatter } from '../_helpers/number-formatter.helper';
import { Entity } from './entity.model';

export class Payment extends Entity {
  public accountStatementId: number;
  public dateTimeReceived: Date;
  public amount: number;
  public receivedBy: string;
  public notes: string;

  constructor() {
    super();
  }

  get dateTimeReceivedFormatted(): string {
    return DateFormatter.format(this.dateTimeReceived, 'MMM DD, YYYY h:mm A');
  }

  get amountFormatted(): string {
    return NumberFormatter.formatCurrency(this.amount);
  }

  get hasNotes() {
    return this.notes && this.notes.length > 0;
  }
}

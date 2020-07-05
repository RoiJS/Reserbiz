import { Entity } from './entity.model';
import { NumberFormatter } from '../_helpers/number-formatter.helper';

export abstract class Miscellaneous extends Entity {
  public name: string;
  public description: string;
  public amount: number;

  get formattedAmount(): string {
    return NumberFormatter.formatCurrency(this.amount);
  }

  get nameInitials(): string {
    return `${this.name[0]}`;
  }

  get photoBackgroundColor(): string {
    const randomIndex = this.getNumberFirstDigit();
    // Get color randomly from the list
    return this.colorList[randomIndex];
  }

  private getNumberFirstDigit(): number {
    const stringId = this.id.toString().split('');
    return +stringId[stringId.length - 1];
  }
}

import { Entity } from './entity.model';

export abstract class Miscellaneous extends Entity {
  public name: string;
  public description: string;
  public amount: number;

  get amountWithCurrency(): string {
    return `Php ${this.amount.toFixed(2)}`;
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

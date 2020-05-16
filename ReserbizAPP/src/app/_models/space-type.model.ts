import { Entity } from './entity.model';

export class SpaceType extends Entity {
  public name: string;
  public description: string;
  public rate: number;
  public availableSlot: number;

  constructor() {
    super();
  }

  get nameInitials(): string {
    return this.name[0];
  }

  get photoBackgroundColor(): string {
    const colorList = [
      '#f6d186',
      '#a8d3da',
      '#f4eeff',
      '#ffaaa5',
      '#beebe9',
      '#f6eec7',
      '#ffd5e5',
      '#ffffdd',
      '#81f5ff',
      '#ffffc5',
    ];

    const randomIndex = this.getNumberFirstDigit();
    // Get color randomly from the list
    return colorList[randomIndex];
  }

  private getNumberFirstDigit(): number {
    const stringId = this.id.toString().split('');
    return +stringId[stringId.length - 1];
  }
}

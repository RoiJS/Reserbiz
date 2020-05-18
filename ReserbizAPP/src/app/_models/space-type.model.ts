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
    const randomIndex = this.getNumberFirstDigit();
    // Get color randomly from the list
    return this.colorList[randomIndex];
  }

  private getNumberFirstDigit(): number {
    const stringId = this.id.toString().split('');
    return +stringId[stringId.length - 1];
  }
}

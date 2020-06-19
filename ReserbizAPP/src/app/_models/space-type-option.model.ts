export class SpaceTypeOption {
  public id: number;
  public name: string;
  public rate: number;
  public isDelete: boolean;
  public isActive: boolean;
  public canBeSelected: boolean;
  public inactiveText: string;

  get displayName(): string {
    return this.canBeSelected
      ? this.name
      : `${this.name} (${this.inactiveText})`;
  }
}

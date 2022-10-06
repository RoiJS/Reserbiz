import { IEntityOption } from '../../_interfaces/ientity-option.interface';

export class EntityOption implements IEntityOption {
  public id: number;
  public name: string;
  public isDelete: boolean;
  public isActive: boolean;
  public canBeSelected: boolean;
  public inactiveText: string;

  get displayName(): string {
    return this.canBeSelected
      ? this.name
      : `${this.name} - (${this.inactiveText})`;
  }
}

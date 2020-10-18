import { NumberFormatter } from '../_helpers/number-formatter.helper';
import { Entity } from './entity.model';

export class Space extends Entity {
  public description: string;
  public spaceTypeId: number;
  public spaceTypeName: string;
  public spaceTypeRate: number;
  public isNotOccupied: boolean;

  constructor() {
    super();
  }

  get spaceTypeRateFormatted(): string {
    return NumberFormatter.formatCurrency(this.spaceTypeRate);
  }
}

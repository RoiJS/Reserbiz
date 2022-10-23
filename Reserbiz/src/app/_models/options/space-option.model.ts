import { EntityOption } from './entity-option.model';

export class SpaceOption extends EntityOption {
  public spaceTypeId: number;
  public isNotOccupied: boolean;
  public occupiedByContractId: number;
}

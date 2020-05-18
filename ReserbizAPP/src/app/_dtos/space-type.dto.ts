import { IBaseDTO } from '../_interfaces/ibase-dto.interface';

export class SpaceTypeDto implements IBaseDTO {
  constructor(
    public name: string,
    public description: string,
    public rate: number,
    public availableSlot: number
  ) {}
}

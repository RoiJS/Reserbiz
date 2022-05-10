import { IBaseDto } from '../_interfaces/ibase-dto.interface';

export class SpaceTypeDto implements IBaseDto {
  constructor(
    public name: string,
    public description: string,
    public rate: number
  ) {}
}

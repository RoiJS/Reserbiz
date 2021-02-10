import { IBaseDto } from '../_interfaces/ibase-dto.interface';

export class SpaceDto implements IBaseDto {
  constructor(public description: string, public spaceTypeId: number) {}
}

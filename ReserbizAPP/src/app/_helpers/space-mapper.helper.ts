import { SpaceDto } from '../_dtos/space-dto';
import { IBaseDtoEntityMapper } from '../_interfaces/ibase-dto-entity-mapper.interface';
import { IBaseEntityMapper } from '../_interfaces/ibase-entity-mapper.interface';
import { SpaceFormSource } from '../_models/space-form.model';
import { Space } from '../_models/space.model';

export class SpaceMapper
  implements
    IBaseEntityMapper<Space>,
    IBaseDtoEntityMapper<Space, SpaceFormSource, SpaceDto> {
  mapEntity(s: Space) {
    const space = new Space();

    space.id = s.id;
    space.description = s.description;
    space.spaceTypeId = s.spaceTypeId;
    space.spaceTypeName = s.spaceTypeName;
    space.spaceTypeRate = s.spaceTypeRate;
    space.isNotOccupied = s.isNotOccupied;
    space.occupiedByContractId = s.occupiedByContractId;
    space.isDeletable = s.isDeletable;

    return space;
  }

  initFormSource(): SpaceFormSource {
    return new SpaceFormSource('', 0);
  }

  mapFormSourceToDto(sfc: SpaceFormSource): SpaceDto {
    const spaceDto = new SpaceDto(sfc.description, sfc.spaceTypeId);
    return spaceDto;
  }

  mapEntityToFormSource(space: Space): SpaceFormSource {
    const spaceFormSource = new SpaceFormSource(
      space.description,
      space.spaceTypeId
    );

    return spaceFormSource;
  }

  mapFormSourceToEntity(formSource: SpaceFormSource): Space {
    throw new Error('Not implemented');
  }
}

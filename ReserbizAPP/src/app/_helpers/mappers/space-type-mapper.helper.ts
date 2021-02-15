import { IBaseEntityMapper } from '../../_interfaces/mappers/ibase-entity-mapper.interface';
import { IBaseDtoEntityMapper } from '../../_interfaces/mappers/ibase-dto-entity-mapper.interface';

import { SpaceType } from '../../_models/space-type.model';
import { SpaceTypeFormSource } from '../../_models/form/space-type-form.model';

import { SpaceTypeDto } from '../../_dtos/space-type.dto';

export class SpaceTypeMapper
  implements
    IBaseEntityMapper<SpaceType>,
    IBaseDtoEntityMapper<SpaceType, SpaceTypeFormSource, SpaceTypeDto> {
  mapEntity(st: SpaceType): SpaceType {
    const spaceType = new SpaceType();
    spaceType.id = st.id;
    spaceType.name = st.name;
    spaceType.description = st.description;
    spaceType.rate = st.rate;
    spaceType.availableSlot = st.availableSlot;
    spaceType.isActive = st.isActive;
    spaceType.isDeletable = st.isDeletable;
    return spaceType;
  }

  initFormSource(): SpaceTypeFormSource {
    const spaceTypFormSource = new SpaceTypeFormSource('', '', 0);
    return spaceTypFormSource;
  }

  mapFormSourceToDto(std: SpaceTypeFormSource): SpaceTypeDto {
    const spaceTypeForCreate = new SpaceTypeDto(
      std.name,
      std.description,
      std.rate
    );
    return spaceTypeForCreate;
  }

  mapEntityToFormSource(spaceType: SpaceType): SpaceTypeFormSource {
    const spaceTypeFormSource = new SpaceTypeFormSource(
      spaceType.name,
      spaceType.description,
      spaceType.rate
    );

    return spaceTypeFormSource;
  }

  mapFormSourceToEntity(formSource: SpaceTypeFormSource): SpaceType {
    throw new Error('Not implemented');
  }
}

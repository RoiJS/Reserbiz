import { IBaseEntityMapper } from '../_interfaces/ibase-entity-mapper.interface';
import { SpaceType } from '../_models/space-type.model';
import { SpaceTypeFormSource } from '../_models/space-type-form.model';
import { IBaseDtoEntityMapper } from '../_interfaces/ibase-dto-entity-mapper.interface';
import { SpaceTypeDto } from '../_dtos/space-type.dto';

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
    const spaceTypFormSource = new SpaceTypeFormSource('', '', 0, 0);
    return spaceTypFormSource;
  }

  mapFormSourceToDto(std: SpaceTypeFormSource): SpaceTypeDto {
    const spaceTypeForCreate = new SpaceTypeDto(
      std.name,
      std.description,
      std.rate,
      std.availableSlot
    );
    return spaceTypeForCreate;
  }

  mapEntityToFormSource(spaceType: SpaceType): SpaceTypeFormSource {
    const spaceTypeFormSource = new SpaceTypeFormSource(
      spaceType.name,
      spaceType.description,
      spaceType.rate,
      spaceType.availableSlot
    );

    return spaceTypeFormSource;
  }
}

import { SpaceTypeOption } from '../_models/space-type-option.model';

export interface ISpaceTypeValueProvider {
  spaceTypeOptions: { key: string; label: string; items: SpaceTypeOption[] };
}

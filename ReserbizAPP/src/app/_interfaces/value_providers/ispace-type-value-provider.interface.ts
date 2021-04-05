import { SpaceTypeOption } from '../../_models/options/space-type-option.model';

export interface ISpaceTypeValueProvider {
  spaceTypeOptions: { key: string; label: string; items: SpaceTypeOption[] };
}

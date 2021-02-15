import { SpaceOption } from '../../_models/options/space-option.model';

export interface ISpaceValueProvider {
  spaceOptions: { key: string; label: string; items: SpaceOption[] };
}

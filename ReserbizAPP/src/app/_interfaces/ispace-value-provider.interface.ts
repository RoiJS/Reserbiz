import { SpaceOption } from '../_models/space-option.model';

export interface ISpaceValueProvider {
  spaceOptions: { key: string; label: string; items: SpaceOption[] };
}

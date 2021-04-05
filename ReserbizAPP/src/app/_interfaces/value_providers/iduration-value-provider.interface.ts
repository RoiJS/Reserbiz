import { DurationEnum } from '../../_enum/duration-unit.enum';

export interface IDurationValueProvider {
  durationOptions: Array<{ key: DurationEnum; label: string }>;
}

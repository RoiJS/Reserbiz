import { DurationEnum } from '../_enum/duration-unit.enum';

export interface IDurationRangeValueProvider {
  currentDuration: DurationEnum;
  minimum: number;
  maximum: number;
  checkIfExceed(value: number): boolean;
}

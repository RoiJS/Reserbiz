import { IDurationRangeValueProvider } from '../_interfaces/iduration-range-value-provider.interface';
import { DurationEnum } from '../_enum/duration-unit.enum';

export class DurationRangeValueProvider implements IDurationRangeValueProvider {
  private NONE_MAX_VALUE = 0;
  private DAY_MAX_VALUE = 365;
  private WEEK_MAX_VALUE = 52;
  private MONTH_MAX_VALUE = 12;
  private QUARTER_MAX_VALUE = 4;
  private YEAR_MAX_VALUE = 1;

  constructor(public currentDuration = DurationEnum.None) {}

  get minimum(): number {
    return 0;
  }

  get maximum(): number {
    // Maximum values for each duration unit type.
    // Sequence are based on DurationEnum
    const durationMaxValues = [
      this.NONE_MAX_VALUE,
      this.DAY_MAX_VALUE,
      this.WEEK_MAX_VALUE,
      this.MONTH_MAX_VALUE,
      this.QUARTER_MAX_VALUE,
      this.YEAR_MAX_VALUE,
    ];
    return durationMaxValues[this.currentDuration];
  }

  checkIfExceed(value: number) {
    return !!(value > this.maximum);
  }
}

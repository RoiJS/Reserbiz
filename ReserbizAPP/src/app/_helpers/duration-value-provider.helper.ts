import { IDurationValueProvider } from '../_interfaces/iduration-value-provider.interface';
import { DurationEnum } from '../_enum/duration-unit.enum';
import { TranslateService } from '@ngx-translate/core';

export class DurationValueProvider implements IDurationValueProvider {
  constructor(private translateService: TranslateService) {}

  get durationOptions(): Array<{ key: DurationEnum; label: string }> {
    return [
      {
        key: DurationEnum.None,
        label: this.translateService.instant('GENERAL_TEXTS.DURATION.NONE'),
      },
      {
        key: DurationEnum.Day,
        label: this.translateService.instant(
          'GENERAL_TEXTS.DURATION.DAY.BASE_FORM'
        ),
      },
      {
        key: DurationEnum.Week,
        label: this.translateService.instant(
          'GENERAL_TEXTS.DURATION.WEEK.BASE_FORM'
        ),
      },
      {
        key: DurationEnum.Month,
        label: this.translateService.instant(
          'GENERAL_TEXTS.DURATION.MONTH.BASE_FORM'
        ),
      },
      {
        key: DurationEnum.Quarter,
        label: this.translateService.instant(
          'GENERAL_TEXTS.DURATION.QUARTER.BASE_FORM'
        ),
      },
      {
        key: DurationEnum.Year,
        label: this.translateService.instant(
          'GENERAL_TEXTS.DURATION.YEAR.BASE_FORM'
        ),
      },
    ];
  }

  getDurationName(durationNumber: number, durationUnitText: string): string {
    let durationName = '';
    durationUnitText = durationUnitText.toUpperCase();

    durationName =
      durationNumber > 1
        ? this.translateService.instant(
            `GENERAL_TEXTS.DURATION.${durationUnitText}.S_FORM`
          )
        : this.translateService.instant(
            `GENERAL_TEXTS.DURATION.${durationUnitText}.BASE_FORM`
          );

    return durationName;
  }
}

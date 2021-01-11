import { TranslateService } from '@ngx-translate/core';

import { IMonthValueProvider } from '../_interfaces/imonth-value-provider.interface';

import { MonthOption } from '../_models/month-option.model';

import { MonthOptions } from '../_enum/month-options.enum';

export class MonthValueProvider implements IMonthValueProvider {
  constructor(private translateService: TranslateService) {}

  get monthOptions(): Array<MonthOption> {
    const monthOptions = [
      {
        key: MonthOptions.January,
        label: this.translateService.instant('GENERAL_TEXTS.MONTH.JANUARY'),
      },
      {
        key: MonthOptions.February,
        label: this.translateService.instant('GENERAL_TEXTS.MONTH.FEBRUARY'),
      },
      {
        key: MonthOptions.March,
        label: this.translateService.instant('GENERAL_TEXTS.MONTH.MARCH'),
      },
      {
        key: MonthOptions.April,
        label: this.translateService.instant('GENERAL_TEXTS.MONTH.APRIL'),
      },
      {
        key: MonthOptions.May,
        label: this.translateService.instant('GENERAL_TEXTS.MONTH.MAY'),
      },
      {
        key: MonthOptions.June,
        label: this.translateService.instant('GENERAL_TEXTS.MONTH.JUNE'),
      },
      {
        key: MonthOptions.July,
        label: this.translateService.instant('GENERAL_TEXTS.MONTH.JULY'),
      },
      {
        key: MonthOptions.August,
        label: this.translateService.instant('GENERAL_TEXTS.MONTH.AUGUST'),
      },
      {
        key: MonthOptions.September,
        label: this.translateService.instant('GENERAL_TEXTS.MONTH.SEPTEMBER'),
      },
      {
        key: MonthOptions.October,
        label: this.translateService.instant('GENERAL_TEXTS.MONTH.OCTOBER'),
      },
      {
        key: MonthOptions.November,
        label: this.translateService.instant('GENERAL_TEXTS.MONTH.NOVEMBER'),
      },
      {
        key: MonthOptions.December,
        label: this.translateService.instant('GENERAL_TEXTS.MONTH.DECEMBER'),
      },
    ];

    const currentMonth = new Date().getMonth() + 1;

    return monthOptions.filter((m: MonthOption) => m.key >= currentMonth);
  }
}

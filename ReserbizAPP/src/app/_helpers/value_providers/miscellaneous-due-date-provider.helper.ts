import { TranslateService } from '@ngx-translate/core';

import { IMiscellaneousValueProvider } from '@src/app/_interfaces/value_providers/imiscellaneous-value-provider.interface';
import { MiscellaneousDueDateEnum } from '@src/app/_enum/miscellaneous-due-date.enum';

export class MiscellaneousValueProvider implements IMiscellaneousValueProvider {
  constructor(private translateService: TranslateService) {}

  get dueDateOptions(): Array<{
    key: MiscellaneousDueDateEnum;
    label: string;
  }> {
    return [
      {
        key: MiscellaneousDueDateEnum.SameWithRentalDueDate,
        label: this.translateService.instant(
          'GENERAL_TEXTS.MISCELLANEOUS_DUE_DATE_OPTIONS.SAME_WITH_RENTAL_DUE_DATE'
        ),
      },
      {
        key: MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
        label: this.translateService.instant(
          'GENERAL_TEXTS.MISCELLANEOUS_DUE_DATE_OPTIONS.SAME_WITH_UTILITY_BILL_DUE_DATE'
        ),
      },
    ];
  }
}

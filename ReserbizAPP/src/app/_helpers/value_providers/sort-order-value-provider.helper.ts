import { TranslateService } from '@ngx-translate/core';

import { ISortOrderValueProvider } from '../../_interfaces/value_providers/isort-order-value-provider.interface.tns';
import { SortOrderEnum } from '../../_enum/sort-order.enum';

export class SortOrderValueProvider implements ISortOrderValueProvider {
  constructor(private translateService: TranslateService) {}

  get sortOrderOptions(): Array<{ key: SortOrderEnum; label: string }> {
    return [
      {
        key: SortOrderEnum.Ascending,
        label: this.translateService.instant('GENERAL_TEXTS.SORT_ORDER.ASCENDING'),
      },
      {
        key: SortOrderEnum.Descending,
        label: this.translateService.instant('GENERAL_TEXTS.SORT_ORDER.DESCENDING'),
      },
    ];
  }
}

import { IValueTypeValueProvider } from '../../_interfaces/value_providers/ivalue-type-value-provider.interface';
import { TranslateService } from '@ngx-translate/core';

import { ValueTypeEnum } from '../../_enum/value-type.enum';

export class ValueTypeValueProvider implements IValueTypeValueProvider {
  constructor(private translateService: TranslateService) {}

  get valueTypeOptions(): Array<{ key: ValueTypeEnum; label: string }> {
    return [
      {
        key: ValueTypeEnum.Fixed,
        label: this.translateService.instant('GENERAL_TEXTS.VALUE_TYPE.FIXED'),
      },
      {
        key: ValueTypeEnum.Percentage,
        label: this.translateService.instant(
          'GENERAL_TEXTS.VALUE_TYPE.PERCENTAGE'
        ),
      },
    ];
  }
}

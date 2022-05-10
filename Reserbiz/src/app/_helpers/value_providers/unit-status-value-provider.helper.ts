import { TranslateService } from '@ngx-translate/core';

import { UnitStatusEnum } from '../../_enum/unit-status.enum';
import { IUnitStatusValueProvider } from '../../_interfaces/value_providers/iunit-status-value-provider.interface';

export class UnitStatusValueProvider implements IUnitStatusValueProvider {
  constructor(private translateService: TranslateService) {}

  get statusOptions(): Array<{ key: UnitStatusEnum; label: string }> {
    const options: { key: UnitStatusEnum; label: string }[] = [];

    options.push({
      key: UnitStatusEnum.All,
      label: this.translateService.instant(
        'GENERAL_TEXTS.UNIT_STATUS_OPTIONS.ALL'
      ),
    });

    options.push({
      key: UnitStatusEnum.Available,
      label: this.translateService.instant(
        'GENERAL_TEXTS.UNIT_STATUS_OPTIONS.AVAILABLE'
      ),
    });

    options.push({
      key: UnitStatusEnum.Occupied,
      label: this.translateService.instant(
        'GENERAL_TEXTS.UNIT_STATUS_OPTIONS.OCCUPIED'
      ),
    });

    return options;
  }
}

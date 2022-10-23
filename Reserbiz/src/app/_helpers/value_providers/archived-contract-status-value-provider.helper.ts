import { TranslateService } from '@ngx-translate/core';

import { ArchivedContractStatusEnum } from '../../_enum/archived-contract-options.enum';
import { IArchivedContractStatusValueProvider } from '../../_interfaces/value_providers/iarchived-contract-status-value-provider.interface';

export class ArchivedContractStatusValueProvider
  implements IArchivedContractStatusValueProvider
{
  constructor(private translateService: TranslateService) {}

  get statusOptions(): Array<{
    key: ArchivedContractStatusEnum;
    label: string;
  }> {
    const options: { key: ArchivedContractStatusEnum; label: string }[] = [];

    options.push({
      key: ArchivedContractStatusEnum.All,
      label: this.translateService.instant(
        'GENERAL_TEXTS.ARCHIVED_CONTRACT_STATUS_OPTIONS.ALL'
      ),
    });

    options.push({
      key: ArchivedContractStatusEnum.Expired,
      label: this.translateService.instant(
        'GENERAL_TEXTS.ARCHIVED_CONTRACT_STATUS_OPTIONS.EXPIRED'
      ),
    });

    options.push({
      key: ArchivedContractStatusEnum.Inactive,
      label: this.translateService.instant(
        'GENERAL_TEXTS.ARCHIVED_CONTRACT_STATUS_OPTIONS.INACTIVE'
      ),
    });

    return options;
  }
}

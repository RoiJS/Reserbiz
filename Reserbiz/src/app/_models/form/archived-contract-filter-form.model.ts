import { BaseForm } from './base-form.model';

import { SortOrderEnum } from '../../_enum/sort-order.enum';
import { ArchivedContractStatusEnum } from '../../_enum/archived-contract-options.enum';

export class ArchivedContractFilterFormSource extends BaseForm<ArchivedContractFilterFormSource> {
  constructor(
    public archivedContractStatus: ArchivedContractStatusEnum,
    public codeSortOrder: SortOrderEnum
  ) {
    super();
  }

  clone() {
    return new ArchivedContractFilterFormSource(
      this.archivedContractStatus,
      this.codeSortOrder
    );
  }
}

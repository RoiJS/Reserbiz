import { BaseForm } from './base-form.model';
import { SortOrderEnum } from '../../_enum/sort-order.enum';

export class ContractFilterFormSource extends BaseForm<
  ContractFilterFormSource
> {
  constructor(
    public tenantId: number,
    public activeFrom: Date,
    public activeTo: Date,
    public nextDueDateFrom: Date,
    public nextDueDateTo: Date,
    public openContract: boolean,
    public sortOrder: SortOrderEnum
  ) {
    super();
  }

  clone() {
    return new ContractFilterFormSource(
      this.tenantId,
      this.activeFrom,
      this.activeTo,
      this.nextDueDateFrom,
      this.nextDueDateTo,
      this.openContract,
      this.sortOrder,
    );
  }
}

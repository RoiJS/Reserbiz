import { SortOrderEnum } from '../../_enum/sort-order.enum';
import { IPenaltyFilter } from '../../_interfaces/filters/ipenalty-filter.interface';
import { EntityFilter } from './entity-filter.model';

export class PenaltyFilter extends EntityFilter implements IPenaltyFilter {
  constructor() {
    super();
    this.sortOrder = SortOrderEnum.Descending;
  }

  reset(): void {
    this.sortOrder = SortOrderEnum.Descending;
  }

  toFilterJSON() {
    return {
      page: this.page,
      accountStatementId: this.parentId,
      sortOrder: this.sortOrder,
    };
  }
}

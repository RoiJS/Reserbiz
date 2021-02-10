import { IEntityFilter } from '../_interfaces/ientity-filter.interface';
import { SortOrderEnum } from '../_enum/sort-order.enum';

export class EntityFilter implements IEntityFilter {
  public page: number;
  public parentId: number;
  public searchKeyword: string;
  public sortOrder: SortOrderEnum;

  constructor() {}

  reset() {}

  isFilterActive() {
    return false;
  }

  toFilterJSON() {
    return {};
  }
}

import { IEntityFilter } from '../_interfaces/ientity-filter.interface';

export class EntityFilter implements IEntityFilter {
  public page: number;
  public parentId: number;
  public searchKeyword: string;

  constructor() {}

  reset() {}

  isFilterActive() {
    return false;
  }

  toFilterJSON() {
    return {};
  }
}

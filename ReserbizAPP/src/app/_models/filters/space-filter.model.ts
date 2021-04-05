import { ISpaceFilter } from '../../_interfaces/filters/ispace-filter.interface';
import { EntityFilter } from './entity-filter.model';

export class SpaceFilter extends EntityFilter implements ISpaceFilter {
  constructor() {
    super();
  }

  reset(): void {
  }

  isFilterActive(): boolean {
    return false;
  }

  toFilterJSON() {
    return {
      page: this.page,
      searchKeyword: this.searchKeyword,
    };
  }
}

import { UnitStatusEnum } from '../../_enum/unit-status.enum';

import { ISpaceFilter } from '../../_interfaces/filters/ispace-filter.interface';
import { EntityFilter } from './entity-filter.model';

export class SpaceFilter extends EntityFilter implements ISpaceFilter {
  public status: UnitStatusEnum;
  public unitTypeId: number;
  constructor() {
    super();
    this.status = UnitStatusEnum.All;
    this.unitTypeId = 0;
  }

  reset(): void {
    this.status = UnitStatusEnum.All;
    this.unitTypeId = 0;
  }

  isFilterActive(): boolean {
    const hasFilter = Boolean(
      this.status !== UnitStatusEnum.All || this.unitTypeId > 0
    );
    return hasFilter;
  }

  toFilterJSON() {
    return {
      page: this.page,
      searchKeyword: this.searchKeyword,
      status: this.status,
      unitTypeId: this.unitTypeId,
    };
  }
}

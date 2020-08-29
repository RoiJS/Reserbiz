import { IContractFilter } from '../_interfaces/icontract-filter.interface';
import { EntityFilter } from './entity-filter.model';
import { DateFormatter } from '../_helpers/date-formatter.helper';

export class ContractFilter extends EntityFilter implements IContractFilter {
  public code: string;
  public tenantId: number;
  public activeFromFilter: Date;
  public activeToFilter: Date;
  public nextDueDateFromFilter: Date;
  public nextDueDateToFilter: Date;
  public openContract: boolean;
  public archived: boolean;
  public page: number;

  constructor() {
    super();
    this.tenantId = 0;
    this.activeFromFilter = null;
    this.activeToFilter = null;
    this.nextDueDateFromFilter = null;
    this.nextDueDateToFilter = null;
    this.openContract = false;
  }

  reset(): void {
    this.tenantId = null;
    this.activeToFilter = null;
    this.activeFromFilter = null;
    this.nextDueDateFromFilter = null;
    this.nextDueDateToFilter = null;
    this.openContract = null;
  }

  isFilterActive(): boolean {
    const hasFilter = Boolean(
      this.tenantId ||
        this.activeFromFilter ||
        this.activeToFilter ||
        this.nextDueDateFromFilter ||
        this.nextDueDateToFilter ||
        this.openContract
    );

    return hasFilter;
  }

  toFilterJSON() {
    return {
      tenantId: this.tenantId,
      activeFrom: DateFormatter.format(this.activeToFilter),
      activeTo: DateFormatter.format(this.activeFromFilter),
      nextDueDateFrom: DateFormatter.format(this.nextDueDateFromFilter),
      nextDueDateTo: DateFormatter.format(this.nextDueDateToFilter),
      openContract: this.openContract,
      archived: this.archived,
      page: this.page,
      searchKeyword: this.searchKeyword
    };
  }
}

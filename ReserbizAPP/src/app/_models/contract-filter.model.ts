import { IContractFilter } from '../_interfaces/icontract-filter.interface';
import { EntityFilter } from './entity-filter.model';

export class ContractFilter extends EntityFilter implements IContractFilter {
  public code: string;
  public tenantId: number;
  public activeFrom: Date;
  public activeTo: Date;
  public nextDueDateFrom: Date;
  public nextDueDateTo: Date;
  public openContract: boolean;
  public page: number;

  constructor() {
    super();
    this.tenantId = 0;
    this.activeFrom = null;
    this.activeTo = null;
    this.nextDueDateFrom = null;
    this.nextDueDateTo = null;
    this.openContract = false;
  }

  reset(): void {
    this.tenantId = null;
    this.activeFrom = null;
    this.activeTo = null;
    this.nextDueDateFrom = null;
    this.nextDueDateTo = null;
    this.openContract = null;
  }

  isFilterActive(): boolean {
    const hasFilter = Boolean(
      this.tenantId ||
        this.activeFrom ||
        this.activeTo ||
        this.nextDueDateFrom ||
        this.nextDueDateTo ||
        this.openContract
    );

    return hasFilter;
  }
}

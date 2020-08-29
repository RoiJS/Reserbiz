import { IEntityFilter } from './ientity-filter.interface';

export interface IContractFilter extends IEntityFilter {
  code: string;
  tenantId: number;
  activeFromFilter: Date;
  activeToFilter: Date;
  nextDueDateFromFilter: Date;
  nextDueDateToFilter: Date;
  openContract: boolean;
  archived: boolean;
}

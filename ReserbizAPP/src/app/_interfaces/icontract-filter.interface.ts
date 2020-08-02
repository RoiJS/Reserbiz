import { IEntityFilter } from './ientity-filter.interface';

export interface IContractFilter extends IEntityFilter {
  code: string;
  tenantId: number;
  activeFrom: Date;
  activeTo: Date;
  nextDueDateFrom: Date;
  nextDueDateTo: Date;
  openContract: boolean;
}

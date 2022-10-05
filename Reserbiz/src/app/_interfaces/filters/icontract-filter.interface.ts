import { IEntityFilter } from "./ientity-filter.interface";

import { ArchivedContractStatusEnum } from "~/app/_enum/archived-contract-options.enum";

export interface IContractFilter extends IEntityFilter {
  code: string;
  tenantId: number;
  activeFromFilter: Date;
  activeToFilter: Date;
  nextDueDateFromFilter: Date;
  nextDueDateToFilter: Date;
  openContract: boolean;
  archived: boolean;
  archivedContractStatus: ArchivedContractStatusEnum;
}

import { IEntityPaginationList } from './ientity-pagination-list.interface';

export interface IContractPaginationList
  extends IEntityPaginationList {
  totalNumberOfOpenContracts: number;
}

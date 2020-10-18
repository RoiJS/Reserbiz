import { IEntityPaginationList } from './ientity-pagination-list.interface';

export interface IAccountStatementPaginationList extends IEntityPaginationList {
  totalExpectedAmount: number;
  totalPaidAmount: number;
}

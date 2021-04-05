import { IEntityPaginationList } from './ientity-pagination-list.interface';

export interface IPenaltyPaginationList extends IEntityPaginationList {
  totalAmount: number;
}

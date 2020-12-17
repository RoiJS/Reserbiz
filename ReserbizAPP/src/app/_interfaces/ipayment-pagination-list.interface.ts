import { IEntityPaginationList } from './ientity-pagination-list.interface';

export interface IPaymentPaginationList extends IEntityPaginationList {
  totalAmount: number;
}

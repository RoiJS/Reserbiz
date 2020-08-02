import { IEntity } from './ientity.interface';

export interface IEntityPaginationList {
  totalItems: number;
  page: number;
  numberOfItemsPerPage: number;
  items: IEntity[];
}

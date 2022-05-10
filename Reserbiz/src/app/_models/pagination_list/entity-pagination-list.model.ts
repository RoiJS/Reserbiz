import { IEntityPaginationList } from '../../_interfaces/pagination_list/ientity-pagination-list.interface';
import { IEntity } from '../../_interfaces/ientity.interface';

export class EntityPaginationList implements IEntityPaginationList {
  public totalItems: number;
  public page: number;
  public numberOfItemsPerPage: number;
  public items: IEntity[];

  constructor() {
    this.totalItems = 0;
    this.page = 0;
    this.numberOfItemsPerPage = 0;
    this.items = [];
  }
}

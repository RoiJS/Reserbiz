import { IEntity } from '../../_interfaces/ientity.interface';
import { IUserNotificationPaginationList } from '../../_interfaces/pagination_list/iuser-notification-pagination-list.interface';

export class UserNotificationPaginationList
  implements IUserNotificationPaginationList
{
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

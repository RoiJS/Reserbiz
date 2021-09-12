import { SortOrderEnum } from '@src/app/_enum/sort-order.enum';
import { DateFormatter } from '@src/app/_helpers/formatters/date-formatter.helper';
import { IUserNotificationFilter } from '@src/app/_interfaces/filters/iuser-notification-filter.interface';
import { EntityFilter } from './entity-filter.model';

export class UserNotificationFilter
  extends EntityFilter
  implements IUserNotificationFilter
{
  public fromDate: Date;
  public toDate: Date;

  constructor() {
    super();
    this.fromDate = null;
    this.toDate = null;
    this.sortOrder = SortOrderEnum.Descending;
  }

  reset(): void {
    this.fromDate = null;
    this.toDate = null;
    this.sortOrder = SortOrderEnum.Descending;
  }

  isFilterActive(): boolean {
    const hasFilter = Boolean(
      this.fromDate ||
        this.toDate ||
        this.sortOrder !== SortOrderEnum.Descending
    );

    return hasFilter;
  }

  toFilterJSON() {
    return {
      page: this.page,
      fromDate: DateFormatter.format(this.fromDate),
      toDate: DateFormatter.format(this.toDate, 'YYYY-MM-DD 23:00'),
      sortOrder: this.sortOrder,
    };
  }
}

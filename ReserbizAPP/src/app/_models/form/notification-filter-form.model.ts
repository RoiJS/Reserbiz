import { SortOrderEnum } from '@src/app/_enum/sort-order.enum';
import { BaseForm } from './base-form.model';

export class NotificationFilterFormSource extends BaseForm<NotificationFilterFormSource> {
  constructor(
    public fromDate: Date,
    public toDate: Date,
    public sortOrder: SortOrderEnum
  ) {
    super();
  }
  clone(): NotificationFilterFormSource {
    return new NotificationFilterFormSource(
      this.fromDate,
      this.toDate,
      this.sortOrder
    );
  }
}

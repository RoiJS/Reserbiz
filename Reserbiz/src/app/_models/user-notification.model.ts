import { NotificationItemTypeEnum } from '../_enum/notification-item-type.enum';

import { DateFormatter } from '../_helpers/formatters/date-formatter.helper';

import { Entity } from './entity.model';

export class UserNotification extends Entity {
  public readStatus: boolean;
  public notificationMessage: string;
  public notificationUrl: string;
  public notificationDateTime: Date;
  public notificationDateTimeDaysAgo: number;
  public notificationFrom: string;
  public notificationItemType: NotificationItemTypeEnum;

  constructor() {
    super();
  }

  get notificationItemTypeIdentifier(): string {
    return this.notificationItemType === 1
      ? 'notificationheader'
      : 'notificationitem';
  }

  get userNotificationRelativeDateTimeFormatted(): string {
    if (this.notificationDateTimeDaysAgo > 1) {
      return DateFormatter.format(this.notificationDateTime, 'MMM DD, YYYY h:mm a');
    } else {
      return DateFormatter.relativeTimeFormat(this.notificationDateTime);
    }
  }

  get userNotificationDateTimeFormatted(): string {
    return DateFormatter.format(this.notificationDateTime, 'MMM DD, YYYY');
  }
}

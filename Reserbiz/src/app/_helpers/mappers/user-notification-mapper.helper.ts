import { IBaseEntityMapper } from '../../_interfaces/mappers/ibase-entity-mapper.interface';
import { UserNotification } from '../../_models/user-notification.model';

export class UserNotificationMapper
  implements IBaseEntityMapper<UserNotification>
{
  mapEntity(u: UserNotification): UserNotification {
    const userNotification = new UserNotification();

    userNotification.id = u.id;
    userNotification.readStatus = u.readStatus;
    userNotification.notificationMessage = u.notificationMessage;
    userNotification.notificationUrl = u.notificationUrl;
    userNotification.notificationDateTime = u.notificationDateTime;
    userNotification.notificationFrom = u.notificationFrom;
    userNotification.notificationItemType = u.notificationItemType;
    userNotification.notificationDateTimeDaysAgo = u.notificationDateTimeDaysAgo;

    return userNotification;
  }
}

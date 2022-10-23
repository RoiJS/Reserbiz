import { IEntityFilter } from './ientity-filter.interface';

export interface IUserNotificationFilter extends IEntityFilter {
  fromDate: Date;
  toDate: Date;
}

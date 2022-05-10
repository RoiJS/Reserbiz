import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { UserNotificationMapper } from '../_helpers/mappers/user-notification-mapper.helper';

import { IEntityFilter } from '../_interfaces/filters/ientity-filter.interface';
import { IUserNotificationFilter } from '../_interfaces/filters/iuser-notification-filter.interface';
import { IBaseService } from '../_interfaces/services/ibase-service.interface';

import { EntityPaginationList } from '../_models/pagination_list/entity-pagination-list.model';
import { UserNotification } from '../_models/user-notification.model';

import { BaseService } from './base.service';

@Injectable({ providedIn: 'root' })
export class UserNotificationService
  extends BaseService<UserNotification>
  implements IBaseService<UserNotification>
{
  private _loadUserNotificationListFlag = new BehaviorSubject<void>(null);
  private _unreadNotificationCount = new BehaviorSubject<number>(0);

  constructor(public http: HttpClient) {
    super(new UserNotificationMapper(), http);
  }

  getPaginatedEntities?(
    userNotificationFilter: IEntityFilter
  ): Observable<EntityPaginationList> {
    const params = <IUserNotificationFilter>(
      this.parseRequestParams(userNotificationFilter.toFilterJSON())
    );

    return this.getPaginatedEntitiesFromServer(
      `${this._apiBaseUrl}/userNotification/getUserNotifications`,
      params
    );
  }

  async getUserUnreadNotificationCount(): Promise<number> {
    return this.http
      .get<number>(
        `${this._apiBaseUrl}/userNotification/getUserUnreadNotificationsCount`
      )
      .toPromise();
  }

  setEntityStatus(
    userNotificationId: number,
    status: boolean
  ): Observable<void> {
    return this.setEntityStatusOnServer(
      `${this._apiBaseUrl}/userNotification/setReadStatus/${userNotificationId}/${status}`
    );
  }

  async checkNotificationUpdate(): Promise<void> {
    this._unreadNotificationCount.next(
      await this.getUserUnreadNotificationCount()
    );
  }

  reloadListFlag(reset?: boolean): void {
    this._loadUserNotificationListFlag.next();
  }

  get loadUserNotificationListFlag(): BehaviorSubject<void> {
    return this._loadUserNotificationListFlag;
  }

  get unreadNotificationCount(): BehaviorSubject<number> {
    return this._unreadNotificationCount;
  }
}

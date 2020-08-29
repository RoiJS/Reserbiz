import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { IEntity } from '../_interfaces/ientity.interface';
import { IEntityService } from '../_interfaces/ientity-service.interface';

@Injectable({ providedIn: 'root' })
export class EntityService<TEntity extends IEntity>
  implements IEntityService<TEntity> {
  private _entityDetails = new BehaviorSubject<TEntity>(null);
  private _entitySavedDetails = new BehaviorSubject<void>(null);
  private _cancelEntitySavedDetails = new BehaviorSubject<void>(null);

  constructor(private tentity: new () => TEntity) {
    this._entityDetails.next(new this.tentity());
  }

  resetEntityDetails() {
    this._entityDetails.next(new this.tentity());
  }

  isSame(otherEntityDetails: TEntity): boolean {
    return true;
  }

  get entityDetails(): BehaviorSubject<TEntity> {
    return this._entityDetails;
  }

  get entitySavedDetails(): BehaviorSubject<void> {
    return this._entitySavedDetails;
  }

  get entityCancelSaveDetails(): BehaviorSubject<void> {
    return this._cancelEntitySavedDetails;
  }
}

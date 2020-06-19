import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import { Entity } from '../_models/entity.model';
import { IEntityListService } from '../_interfaces/ientity-list-service.interface';

@Injectable({ providedIn: 'root' })
export class EntityListService<TEntity extends Entity>
  implements IEntityListService<TEntity> {
  private _entityList = new BehaviorSubject<TEntity[]>([]);

  constructor() {}

  getEntity(entityId: number): TEntity {
    const entity = this._entityList.value.find(
      (e: TEntity) => e.id === entityId
    );
    return entity;
  }

  addNewEntity(entity: TEntity) {
    entity.id = this.generateEntityId();
    this._entityList.value.push(entity);

    this._entityList.next(this._entityList.value);
  }

  updateEntity(entity: TEntity) {
    const index = this._entityList.value.findIndex(
      (e: TEntity) => e.id === entity.id
    );

    if (index === -1) {
      return;
    }

    this._entityList.value.splice(index, 1, entity);

    this._entityList.next(this._entityList.value);
  }

  removeEntity(entityId: number) {
    const index = this._entityList.value.findIndex(
      (e: TEntity) => e.id === entityId
    );

    if (index === -1) {
      return;
    }

    this._entityList.value.splice(index, 1);

    this._entityList.next(this._entityList.value);
  }

  resetEntityList() {
    this._entityList.next([]);
  }

  private generateEntityId(): number {
    let entityId = -1;

    if (this._entityList.value.length > 0) {
      entityId =
        this._entityList.value[this._entityList.value.length - 1].id - 1;
    }

    return entityId;
  }

  get entityList(): BehaviorSubject<TEntity[]> {
    return this._entityList;
  }
}

import { Entity } from '../../_models/entity.model';

export interface IEntityListService<TEntity extends Entity> {
  getEntity(entityId: number): TEntity;
  addNewEntity(entity: TEntity): void;
  updateEntity(entity: TEntity): void;
  removeEntity(entityId: number): void;
  resetEntityList(): void;
}

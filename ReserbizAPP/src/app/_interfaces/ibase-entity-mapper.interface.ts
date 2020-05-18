export interface IBaseEntityMapper<TEntity> {
  mapEntity(e: TEntity): TEntity;
}

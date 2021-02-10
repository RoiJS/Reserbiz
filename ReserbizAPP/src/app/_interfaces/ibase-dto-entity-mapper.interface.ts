import { IBaseDto } from './ibase-dto.interface';
import { IBaseFormSource } from './ibase-form-source.interface';
import { IEntity } from './ientity.interface';

export interface IBaseDtoEntityMapper<
  TEntity extends IEntity,
  TFormSource extends IBaseFormSource<TFormSource>,
  TDtoEntity extends IBaseDto
> {
  initFormSource(): TFormSource;
  mapFormSourceToDto(e: TFormSource): TDtoEntity;
  mapEntityToFormSource(e: TEntity): TFormSource;
  mapFormSourceToEntity(e: TFormSource): TEntity;
}

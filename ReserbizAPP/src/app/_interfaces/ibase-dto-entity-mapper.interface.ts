import { IBaseDTO } from './ibase-dto.interface';
import { IBaseFormSource } from './ibase-form-source.interface';
import { IEntity } from './ientity.interface';

export interface IBaseDtoEntityMapper<
  TEntity extends IEntity,
  TFormSource extends IBaseFormSource<TFormSource>,
  TDtoEntity extends IBaseDTO
> {
  initFormSource(): TFormSource;
  mapFormSourceToDto(e: TFormSource): TDtoEntity;
  mapEntityToFormSource(e: TEntity): TFormSource;
}

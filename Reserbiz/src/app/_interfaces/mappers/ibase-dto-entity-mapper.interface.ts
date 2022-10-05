import { IBaseDto } from "~/app/_interfaces/ibase-dto.interface";
import { IBaseFormSource } from "~/app/_interfaces/ibase-form-source.interface";
import { IEntity } from "~/app/_interfaces/ientity.interface";

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

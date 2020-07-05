import { Observable } from 'rxjs';

import { IEntityFilter } from './ientity-filter.interface';
import { IDtoProcess } from './idto-process.interface';

export interface IBaseService<IEntity> {
  getEntities(entityFilter: IEntityFilter): Observable<IEntity[]>;
  deleteMultipleItems(entities: IEntity[]): Observable<void>;
  deleteItem(entityId: number): Observable<void>;
  setEntityStatus?(entityId: number, status: boolean): Observable<void>;
  saveNewEntity?(dtoEntityProcess: IDtoProcess): Observable<void>;
  updateEntity?(dtoEntityProcess: IDtoProcess): Observable<void>;
  reloadListFlag(): void;
}

import { Observable } from 'rxjs';

import { IEntityFilter } from '../filters/ientity-filter.interface';
import { IDtoProcess } from '../idto-process.interface';
import { IEntityPaginationList } from '../pagination_list/ientity-pagination-list.interface';

export interface IBaseService<IEntity> {
  getEntities?(entityFilter: IEntityFilter): Observable<IEntity[]>;
  getPaginatedEntities?(
    entityFilter: IEntityFilter
  ): Observable<IEntityPaginationList>;
  deleteMultipleItems?(entities: IEntity[]): Observable<void>;
  deleteItem?(entityId: number): Observable<void>;
  setEntityStatus?(entityId: number, status: boolean): Observable<void>;
  setMultipleEntityStatus?(entities: IEntity[], status: boolean): Observable<void>;
  saveNewEntity?(dtoEntityProcess: IDtoProcess): Observable<void>;
  updateEntity?(dtoEntityProcess: IDtoProcess): Observable<void>;
  validateEntityForDeletion?(entityId: number): Promise<boolean>;
  reloadListFlag(reset?: boolean): void;
}

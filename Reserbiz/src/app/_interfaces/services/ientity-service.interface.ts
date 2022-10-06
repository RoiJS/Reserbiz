import { BehaviorSubject } from 'rxjs';

import { IEntity } from '../ientity.interface';

export interface IEntityService<TEntity extends IEntity> {
  entityDetails: BehaviorSubject<TEntity>;
  entitySavedDetails: BehaviorSubject<boolean>;
  entityCancelSaveDetails: BehaviorSubject<void>;
  resetEntityDetails();
}

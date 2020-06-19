import { Injectable } from '@angular/core';

import { TermMiscellaneous } from '../_models/term-miscellaneous.model';
import { EntityListService } from './entity-list.service';

@Injectable({ providedIn: 'root' })
export class AddTermMiscellaneousService extends EntityListService<
  TermMiscellaneous
> {
  constructor() {
    super();
  }
}

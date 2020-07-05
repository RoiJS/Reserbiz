import { Injectable } from '@angular/core';
import { Term } from '../_models/term.model';
import { EntityService } from './entity.service';

@Injectable({ providedIn: 'root' })
export class AddTermService extends EntityService<Term> {
  constructor() {
    super(Term);
  }
}

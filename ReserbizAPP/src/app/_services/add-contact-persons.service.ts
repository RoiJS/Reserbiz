import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { ContactPerson } from '../_models/contact-person.model';
import { EntityListService } from './entity-list.service';

@Injectable({ providedIn: 'root' })
export class AddContactPersonsService extends EntityListService<ContactPerson> {
  constructor() {
    super();
  }
}

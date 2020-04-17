import {
  Component,
  OnInit,
  Input,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { ContactPerson } from '@src/app/_models/contact-person.model';

import { ObservableArray } from 'tns-core-modules/data/observable-array';

@Component({
  selector: 'ns-tenant-contact-person-list',
  templateUrl: './tenant-contact-person-list.component.html',
  styleUrls: ['./tenant-contact-person-list.component.scss'],
})
export class TenantContactPersonListComponent implements OnInit, OnChanges {
  @Input() contactPersons: ContactPerson[];

  constructor() {}

  ngOnChanges(changes: SimpleChanges): void {}

  ngOnInit() {}

  get contactPersonList(): ContactPerson[] {
    return this.contactPersons;
  }
}

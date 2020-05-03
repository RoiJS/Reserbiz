import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ContactPerson } from '../_models/contact-person.model';

@Injectable({ providedIn: 'root' })
export class AddContactPersonsService {
  private _contactPersonList = new BehaviorSubject<ContactPerson[]>([]);

  constructor() {}

  resetContactPersonList() {
    this._contactPersonList.next([]);
  }

  getContactPerson(contactPersonId: number): ContactPerson {
    const contactPerson = this._contactPersonList.value.find(
      (c: ContactPerson) => c.id === contactPersonId
    );
    return contactPerson;
  }

  addNewContactPerson(contactPerson: ContactPerson) {
    contactPerson.id = this.generateContactPersonId();
    this._contactPersonList.value.push(contactPerson);

    this._contactPersonList.next(this._contactPersonList.value);
  }

  updateContactPerson(contactPerson: ContactPerson) {
    const index = this._contactPersonList.value.findIndex(
      (c: ContactPerson) => c.id === contactPerson.id
    );

    if (index === -1) {
      return;
    }

    this._contactPersonList.value.splice(index, 1, contactPerson);

    this._contactPersonList.next(this._contactPersonList.value);
  }

  public removeContactPerson(contactPersonId: number) {
    const index = this._contactPersonList.value.findIndex(
      (c: ContactPerson) => c.id === contactPersonId
    );

    if (index === -1) {
      return;
    }

    this._contactPersonList.value.splice(index, 1);

    this._contactPersonList.next(this._contactPersonList.value);
  }

  private generateContactPersonId(): number {
    let contactPersonId = -1;

    if (this._contactPersonList.value.length > 0) {
      contactPersonId =
        this._contactPersonList.value[this._contactPersonList.value.length - 1]
          .id - 1;
    }

    return contactPersonId;
  }

  get contactList(): BehaviorSubject<ContactPerson[]> {
    return this._contactPersonList;
  }
}

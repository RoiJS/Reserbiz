import { BaseForm } from './base-form.model';
import { GenderEnum } from '../_enum/gender.enum';

export class ContactPersonDetailsFormSource extends BaseForm<
  ContactPersonDetailsFormSource
> {
  constructor(
    public firstName: string,
    public middleName: string,
    public lastName: string,
    public gender: GenderEnum,
    public contactNumber: string
  ) {
    super();
  }

  clone() {
    return new ContactPersonDetailsFormSource(
      this.firstName,
      this.middleName,
      this.lastName,
      this.gender,
      this.contactNumber
    );
  }
}

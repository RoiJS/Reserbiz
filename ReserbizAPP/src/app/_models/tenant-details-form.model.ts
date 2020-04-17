import { BaseForm } from './base-form.model';
import { GenderEnum } from '../_enum/gender.enum';

export class TenantDetailsFormSource extends BaseForm<TenantDetailsFormSource> {
  constructor(
    public firstName: string,
    public middleName: string,
    public lastName: string,
    public gender: GenderEnum,
    public address: string,
    public contactNumber: string,
    public emailAddress: string
  ) {
    super();
  }

  clone() {
    return new TenantDetailsFormSource(
      this.firstName,
      this.middleName,
      this.lastName,
      this.gender,
      this.address,
      this.contactNumber,
      this.emailAddress
    );
  }
}

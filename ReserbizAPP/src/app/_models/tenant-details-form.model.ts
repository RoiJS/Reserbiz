import { BaseForm } from './base-form.model';
import { GenderEnum } from '../_enum/gender.enum';
import { IBaseFormSource } from '../_interfaces/ibase-form-source.interface';

export class TenantDetailsFormSource extends BaseForm<TenantDetailsFormSource>
  implements IBaseFormSource<TenantDetailsFormSource> {
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

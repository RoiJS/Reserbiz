import { GenderEnum } from '../../_enum/gender.enum';
import { BaseForm } from './base-form.model';

export class UserPersonalInfoFormSource extends BaseForm<
  UserPersonalInfoFormSource
> {
  constructor(
    public firstName: string,
    public middleName: string,
    public lastName: string,
    public gender: GenderEnum
  ) {
    super();
  }

  clone() {
    return new UserPersonalInfoFormSource(
      this.firstName,
      this.middleName,
      this.lastName,
      this.gender
    );
  }
}

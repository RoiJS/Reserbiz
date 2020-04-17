import { GenderEnum } from '../_enum/gender.enum';

export class TenantUpdateDto {
  constructor(
    public firstName: string,
    public middleName: string,
    public lastName: string,
    public gender: GenderEnum,
    public address: string,
    public contactNumber: string,
    public emailAddress: string
  ) {}
}

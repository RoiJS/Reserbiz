import { GenderEnum } from '../_enum/gender.enum';
import { ContactPersonCreateDto } from './contact-person-create.dto';

export class TenantCreateDto {
  public contactPersons?: ContactPersonCreateDto[];

  constructor(
    public firstName: string,
    public middleName: string,
    public lastName: string,
    public gender: GenderEnum,
    public address: string,
    public contactNumber: string,
    public emailAddress: string
  ) {
    this.contactPersons = [];
  }
}

import { GenderEnum } from '../_enum/gender.enum';
import { ContactPersonDto } from './contact-person.dto';

import { Entity } from '../_models/entity.model';

export class TenantDto extends Entity {
  public contactPersons?: ContactPersonDto[];

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
    this.contactPersons = [];
  }
}

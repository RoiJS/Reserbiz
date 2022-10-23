import { GenderEnum } from '../_enum/gender.enum';
import { IBaseDto } from '../_interfaces/ibase-dto.interface';

export class ContactPersonDto implements IBaseDto {
  constructor(
    public firstName: string,
    public middleName: string,
    public lastName: string,
    public gender: GenderEnum,
    public contactNumber: string,
    public relation: string
  ) {}
}

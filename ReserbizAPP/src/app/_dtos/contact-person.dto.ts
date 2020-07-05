import { GenderEnum } from '../_enum/gender.enum';
import { IBaseDTO } from '../_interfaces/ibase-dto.interface';

export class ContactPersonDto implements IBaseDTO {
  constructor(
    public firstName: string,
    public middleName: string,
    public lastName: string,
    public gender: GenderEnum,
    public contactNumber: string
  ) {}
}

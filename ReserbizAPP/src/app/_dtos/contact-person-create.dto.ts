import { GenderEnum } from '../_enum/gender.enum';

export class ContactPersonCreateDto {
  constructor(
    public firstName: string,
    public middleName: string,
    public lastName: string,
    public gender: GenderEnum,
    public contactNumber: string,
  ) {}
}

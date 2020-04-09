import { GenderEnum } from '../_enum/gender.enum';

export class ContactPerson {
  constructor(
    public id: number,
    public firstName: string,
    public middlename: string,
    public lastName: string,
    public gender: GenderEnum,
    public contactNumber: string,
    public tenanId: number
  ) {}

  get fullName(): string {
    return `${this.firstName} ${this.lastName}`;
  }
}

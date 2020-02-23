import { GenderEnum } from '../_enum/gender.enum';

export class Tenant {
  constructor(
    public Id: number,
    public FirstName: string,
    public MiddleName: string,
    public LastName: string,
    public Gender: GenderEnum,
    public Address: string,
    public ContactNumber: string,
    public EmailAddress: string
  ) {}

  public FullName(): string {
    return `${this.FirstName} ${this.LastName}`;
  }
}

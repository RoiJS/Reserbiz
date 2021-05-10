import { GenderEnum } from '../_enum/gender.enum';
import { User } from './user.model';

export class ForgotPasswordUser extends User {
  constructor(
    public id: number,
    public firstName: string,
    public middleName: string,
    public lastName: string,
    public username: string,
    public gender: GenderEnum,
    public emailAddress: string
  ) {
    super(firstName, middleName, lastName, username, gender, emailAddress);
  }
}

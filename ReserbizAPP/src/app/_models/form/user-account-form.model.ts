import { BaseForm } from './base-form.model';

export class UserAccountInfoFormSource extends BaseForm<UserAccountInfoFormSource> {
  constructor(
    public username: string,
    public emailAddress: string,
    public password: string,
    public confirmPassword: string
  ) {
    super();
  }

  clone() {
    return new UserAccountInfoFormSource(
      this.username,
      this.emailAddress,
      this.password,
      this.confirmPassword
    );
  }
}

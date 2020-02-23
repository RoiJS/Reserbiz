import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';

import { AuthService } from '@src/app/_services/auth.service';
import { User } from '@src/app/_models/user.model';
import { UserAccountInfoFormSource } from '@src/app/_models/user-account-form.model';

@Component({
  selector: 'ns-profile-account-info',
  templateUrl: './profile-account-info.component.html',
  styleUrls: ['./profile-account-info.component.scss']
})
export class ProfileAccountInfoComponent implements OnInit {
  private _userAccountFormSource: UserAccountInfoFormSource;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.user.pipe(take(1)).subscribe((currentUser: User) => {
      this._userAccountFormSource = new UserAccountInfoFormSource(currentUser.username, '', '');
    });
  }

  get userAccountFormSource(): UserAccountInfoFormSource {
    return this._userAccountFormSource;
  }
}

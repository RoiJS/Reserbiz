import { Component, OnInit, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';

import { AuthService } from '../../_services/auth.service';
import { User } from '../../_models/user.model';

@Component({
  selector: 'ns-profile-information',
  templateUrl: './profile-information.component.html',
  styleUrls: ['./profile-information.component.scss'],
})
export class ProfileInformationComponent implements OnInit, OnDestroy {
  private _currentUser: User;
  private _currentUserSub: Subscription;

  private _isBusy = false;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this._currentUserSub = this.authService.user.subscribe(
      (currentUser: User) => {
        this._currentUser = currentUser;
      }
    );
  }

  ngOnDestroy(): void {
    if (this._currentUserSub) {
      this._currentUserSub.unsubscribe();
    }
  }

  get IsBusy(): boolean {
    return this._isBusy;
  }

  get currentUser(): User {
    return this._currentUser;
  }
}

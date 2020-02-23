import { Component, OnInit, ViewChild } from '@angular/core';

import { RadDataFormComponent } from 'nativescript-ui-dataform/angular/dataform-directives';

import { take } from 'rxjs/operators';

import { AuthService } from '@src/app/_services/auth.service';

import { GenderEnum } from '@src/app/_enum/gender.enum';
import { User } from '@src/app/_models/user.model';
import { UserPersonalInfoFormSource } from '@src/app/_models/user-personal-form.model';
import { DialogService } from '@src/app/_services/dialog.service';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

@Component({
  selector: 'ns-profile',
  templateUrl: './profile-personal-info.component.html',
  styleUrls: ['./profile-personal-info.component.scss']
})
export class ProfilePersonalInfoComponent implements OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  profileForm: RadDataFormComponent;

  private _userFormSource: UserPersonalInfoFormSource;
  private _userFormSourceOriginal: UserPersonalInfoFormSource;

  constructor(
    private authService: AuthService,
    private dialogService: DialogService
  ) {}

  ngOnInit() {
    this.authService.user.pipe(take(1)).subscribe((currentUser: User) => {
      this._userFormSource = new UserPersonalInfoFormSource(
        currentUser.firstName,
        currentUser.middleName,
        currentUser.lastName,
        currentUser.gender
      );

      this._userFormSourceOriginal = this._userFormSource.clone();
    });
  }

  savePersonalInformation() {
    const isFormInvalid = this.profileForm.dataForm.hasValidationErrors();
    const isFormHasChanged = !this._userFormSource.isSame(
      this._userFormSourceOriginal
    );

    if (!isFormInvalid && isFormHasChanged) {
      this.dialogService
        .confirm('Update Profile', 'Are you sure to save changes?')
        .then(res => {
          if (res === ButtonOptions.YES) {
            this.authService
              .updatePersonalInformation(this._userFormSource)
              .subscribe(
                () => {
                  this.dialogService.alert(
                    'Update Profile',
                    'New changes has been saved!',
                    () => {
                      console.log('success!!');
                    }
                  );

                  this._userFormSourceOriginal = this._userFormSource.clone();
                },
                (error: Error) => {
                  this.dialogService.alert(
                    'Update Profile',
                    `Update information failed. ${error.message}`
                  );
                }
              );
          }
        });
    }
  }

  get userFormSource(): UserPersonalInfoFormSource {
    return this._userFormSource;
  }

  get genderOptions(): Array<{ key: GenderEnum; label: string }> {
    return [
      { key: GenderEnum.Male, label: 'Male' },
      { key: GenderEnum.Female, label: 'Female' }
    ];
  }
}

import { Component, OnInit, Input } from '@angular/core';

import { RouterExtensions } from 'nativescript-angular/router';

import { GenderEnum } from '@src/app/_enum/gender.enum';

@Component({
  selector: 'ns-profile-general-information',
  templateUrl: './profile-general-information.component.html',
  styleUrls: ['./profile-general-information.component.scss'],
})
export class ProfileGeneralInformationComponent implements OnInit {
  @Input() fullName: string;
  @Input() gender: GenderEnum;

  constructor(private router: RouterExtensions) {}

  ngOnInit() {}

  onNavigateToEditGeneralInformation() {
    this.router.navigate(['profile/personalInfo'], {
      transition: {
        name: 'slideLeft',
      },
    });
  }
}

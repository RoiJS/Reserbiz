import { Component, OnInit, Input } from '@angular/core';
import { RouterExtensions } from '@nativescript/angular';

@Component({
  selector: 'ns-profile-account-information',
  templateUrl: './profile-account-information.component.html',
  styleUrls: ['./profile-account-information.component.scss'],
})
export class ProfileAccountInformationComponent implements OnInit {
  @Input() username: string;

  constructor(private router: RouterExtensions) {}

  ngOnInit() {}

  onNavigateToEditAccountInformation() {
    this.router.navigate(['profile/accountInfo'], {
      transition: {
        name: 'slideLeft',
      },
    });
  }
}

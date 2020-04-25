import { Component, OnInit, Input } from '@angular/core';
import { RouterExtensions } from 'nativescript-angular/router/router-extensions';

import { ContactPerson } from '@src/app/_models/contact-person.model';

@Component({
  selector: 'ns-tenant-contact-person-list-panel',
  templateUrl: './tenant-contact-person-list-panel.component.html',
  styleUrls: ['./tenant-contact-person-list-panel.component.scss'],
})
export class TenantContactPersonListPanelComponent implements OnInit {
  @Input() contactPersons: ContactPerson[];
  @Input() tenantId: number;

  constructor(private router: RouterExtensions) {}
  ngOnInit() {}

  onNavigateToManageContactPersonList() {
    this.router.navigate(
      [`/tenants/${this.tenantId}/contact-person-list`],
      {
        transition: {
          name: 'slideLeft',
        },
      }
    );
  }

  get contactPersonList(): ContactPerson[] {
    return this.contactPersons;
  }
}

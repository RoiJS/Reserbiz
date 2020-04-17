import { Component, OnInit, Input } from '@angular/core';
import { PageRoute, RouterExtensions } from 'nativescript-angular/router';

import { TenantService } from '@src/app/_services/tenant.service';
import { Tenant } from '@src/app/_models/tenant.model';
import { ContactPerson } from '@src/app/_models/contact-person.model';

@Component({
  selector: 'ns-tenant-information',
  templateUrl: './tenant-information.component.html',
  styleUrls: ['./tenant-information.component.scss'],
})
export class TenantInformationComponent implements OnInit {
  @Input() currentTenant: Tenant;

  constructor() {}

  ngOnInit() {}
}

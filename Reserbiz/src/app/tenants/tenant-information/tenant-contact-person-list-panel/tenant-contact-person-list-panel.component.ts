import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { RouterExtensions } from '@nativescript/angular';

import { Subscription } from 'rxjs';
import { take } from 'rxjs/operators';

import { ContactPersonService } from '../../../_services/contact-person.service';
import { ContactPerson } from '../../../_models/contact-person.model';
import { EntityFilter } from '../../../_models/filters/entity-filter.model';

@Component({
  selector: 'ns-tenant-contact-person-list-panel',
  templateUrl: './tenant-contact-person-list-panel.component.html',
  styleUrls: ['./tenant-contact-person-list-panel.component.scss'],
})
export class TenantContactPersonListPanelComponent
  implements OnInit, OnDestroy {
  @Input() tenantId: number;

  private _contactPersons: ContactPerson[];
  private _updateContactPersonListFlag: Subscription;

  constructor(
    private contactPersonService: ContactPersonService,
    private router: RouterExtensions
  ) {}
  ngOnInit() {
    this._updateContactPersonListFlag = this.contactPersonService.updateContactPersonListFlag.subscribe(
      () => {
        this.getContactPersonList();
      }
    );
  }

  ngOnDestroy() {
    this._updateContactPersonListFlag.unsubscribe();
  }

  getContactPersonList() {
    setTimeout(() => {
      const filter = new EntityFilter();
      filter.parentId = this.tenantId;

      this.contactPersonService
        .getEntities(filter)
        .pipe(take(1))
        .subscribe((contactPersons: ContactPerson[]) => {
          this._contactPersons = contactPersons;
        });
    }, 500);
  }

  onNavigateToManageContactPersonList() {
    this.router.navigate([`/tenants/${this.tenantId}/contact-person-list`], {
      transition: {
        name: 'slideLeft',
      },
    });
  }

  get contactPersonList(): ContactPerson[] {
    return this._contactPersons;
  }
}

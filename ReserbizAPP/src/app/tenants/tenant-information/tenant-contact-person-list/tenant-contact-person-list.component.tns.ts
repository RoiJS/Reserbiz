import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';
import { PageRoute, RouterExtensions } from '@nativescript/angular';

import { TranslateService } from '@ngx-translate/core';

import { DialogService } from '@src/app/_services/dialog.service';
import { ContactPersonService } from '@src/app/_services/contact-person.service';
import { ContactPerson } from '@src/app/_models/contact-person.model';
import { BaseListComponent } from '@src/app/shared/component/base-list.component';
import { IBaseListComponent } from '@src/app/_interfaces/components/ibase-list-component.interface';

@Component({
  selector: 'ns-tenant-contact-list',
  templateUrl: './tenant-contact-person-list.component.html',
  styleUrls: ['./tenant-contact-person-list.component.scss'],
})
export class TenantContactPersonListComponent
  extends BaseListComponent<ContactPerson>
  implements IBaseListComponent, OnInit, OnDestroy {
  private currentTenantId: number;
  constructor(
    private contactPersonService: ContactPersonService,
    private pageRoute: PageRoute,
    activatedRoute: ActivatedRoute,
    dialogService: DialogService,
    ngZone: NgZone,
    location: Location,
    router: RouterExtensions,
    translateService: TranslateService
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.activatedRoute = activatedRoute;
    this.entityService = contactPersonService;
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this.currentTenantId = +paramMap.get('tenantId');

        this._loadListFlagSub = this.contactPersonService.updateContactPersonListFlag.subscribe(
          () => {
            this._entityFilter.parentId = this.currentTenantId;
            this.getEntities();
          }
        );
      });
    });

    this.initDialogTexts();
    super.ngOnInit();
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }

  initDialogTexts() {
    this._deleteMultipleItemsDialogTexts = {
      title: this.translateService.instant(
        'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSONS_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSONS_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSONS_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSONS_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._deleteItemDialogTexts = {
      title: this.translateService.instant(
        'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.ERROR_MESSAGE'
      ),
    };
  }

  goToAddContactPesonPage() {
    this.navigateToOtherPage(`${this.currentTenantId}/add`);
  }

  goToEditDetailsFromOptionItem() {
    this.navigateToOtherPage(this._currentItem.id.toString());
  }
}

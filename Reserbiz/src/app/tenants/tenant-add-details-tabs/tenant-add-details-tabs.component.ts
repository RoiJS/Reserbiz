import { Component, OnInit, OnDestroy } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";

import { Subscription } from "rxjs";
import { finalize } from "rxjs/operators";

import { RouterExtensions } from "@nativescript/angular";

import { Tenant } from "~/app/_models/tenant.model";
import { ContactPerson } from "~/app/_models/contact-person.model";
import { AddContactPersonsService } from "~/app/_services/add-contact-persons.service";
import { DialogService } from "~/app/_services/dialog.service";
import { AddTenantService } from "~/app/_services/add-tenant.service";
import { TenantService } from "~/app/_services/tenant.service";

@Component({
  selector: "ns-tenant-add-details",
  templateUrl: "./tenant-add-details-tabs.component.html",
  styleUrls: ["./tenant-add-details-tabs.component.scss"],
})
export class TenantAddDetailsTabsComponent implements OnInit, OnDestroy {
  private _actionBarTitle: string[];
  private _currentActionBarTitle: string;
  private _tenantDetailsSub: Subscription;
  private _contactPersonsSub: Subscription;
  private _tenantDetails: Tenant;
  private _contactPersons: ContactPerson[];
  private _isBusy = false;

  constructor(
    private addTenantService: AddTenantService,
    private addContactPersonsService: AddContactPersonsService,
    private dialogService: DialogService,
    private tenantService: TenantService,
    private translateService: TranslateService,
    private router: RouterExtensions
  ) {}

  ngOnInit() {
    this.initializeActionBarTitles();

    this._tenantDetailsSub = this.addTenantService.entityDetails
      .asObservable()
      .subscribe((tenant: Tenant) => {
        this._tenantDetails = tenant;
      });

    this._contactPersonsSub = this.addContactPersonsService.entityList
      .asObservable()
      .subscribe((contactPersons: ContactPerson[]) => {
        this._contactPersons = contactPersons;
      });
  }

  ngOnDestroy() {
    if (this._tenantDetailsSub) {
      this._tenantDetailsSub.unsubscribe();
    }

    if (this._contactPersonsSub) {
      this._contactPersonsSub.unsubscribe();
    }
  }

  initializeActionBarTitles() {
    this._actionBarTitle = [];

    this._actionBarTitle.push(
      this.translateService.instant("TENANTS_DETAILS_PAGE.ACTION_BAR_TITLE")
    );

    this._actionBarTitle.push(
      this.translateService.instant(
        "TENANT_CONTACT_PERSON_LIST_PAGE.ACTION_BAR_TITLE"
      )
    );
  }

  tabsIndexChanged(event: any) {
    this._currentActionBarTitle = this._actionBarTitle[event.newIndex];
  }

  saveInformation() {
    this.addTenantService.entitySavedDetails.next(true);
  }

  onTenantDetailsSaved(e: { newTenant: Tenant; isFormValid: boolean }) {
    // Check if new tenant form is valid
    if (!e.isFormValid) {
      this.dialogService.alert(
        this.translateService.instant(
          "TENANTS_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE"
        ),
        this.translateService.instant(
          "TENANTS_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.INVALID_FORM"
        )
      );
      return;
    }

    const newTenant = e.newTenant;
    const newContactPersons = this.addContactPersonsService.entityList.value;
    // Save the new tenant information
    this.dialogService
      .confirm(
        this.translateService.instant(
          "TENANTS_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE"
        ),
        this.translateService.instant(
          "TENANTS_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.CONFIRM_MESSAGE"
        )
      )
      .then((result: boolean) => {
        if (result) {
          this._isBusy = true;
          this.tenantService
            .saveNewTenant(newTenant, newContactPersons)
            .pipe(
              finalize(() => {
                this._isBusy = false;
              })
            )
            .subscribe({
              next: () => {
                this.addTenantService.resetEntityDetails();
                this.addContactPersonsService.resetEntityList();

                this.dialogService
                  .alert(
                    this.translateService.instant(
                      "TENANTS_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE"
                    ),
                    this.translateService.instant(
                      "TENANTS_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.SUCCESS_MESSAGE"
                    )
                  )
                  .then(() => {
                    this.tenantService.loadTenantListFlag.next();
                    this.router.back();
                  });
              },
              error: () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    "TENANTS_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE"
                  ),
                  this.translateService.instant(
                    "TENANTS_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.ERROR_MESSAGE"
                  )
                );
              },
            });
        }
      });
  }

  navigateBack() {
    this.addTenantService.entityCancelSaveDetails.next();
  }

  onCancelTenantDetailsSaved(e: boolean) {
    if (e) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            "TENANTS_ADD_DETAILS_PAGE.FORM_CONTROL.LEAVE_PAGE_DIALOG.TITLE"
          ),
          this.translateService.instant(
            "TENANTS_ADD_DETAILS_PAGE.FORM_CONTROL.LEAVE_PAGE_DIALOG.CONFIRM_MESSAGE"
          )
        )
        .then((result: boolean) => {
          if (result) {
            this.addTenantService.resetEntityDetails();
            this.addContactPersonsService.resetEntityList();
            this.router.back();
          }
        });
    } else {
      this.router.back();
    }
  }

  get currentActionBarTitle(): string {
    return this._currentActionBarTitle;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }
}

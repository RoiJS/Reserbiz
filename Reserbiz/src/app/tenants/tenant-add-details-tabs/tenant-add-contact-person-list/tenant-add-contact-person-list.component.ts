import { Location } from "@angular/common";
import { Component, OnInit, ViewChild, OnDestroy, NgZone } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

import { Subscription } from "rxjs";

import { ListViewEventData } from "nativescript-ui-listview";

import { TranslateService } from "@ngx-translate/core";

import { ObservableArray } from "@nativescript/core";
import { RouterExtensions } from "@nativescript/angular";
import { RadListViewComponent } from "nativescript-ui-listview/angular";

import { AddContactPersonsService } from "~/app/_services/add-contact-persons.service";
import { DialogService } from "~/app/_services/dialog.service";

import { ContactPerson } from "~/app/_models/contact-person.model";

@Component({
  selector: "ns-tenant-add-contact-person-list",
  templateUrl: "./tenant-add-contact-person-list.component.html",
  styleUrls: ["./tenant-add-contact-person-list.component.scss"],
})
export class TenantAddContactPersonListComponent implements OnInit, OnDestroy {
  @ViewChild("contactPersonView", { static: false })
  contactPersonView: RadListViewComponent;

  private _isBusy = false;
  private _contactPersonListSub: Subscription;

  private _contactPersons: ObservableArray<ContactPerson>;
  private _isNotNavigateToOtherPage = true;
  private _navigateBackSub: any;

  constructor(
    private addContactPersonsService: AddContactPersonsService,
    private active: ActivatedRoute,
    private dialogService: DialogService,
    private location: Location,
    private ngZone: NgZone,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.getContactPersonList();

    this._navigateBackSub = this.location.subscribe(() => {
      this._isNotNavigateToOtherPage = true;
    });
  }

  getContactPersonList() {
    this._isBusy = true;

    setTimeout(() => {
      this._contactPersonListSub =
        this.addContactPersonsService.entityList.subscribe(
          (contactPersons: ContactPerson[]) => {
            this._isBusy = false;
            this._contactPersons = new ObservableArray<ContactPerson>(
              contactPersons
            );
            setTimeout(() => {
              this.contactPersonView.listView.refresh();
            }, 1000);
          }
        );
    }, 500);
  }

  ngOnDestroy() {
    if (this._contactPersonListSub) {
      this._contactPersonListSub.unsubscribe();
    }

    if (this._navigateBackSub) {
      this._navigateBackSub.unsubscribe();
    }
  }

  goToEditDetailsFromMainItem(args: ListViewEventData) {
    const selectedContactPerson = this._contactPersons.getItem(args.index);
    this._isNotNavigateToOtherPage = false;
    this.navigateToEditPage(selectedContactPerson.id);
  }

  navigateToEditPage(contactPersonId: number) {
    setTimeout(() => {
      this.router.navigate([`edit-contact-person/${contactPersonId}`], {
        relativeTo: this.active,
        transition: {
          name: "slideLeft",
        },
      });
    }, 100);
  }

  goToAddContactPesonPage() {
    setTimeout(() => {
      this.router.navigate(["add-contact-person"], {
        relativeTo: this.active,
        transition: {
          name: "slideLeft",
        },
      });
    }, 100);
  }

  deleteSelectedContactPerson(contactPersonId: number) {
    this.dialogService
      .confirm(
        this.translateService.instant(
          "TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.TITLE"
        ),
        this.translateService.instant(
          "TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.CONFIRM_MESSAGE"
        )
      )
      .then((res: boolean) => {
        if (res) {
          this._isBusy = true;

          setTimeout(() => {
            this.dialogService
              .alert(
                this.translateService.instant(
                  "TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.TITLE"
                ),
                this.translateService.instant(
                  "TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.SUCCESS_MESSAGE"
                )
              )
              .then(() => {
                this.ngZone.run(() => {
                  this._isBusy = false;
                  this.addContactPersonsService.removeEntity(contactPersonId);
                });
              });
          }, 1000);
        }
      });
  }

  get contactPersons(): ObservableArray<ContactPerson> {
    return this._contactPersons;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }

  get isNotNavigateToOtherPage(): boolean {
    return this._isNotNavigateToOtherPage;
  }
}

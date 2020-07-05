import { Location } from '@angular/common';
import { Component, OnInit, ViewChild, OnDestroy, NgZone } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';

import { ListViewEventData } from 'nativescript-ui-listview';

import { TranslateService } from '@ngx-translate/core';

import { ObservableArray } from 'tns-core-modules/data/observable-array/observable-array';
import { RadListViewComponent } from 'nativescript-ui-listview/angular/listview-directives';
import { RouterExtensions } from 'nativescript-angular/router';

import { AddContactPersonsService } from '@src/app/_services/add-contact-persons.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { ContactPerson } from '@src/app/_models/contact-person.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

@Component({
  selector: 'ns-tenant-add-contact-person-list',
  templateUrl: './tenant-add-contact-person-list.component.html',
  styleUrls: ['./tenant-add-contact-person-list.component.scss'],
})
export class TenantAddContactPersonListComponent implements OnInit, OnDestroy {
  @ViewChild('contactPersonView', { static: false })
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
      this._contactPersonListSub = this.addContactPersonsService.entityList
        .asObservable()
        .subscribe((contactPersons: ContactPerson[]) => {
          this._isBusy = false;
          this._contactPersons = new ObservableArray<ContactPerson>(
            contactPersons
          );
        });
    }, 500);
  }

  ngOnDestroy() {
    this._contactPersonListSub.unsubscribe();
    this._navigateBackSub.unsubscribe();
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
          name: 'slideLeft',
        },
      });
    }, 100);
  }

  goToAddContactPesonPage() {
    setTimeout(() => {
      this.router.navigate(['add-contact-person'], {
        relativeTo: this.active,
        transition: {
          name: 'slideLeft',
        },
      });
    }, 100);
  }

  deleteSelectedContactPerson(contactPersonId: number) {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          setTimeout(() => {
            this.dialogService.alert(
              this.translateService.instant(
                'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.TITLE'
              ),
              this.translateService.instant(
                'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.SUCCESS_MESSAGE'
              ),
              () => {
                this.ngZone.run(() => {
                  this._isBusy = false;
                  this.addContactPersonsService.removeEntity(contactPersonId);
                });
              }
            );
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

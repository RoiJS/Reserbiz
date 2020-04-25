import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

import { Component, OnInit, ViewChild } from '@angular/core';
import { PageRoute, RouterExtensions } from 'nativescript-angular/router';

import { RadListViewComponent } from 'nativescript-ui-listview/angular/listview-directives';

import {
  ListViewEventData,
  SwipeActionsEventData,
} from 'nativescript-ui-listview';
import { View } from 'tns-core-modules/ui/page/page';
import { take, finalize } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';

import { ObservableArray } from 'tns-core-modules/data/observable-array/observable-array';

import { DialogService } from '@src/app/_services/dialog.service';
import { ContactPersonService } from '@src/app/_services/contact-person.service';
import { ContactPerson } from '@src/app/_models/contact-person.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

@Component({
  selector: 'ns-tenant-contact-list',
  templateUrl: './tenant-contact-person-list.component.html',
  styleUrls: ['./tenant-contact-person-list.component.scss'],
})
export class TenantContactPersonListComponent implements OnInit {
  @ViewChild('contactPersonView', { static: false })
  contactPersonView: RadListViewComponent;

  private _contactPersons: ObservableArray<ContactPerson>;
  private _multipleSelectionActive = false;

  private currentTenantId: number;

  private _currentContactPerson: ContactPerson;
  private _isNotNavigateToOtherPage = true;
  private _locationSub: any;
  private _isBusy = false;

  constructor(
    private active: ActivatedRoute,
    private pageRoute: PageRoute,
    private dialogService: DialogService,
    private router: RouterExtensions,
    private contactPersonService: ContactPersonService,
    private location: Location,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this.currentTenantId = +paramMap.get('tenantId');
        this.getContactPersonList();
      });
    });

    // Event listener when navigating back from previous page.
    this._locationSub = this.location.subscribe(() => {
      this._isNotNavigateToOtherPage = true;
      this._contactPersons = new ObservableArray<ContactPerson>([]);
      this.getContactPersonList();
    });
  }

  getContactPersonList() {
    this._isBusy = true;
    setTimeout(() => {
      this.contactPersonService
        .getContactPersons(this.currentTenantId)
        .pipe(
          take(1),
          finalize(() => (this._isBusy = false))
        )
        .subscribe((contactPersons: ContactPerson[]) => {
          this._multipleSelectionActive = false;

          this._contactPersons = new ObservableArray<ContactPerson>(
            contactPersons
          );
        });
    }, 500);
  }

  activateDeactivateMultipleSelection() {
    this._multipleSelectionActive = !this._multipleSelectionActive;

    if (!this._multipleSelectionActive) {
      this._contactPersons.map((contactPerson) => {
        contactPerson.isSelected = false;
      });

      this.contactPersonView.listView.deselectAll();
    }
  }

  onSwipeCellStarted(args: ListViewEventData) {
    this._currentContactPerson = (<SwipeActionsEventData>args).mainView
      .bindingContext as ContactPerson;
    const swipeLimits = args.data.swipeLimits;
    const swipeView = args['object'];
    const tenantSwipeActions = swipeView.getViewById<View>(
      'contactPersonSwipeActions'
    );
    swipeLimits.right = tenantSwipeActions.getMeasuredWidth();
    swipeLimits.threshold = tenantSwipeActions.getMeasuredWidth();
  }

  goToEditDetailsFromMainItem(args: ListViewEventData) {
    const selectedContactPerson = this._contactPersons.getItem(args.index);

    if (!this._multipleSelectionActive) {
      this._isNotNavigateToOtherPage = false;

      this.navigateToEditPage(selectedContactPerson.id);
    } else {
      selectedContactPerson.isSelected = true;
    }
  }

  goToEditDetailsFromOptionItem() {
    this.navigateToEditPage(this._currentContactPerson.id);
  }

  navigateToEditPage(contactPersonId: number) {
    setTimeout(() => {
      this.router.navigate([`${contactPersonId}`], {
        relativeTo: this.active,
        transition: {
          name: 'slideLeft',
        },
      });
    }, 100);
  }

  goToAddContactPesonPage() {
    setTimeout(() => {
      this.router.navigate([`${this.currentTenantId}/add`], {
        relativeTo: this.active,
        transition: {
          name: 'slideLeft',
        },
      });
    }, 100);
  }

  deselectedItem(args: ListViewEventData) {
    const selectedTenant = this._contactPersons.getItem(args.index);
    selectedTenant.isSelected = false;
  }

  deleteSelectedContactPersons() {
    const me = this;
    const selectedContactPersons = this.contactPersonView.listView.getSelectedItems();
    if (selectedContactPersons.length > 0) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSONS_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSONS_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .then((res: ButtonOptions) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            this.contactPersonService
              .deleteMultipleContactPersons(selectedContactPersons)
              .pipe(finalize(() => (this._isBusy = false)))
              .subscribe(
                () => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSONS_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSONS_DIALOG.SUCCESS_MESSAGE'
                    )
                  );

                  me._contactPersons = new ObservableArray<ContactPerson>([]);
                  me.getContactPersonList();
                },
                (error: Error) => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSONS_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSONS_DIALOG.ERROR_MESSAGE'
                    )
                  );
                }
              );
          }
        });
    }
  }

  deleteSelectedContactPerson(event: any) {
    const selectedContactPersonIndex = (<any>(
      this.contactPersonView.listView
    )).getIndexOf(this._currentContactPerson);

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

          this.contactPersonService
            .deleteContactPerson(this._currentContactPerson.id)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.SUCCESS_MESSAGE'
                  ),
                  () => {
                    (<any>this._contactPersons).splice(
                      selectedContactPersonIndex,
                      1
                    );
                  }
                );
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANT_CONTACT_PERSON_LIST_PAGE.REMOVE_CONTACT_PERSON_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }

  get contactPersonList(): ObservableArray<ContactPerson> {
    return this._contactPersons;
  }

  get multipleSelectionActive(): boolean {
    return this._multipleSelectionActive;
  }

  get isNotNavigateToOtherPage(): boolean {
    return this._isNotNavigateToOtherPage;
  }

  get selectedCount(): string {
    return `${
      this.contactPersonView.listView.getSelectedItems().length
    } Selected Contact Person`;
  }

  get IsBusy(): boolean {
    return this._isBusy;
  }
}

import { Location } from '@angular/common';
import { Component, OnInit, ViewChild, NgZone, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { TranslateService } from '@ngx-translate/core';
import { RouterExtensions } from '@nativescript/angular';
import { ObservableArray } from '@nativescript/core';

import { ListViewEventData } from 'nativescript-ui-listview';
import { RadListViewComponent } from 'nativescript-ui-listview/angular';

import { Subscription } from 'rxjs';

import { TermMiscellaneous } from '../../../_models/term-miscellaneous.model';
import { LocalManageTermMiscellaneousService } from '../../../_services/local-manage-term-miscellaneous.service';
import { DialogService } from '../../../_services/dialog.service';
import { ButtonOptions } from '../../../_enum/button-options.enum';

@Component({
  selector: 'ns-contract-manage-term-miscellaneous',
  templateUrl: './contract-manage-term-miscellaneous.component.html',
  styleUrls: ['./contract-manage-term-miscellaneous.component.scss'],
})
export class ContractManageTermMiscellaneousComponent
  implements OnInit, OnDestroy
{
  @ViewChild('termMiscellaneousListView', { static: false })
  termMiscellaneousListView: RadListViewComponent;

  private _isBusy = false;
  private _termMiscellaneousListSub: Subscription;

  private _termMiscellaneous: ObservableArray<TermMiscellaneous>;
  private _isNotNavigateToOtherPage = true;
  private _navigateBackSub: any;

  constructor(
    private active: ActivatedRoute,
    private router: RouterExtensions,
    private localManageTermMiscellaneousService: LocalManageTermMiscellaneousService,
    private dialogService: DialogService,
    private location: Location,
    private ngZone: NgZone,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.getTermMiscellaneousList();

    this._navigateBackSub = this.location.subscribe(() => {
      this._isNotNavigateToOtherPage = true;
    });
  }

  ngOnDestroy() {
    this._termMiscellaneousListSub.unsubscribe();
    this._navigateBackSub.unsubscribe();
  }

  getTermMiscellaneousList() {
    this._isBusy = true;
    setTimeout(() => {
      this._termMiscellaneousListSub =
        this.localManageTermMiscellaneousService.entityList.subscribe(
          (termMiscellaneous: TermMiscellaneous[]) => {
            this._isBusy = false;
            this._termMiscellaneous = new ObservableArray<TermMiscellaneous>(
              termMiscellaneous
            );
            setTimeout(() => {
              this.termMiscellaneousListView.listView.refresh();
            }, 1000);
          }
        );
    }, 500);
  }

  goToEditDetailsFromMainItem(args: ListViewEventData) {
    const selectedContactPerson = this._termMiscellaneous.getItem(args.index);
    this._isNotNavigateToOtherPage = false;
    this.navigateToEditPage(selectedContactPerson.id);
  }

  navigateToEditPage(termMiscellaneousId: number) {
    setTimeout(() => {
      this.router.navigate([`edit-miscellaneous/${termMiscellaneousId}`], {
        relativeTo: this.active,
        transition: {
          name: 'slideLeft',
        },
      });
    }, 100);
  }

  goToAddMiscellaneousPage() {
    setTimeout(() => {
      this.router.navigate(['add-miscellaneous'], {
        relativeTo: this.active,
        transition: {
          name: 'slideLeft',
        },
      });
    }, 100);
  }

  deleteSelectedMiscellaneous(termMiscellaneousId: number) {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'TERM_MISCELLANEOUS_LIST_PAGE.REMOVE_TERM_MISCELLANEOUS_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'TERM_MISCELLANEOUS_LIST_PAGE.REMOVE_TERM_MISCELLANEOUS_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .subscribe((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          setTimeout(() => {
            this.dialogService.alert(
              this.translateService.instant(
                'TERM_MISCELLANEOUS_LIST_PAGE.REMOVE_TERM_MISCELLANEOUS_DIALOG.TITLE'
              ),
              this.translateService.instant(
                'TERM_MISCELLANEOUS_LIST_PAGE.REMOVE_TERM_MISCELLANEOUS_DIALOG.SUCCESS_MESSAGE'
              ),
              () => {
                this.ngZone.run(() => {
                  this._isBusy = false;
                  this.localManageTermMiscellaneousService.removeEntity(
                    termMiscellaneousId
                  );
                });
              }
            );
          }, 1000);
        }
      });
  }

  get termMiscellaneous(): ObservableArray<TermMiscellaneous> {
    return this._termMiscellaneous;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }
}

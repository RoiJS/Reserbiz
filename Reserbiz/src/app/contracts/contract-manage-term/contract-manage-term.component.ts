import { Component, OnInit, OnDestroy } from '@angular/core';
import { RouterExtensions } from '@nativescript/angular';

import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';

import { Term } from '../../_models/term.model';
import { SpaceType } from '../../_models/space-type.model';
import { TermMiscellaneous } from '../../_models/term-miscellaneous.model';

import { ButtonOptions } from '../../_enum/button-options.enum';

import { LocalManageTermService } from '../../_services/local-manage-term.service';
import { LocalManageTermMiscellaneousService } from '../../_services/local-manage-term-miscellaneous.service';
import { DialogService } from '../../_services/dialog.service';
import { SpaceTypeService } from '../../_services/space-type.service';

@Component({
  selector: 'app-contract-manage-term',
  templateUrl: './contract-manage-term.component.html',
  styleUrls: ['./contract-manage-term.component.scss'],
})
export class ContractManageTermComponent implements OnInit, OnDestroy {
  private _actionBarTitle: string[];
  private _currentActionBarTitle: string;
  private _currentTermId: number;
  private _isBusy = false;
  private _termDetails: Term;
  private _termMiscellaneousList: TermMiscellaneous[];

  private _termDetailsSub: Subscription;
  private _termMiscellaneousSub: Subscription;

  constructor(
    private localManageTermService: LocalManageTermService,
    private localManageTermMiscellaneousService: LocalManageTermMiscellaneousService,
    private dialogService: DialogService,
    private translateService: TranslateService,
    private router: RouterExtensions,
    private spaceTypeService: SpaceTypeService
  ) {}

  ngOnInit() {
    this.initializeActionBarTitles();

    this._termDetailsSub = this.localManageTermService.entityDetails.subscribe(
      (term: Term) => {
        this._termDetails = term;
      }
    );

    this._termMiscellaneousSub = this.localManageTermMiscellaneousService.entityList.subscribe(
      (termMiscellaneous: TermMiscellaneous[]) => {
        this._termMiscellaneousList = termMiscellaneous;
      }
    );
  }

  ngOnDestroy() {
    if (this._termDetailsSub) {
      this._termDetailsSub.unsubscribe();
    }

    if (this._termMiscellaneousSub) {
      this._termMiscellaneousSub.unsubscribe();
    }
  }

  tabsIndexChanged(event: any) {
    this._currentActionBarTitle = this._actionBarTitle[event.newIndex];
  }

  initializeActionBarTitles() {
    this._actionBarTitle = [];

    this._actionBarTitle.push(
      this.translateService.instant('TERM_DETAILS_PAGE.ACTION_BAR_TITLE')
    );

    this._actionBarTitle.push(
      this.translateService.instant(
        'TERM_MISCELLANEOUS_LIST_PAGE.ACTION_BAR_TITLE'
      )
    );
  }

  onTermsDetailsSaved(e: { termDetails: Term; currentSpaceType: SpaceType }) {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'TERM_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'TERM_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .subscribe((result: ButtonOptions) => {
        if (result === ButtonOptions.YES) {
          this._isBusy = true;
          setTimeout(() => {
            this._isBusy = false;
            this.localManageTermService.entityDetails.next(e.termDetails);

            if (e.currentSpaceType) {
              this.spaceTypeService.currentSpaceType.next({
                id: e.currentSpaceType.id,
                name: e.currentSpaceType.name,
              });
            }
            this.router.back();
          }, 1000);
        }
      });
  }

  onCancelTermDetailsSaved(updatedTerm: Term) {
    const termDetailsHasNotChanged = this.localManageTermService.isSame(
      updatedTerm
    );
    const termMiscellaneousHaveNotChanged = this.localManageTermMiscellaneousService.isSame(
      this._termMiscellaneousList
    );

    if (!termDetailsHasNotChanged || !termMiscellaneousHaveNotChanged) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'TERM_ADD_DETAILS_PAGE.FORM_CONTROL.LEAVE_PAGE_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'TERM_ADD_DETAILS_PAGE.FORM_CONTROL.LEAVE_PAGE_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .subscribe((result: ButtonOptions) => {
          if (result === ButtonOptions.YES) {
            this.router.back();
          }
        });
    } else {
      this.router.back();
    }
  }

  navigateBack() {
    this.localManageTermService.entityCancelSaveDetails.next();
  }

  saveInformation() {
    this.localManageTermService.entitySavedDetails.next();
  }

  get currentActionBarTitle(): string {
    return this._currentActionBarTitle;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }
}

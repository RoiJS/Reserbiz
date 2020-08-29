import { Component, OnInit, OnDestroy } from '@angular/core';
import { PageRoute, RouterExtensions } from 'nativescript-angular/router';

import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';

import { Term } from '@src/app/_models/term.model';
import { TermService } from '@src/app/_services/term.service';
import { AddTermService } from '@src/app/_services/add-term.service';
import { AddTermMiscellaneousService } from '@src/app/_services/add-term-miscellaneous.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { TermMiscellaneousService } from '@src/app/_services/term-miscellaneous.service';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { TermMiscellaneous } from '@src/app/_models/term-miscellaneous.model';

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
    private addTermService: AddTermService,
    private addTermMiscellaneousService: AddTermMiscellaneousService,
    private dialogService: DialogService,
    private translateService: TranslateService,
    private router: RouterExtensions
  ) {}

  ngOnInit() {
    this.initializeActionBarTitles();

    this._termDetailsSub = this.addTermService.entityDetails.subscribe(
      (term: Term) => {
        this._termDetails = term;
      }
    );

    this._termMiscellaneousSub = this.addTermMiscellaneousService.entityList.subscribe(
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

  onTermsDetailsSaved(termDetails: Term) {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'TERM_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'TERM_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .then((result: ButtonOptions) => {
        if (result === ButtonOptions.YES) {
          this._isBusy = true;
          setTimeout(() => {
            this._isBusy = false;
            this.addTermService.entityDetails.next(termDetails);
            this.router.back();
          }, 1000);
        }
      });
  }

  onCancelTermDetailsSaved(updatedTerm: Term) {
    const termDetailsHasNotChanged = this.addTermService.isSame(updatedTerm);
    const termMiscellaneousHaveNotChanged = this.addTermMiscellaneousService.isSame(
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
        .then((result: ButtonOptions) => {
          if (result === ButtonOptions.YES) {
            this.addTermService.resetEntityDetails();
            this.addTermMiscellaneousService.resetEntityList();
            this.router.back();
          }
        });
    } else {
      this.router.back();
    }
  }

  navigateBack() {
    this.addTermService.entityCancelSaveDetails.next();
  }

  saveInformation() {
    this.addTermService.entitySavedDetails.next();
  }

  get currentActionBarTitle(): string {
    return this._currentActionBarTitle;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }
}

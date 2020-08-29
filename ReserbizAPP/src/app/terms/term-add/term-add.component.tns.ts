import { Component, OnInit, OnDestroy } from '@angular/core';

import { RouterExtensions } from 'nativescript-angular/router';

import { Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';

import { AddTermService } from '@src/app/_services/add-term.service';
import { AddTermMiscellaneousService } from '@src/app/_services/add-term-miscellaneous.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { TermService } from '@src/app/_services/term.service';
import { Term } from '@src/app/_models/term.model';
import { TermMiscellaneous } from '@src/app/_models/term-miscellaneous.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

@Component({
  selector: 'ns-term-add',
  templateUrl: './term-add.component.html',
  styleUrls: ['./term-add.component.scss'],
})
export class TermAddComponent implements OnInit, OnDestroy {
  private _actionBarTitle: string[];
  private _currentActionBarTitle: string;
  private _termDetailsSub: Subscription;
  private _termMiscellaneousSub: Subscription;
  private _termDetails: Term;
  private _termMiscellaneous: TermMiscellaneous[];
  private _isBusy = false;

  constructor(
    private addTermService: AddTermService,
    private addTermMiscellaneousService: AddTermMiscellaneousService,
    private dialogService: DialogService,
    private termService: TermService,
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
        this._termMiscellaneous = termMiscellaneous;
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

  navigateBack() {
    this.addTermService.entityCancelSaveDetails.next();
  }

  saveInformation() {
    this.addTermService.entitySavedDetails.next();
  }

  onTermsDetailsSaved(e: { newTerm: Term; isFormValid: boolean }) {
    // Check if new tenant form is valid
    if (!e.isFormValid) {
      this.dialogService.alert(
        this.translateService.instant(
          'TERM_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'TERM_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.INVALID_FORM'
        )
      );
      return;
    }

    const newTerm = e.newTerm;
    const newTermMiscellaneous = this.addTermMiscellaneousService.entityList
      .value;

    // Save the new term information
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
          this.termService
            .saveNewTerm(newTerm, newTermMiscellaneous)
            .pipe(
              finalize(() => {
                this._isBusy = false;
              })
            )
            .subscribe(
              () => {
                this.addTermService.resetEntityDetails();
                this.addTermMiscellaneousService.resetEntityList();

                this.dialogService.alert(
                  this.translateService.instant(
                    'TERM_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TERM_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.SUCCESS_MESSAGE'
                  ),
                  () => {
                    this.termService.loadTermListFlag.next();
                    this.router.back();
                  }
                );
              },
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TERM_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TERM_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }

  onCancelTermDetailsSaved(e: boolean) {
    if (e) {
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

  get currentActionBarTitle(): string {
    return this._currentActionBarTitle;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }
}

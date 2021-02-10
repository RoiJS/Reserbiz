import { Component, OnInit, ViewChild } from '@angular/core';

import { PageRoute, RouterExtensions } from '@nativescript/angular';
import { TranslateService } from '@ngx-translate/core';

import { RadDataFormComponent } from 'nativescript-ui-dataform/angular';

import { LocalManageTermMiscellaneousService } from '@src/app/_services/local-manage-term-miscellaneous.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { TermMiscellaneous } from '@src/app/_models/term-miscellaneous.model';
import { TermMiscellaneousFormSource } from '@src/app/_models/term-miscellaneous-form.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

@Component({
  selector: 'ns-contract-manage-term-miscellaneous-edit',
  templateUrl: './contract-manage-term-miscellaneous-edit.component.html',
  styleUrls: ['./contract-manage-term-miscellaneous-edit.component.scss'],
})
export class ContractManageTermMiscellaneousEditComponent implements OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  termMiscellanesouForm: RadDataFormComponent;

  private _isBusy = false;
  private _currentTermMiscellaneousId: number;
  private _currentTermMiscellaneous: TermMiscellaneous;
  private _termMiscellaneousFormSource: TermMiscellaneousFormSource;
  private _termMiscellaneousFormSourceOriginal: TermMiscellaneousFormSource;

  constructor(
    private localManageTermMiscellaneous: LocalManageTermMiscellaneousService,
    private dialogService: DialogService,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentTermMiscellaneousId = +paramMap.get('termMiscellaneousId');

        this._currentTermMiscellaneous = this.localManageTermMiscellaneous.getEntity(
          this._currentTermMiscellaneousId
        );

        this._termMiscellaneousFormSource = new TermMiscellaneousFormSource(
          this._currentTermMiscellaneous.name,
          this._currentTermMiscellaneous.description,
          this._currentTermMiscellaneous.amount
        );

        this._termMiscellaneousFormSourceOriginal = this._termMiscellaneousFormSource.clone();
      });
    });
  }

  saveInformation() {
    const isFormInvalid = this.termMiscellanesouForm.dataForm.hasValidationErrors();
    const isFormHasChanged = !this._termMiscellaneousFormSource.isSame(
      this._termMiscellaneousFormSourceOriginal
    );

    if (!isFormInvalid && isFormHasChanged) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'TERM_MISCELLANEOUS_EDIT_PAGE.FORM_CONTROL.EDIT_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'TERM_MISCELLANEOUS_EDIT_PAGE.FORM_CONTROL.EDIT_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .then((res) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            this._currentTermMiscellaneous.name = this._termMiscellaneousFormSource.name;
            this._currentTermMiscellaneous.amount = this._termMiscellaneousFormSource.amount;

            this.localManageTermMiscellaneous.updateEntity(
              this._currentTermMiscellaneous
            );

            this.dialogService.alert(
              this.translateService.instant(
                'TERM_MISCELLANEOUS_EDIT_PAGE.FORM_CONTROL.EDIT_DIALOG.TITLE'
              ),
              this.translateService.instant(
                'TERM_MISCELLANEOUS_EDIT_PAGE.FORM_CONTROL.EDIT_DIALOG.SUCCESS_MESSAGE'
              ),
              () => {
                this.router.back();
              }
            );
          }
        });
    }
  }

  get termMiscellaneousFormSource(): TermMiscellaneousFormSource {
    return this._termMiscellaneousFormSource;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }
}

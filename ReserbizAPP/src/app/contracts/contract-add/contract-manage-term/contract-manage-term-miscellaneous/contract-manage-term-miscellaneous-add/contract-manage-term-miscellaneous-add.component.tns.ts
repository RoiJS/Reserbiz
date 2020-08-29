import { Component, OnInit, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { RadDataFormComponent } from 'nativescript-ui-dataform/angular/dataform-directives';
import { RouterExtensions } from 'nativescript-angular/router';

import { AddTermMiscellaneousService } from '@src/app/_services/add-term-miscellaneous.service';
import { DialogService } from '@src/app/_services/dialog.service';

import { TermMiscellaneousFormSource } from '@src/app/_models/term-miscellaneous-form.model';
import { TermMiscellaneousMapper } from '@src/app/_helpers/term-miscellaneous-mapper.helper';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { TermMiscellaneous } from '@src/app/_models/term-miscellaneous.model';

@Component({
  selector: 'ns-contract-manage-term-miscellaneous-add',
  templateUrl: './contract-manage-term-miscellaneous-add.component.html',
  styleUrls: ['./contract-manage-term-miscellaneous-add.component.scss'],
})
export class ContractManageTermMiscellaneousAddComponent implements OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  termMiscellaneousForm: RadDataFormComponent;

  private _termMiscellaneousFormSource: TermMiscellaneousFormSource;
  private _isBusy = false;

  constructor(
    private addTermMiscellaneousService: AddTermMiscellaneousService,
    private dialogService: DialogService,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this._termMiscellaneousFormSource = new TermMiscellaneousMapper().initFormSource();
  }

  saveInformation() {
    const isFormInvalid = this.termMiscellaneousForm.dataForm.hasValidationErrors();

    if (!isFormInvalid) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'TERM_MISCELLANEOUS_ADD_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'TERM_MISCELLANEOUS_ADD_PAGE.FORM_CONTROL.ADD_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .then((res) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            setTimeout(() => {
              const newMiscellaneous = new TermMiscellaneous();

              newMiscellaneous.name = this._termMiscellaneousFormSource.name;
              newMiscellaneous.description = this._termMiscellaneousFormSource.description;
              newMiscellaneous.amount = this._termMiscellaneousFormSource.amount;

              this.addTermMiscellaneousService.addNewEntity(newMiscellaneous);

              this.dialogService.alert(
                this.translateService.instant(
                  'TERM_MISCELLANEOUS_ADD_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
                ),
                this.translateService.instant(
                  'TERM_MISCELLANEOUS_ADD_PAGE.FORM_CONTROL.ADD_DIALOG.SUCCESS_MESSAGE'
                ),
                () => {
                  this.router.back();
                }
              );
            }, 1000);
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

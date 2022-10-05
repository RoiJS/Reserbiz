import { Component, OnInit, ViewChild } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";

import { RadDataFormComponent } from "nativescript-ui-dataform/angular";
import { RouterExtensions } from "@nativescript/angular";

import { LocalManageTermMiscellaneousService } from "~/app/_services/local-manage-term-miscellaneous.service";
import { DialogService } from "~/app/_services/dialog.service";

import { TermMiscellaneousFormSource } from "~/app/_models/form/term-miscellaneous-form.model";
import { TermMiscellaneousMapper } from "~/app/_helpers/mappers/term-miscellaneous-mapper.helper";
import { TermMiscellaneous } from "~/app/_models/term-miscellaneous.model";

@Component({
  selector: "ns-contract-manage-term-miscellaneous-add",
  templateUrl: "./contract-manage-term-miscellaneous-add.component.html",
  styleUrls: ["./contract-manage-term-miscellaneous-add.component.scss"],
})
export class ContractManageTermMiscellaneousAddComponent implements OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  termMiscellaneousForm: RadDataFormComponent;

  private _termMiscellaneousFormSource: TermMiscellaneousFormSource;
  private _isBusy = false;

  constructor(
    private localManageTermMiscellaneousService: LocalManageTermMiscellaneousService,
    private dialogService: DialogService,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this._termMiscellaneousFormSource =
      new TermMiscellaneousMapper().initFormSource();
  }

  saveInformation() {
    const isFormInvalid =
      this.termMiscellaneousForm.dataForm.hasValidationErrors();

    if (!isFormInvalid) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            "TERM_MISCELLANEOUS_ADD_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE"
          ),
          this.translateService.instant(
            "TERM_MISCELLANEOUS_ADD_PAGE.FORM_CONTROL.ADD_DIALOG.CONFIRM_MESSAGE"
          )
        )
        .then((res: boolean) => {
          if (res) {
            this._isBusy = true;

            setTimeout(() => {
              const newMiscellaneous = new TermMiscellaneous();

              newMiscellaneous.name = this._termMiscellaneousFormSource.name;
              newMiscellaneous.description =
                this._termMiscellaneousFormSource.description;
              newMiscellaneous.amount =
                this._termMiscellaneousFormSource.amount;

              this.localManageTermMiscellaneousService.addNewEntity(
                newMiscellaneous
              );

              this.dialogService
                .alert(
                  this.translateService.instant(
                    "TERM_MISCELLANEOUS_ADD_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE"
                  ),
                  this.translateService.instant(
                    "TERM_MISCELLANEOUS_ADD_PAGE.FORM_CONTROL.ADD_DIALOG.SUCCESS_MESSAGE"
                  )
                )
                .then(() => {
                  this.router.back();
                });
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

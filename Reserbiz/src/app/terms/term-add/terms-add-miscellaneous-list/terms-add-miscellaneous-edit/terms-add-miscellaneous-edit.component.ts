import { Component, OnInit, ViewChild } from "@angular/core";

import { PageRoute, RouterExtensions } from "@nativescript/angular";
import { TranslateService } from "@ngx-translate/core";

import { RadDataFormComponent } from "nativescript-ui-dataform/angular";

import { LocalManageTermMiscellaneousService } from "~/app/_services/local-manage-term-miscellaneous.service";
import { DialogService } from "~/app/_services/dialog.service";

import { TermMiscellaneous } from "~/app/_models/term-miscellaneous.model";
import { TermMiscellaneousFormSource } from "~/app/_models/form/term-miscellaneous-form.model";

@Component({
  selector: "ns-terms-add-miscellaneous-edit",
  templateUrl: "./terms-add-miscellaneous-edit.component.html",
  styleUrls: ["./terms-add-miscellaneous-edit.component.scss"],
})
export class TermsAddMiscellaneousEditComponent implements OnInit {
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
        this._currentTermMiscellaneousId = +paramMap.get("termMiscellaneousId");

        this._currentTermMiscellaneous =
          this.localManageTermMiscellaneous.getEntity(
            this._currentTermMiscellaneousId
          );

        this._termMiscellaneousFormSource = new TermMiscellaneousFormSource(
          this._currentTermMiscellaneous.name,
          this._currentTermMiscellaneous.description,
          this._currentTermMiscellaneous.amount
        );

        this._termMiscellaneousFormSourceOriginal =
          this._termMiscellaneousFormSource.clone();
      });
    });
  }

  saveInformation() {
    const isFormInvalid =
      this.termMiscellanesouForm.dataForm.hasValidationErrors();
    const isFormHasChanged = !this._termMiscellaneousFormSource.isSame(
      this._termMiscellaneousFormSourceOriginal
    );

    if (!isFormInvalid && isFormHasChanged) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            "TERM_MISCELLANEOUS_EDIT_PAGE.FORM_CONTROL.EDIT_DIALOG.TITLE"
          ),
          this.translateService.instant(
            "TERM_MISCELLANEOUS_EDIT_PAGE.FORM_CONTROL.EDIT_DIALOG.CONFIRM_MESSAGE"
          )
        )
        .then((res: boolean) => {
          if (res) {
            this._isBusy = true;

            this._currentTermMiscellaneous.name =
              this._termMiscellaneousFormSource.name;
            this._currentTermMiscellaneous.amount =
              this._termMiscellaneousFormSource.amount;

            this.localManageTermMiscellaneous.updateEntity(
              this._currentTermMiscellaneous
            );

            this.dialogService
              .alert(
                this.translateService.instant(
                  "TERM_MISCELLANEOUS_EDIT_PAGE.FORM_CONTROL.EDIT_DIALOG.TITLE"
                ),
                this.translateService.instant(
                  "TERM_MISCELLANEOUS_EDIT_PAGE.FORM_CONTROL.EDIT_DIALOG.SUCCESS_MESSAGE"
                )
              )
              .then(() => {
                this.router.back();
              });
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

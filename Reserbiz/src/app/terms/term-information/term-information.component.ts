import { Component, OnInit, OnDestroy } from "@angular/core";
import { PageRoute, RouterExtensions } from "@nativescript/angular";
import { Page } from "@nativescript/core";

import { Subscription } from "rxjs";
import { finalize } from "rxjs/operators";

import { TranslateService } from "@ngx-translate/core";

import { Term } from "~/app/_models/term.model";

import { DialogService } from "~/app/_services/dialog.service";
import { TermService } from "~/app/_services/term.service";

import { DurationValueProvider } from "~/app/_helpers/value_providers/duration-value-provider.helper";

import { MiscellaneousDueDateEnum } from "~/app/_enum/miscellaneous-due-date.enum";

@Component({
  selector: "ns-term-information",
  templateUrl: "./term-information.component.html",
  styleUrls: ["./term-information.component.scss"],
})
export class TermInformationComponent implements OnInit, OnDestroy {
  private _currentTerm: Term;
  private _currentTermId: number;
  private _isBusy = false;

  private _updateTermListFlag: Subscription;
  private _durationValueProvider: DurationValueProvider;

  constructor(
    private dialogService: DialogService,
    private page: Page,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private translateService: TranslateService,
    private termService: TermService
  ) {
    this._durationValueProvider = new DurationValueProvider(
      this.translateService
    );
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentTermId = +paramMap.get("termId");

        this._updateTermListFlag = this.termService.loadTermListFlag.subscribe(
          () => {
            this.getTermInformation();
          }
        );
      });
    });
  }

  ngOnDestroy() {
    this._updateTermListFlag.unsubscribe();
  }

  getTermInformation() {
    this._isBusy = true;

    (async () => {
      this._currentTerm = await this.termService.getTerm(this._currentTermId);
      this._isBusy = false;

      this._currentTerm.advancedPaymentDurationText =
        this._durationValueProvider.getDurationName(
          this._currentTerm.advancedPaymentDurationValue,
          this._currentTerm.durationUnitText
        );

      this._currentTerm.depositPaymentDurationText =
        this._durationValueProvider.getDurationName(
          this._currentTerm.depositPaymentDurationValue,
          this._currentTerm.durationUnitText
        );

      this._currentTerm.penaltyEffectiveAfterDurationUnitText =
        this._durationValueProvider.getDurationName(
          this._currentTerm.penaltyEffectiveAfterDurationValue,
          this._currentTerm.penaltyEffectiveAfterDurationUnitText
        );

      let penaltyAmountText = this.translateService.instant(
        "TERM_DETAILS_PAGE.PENALTY_DETAILS_GROUP.PENALTY_AMOUNT_PER_DURATION_LABEL"
      );
      penaltyAmountText = penaltyAmountText
        .replace("{0}", this._currentTerm.penaltyAmountPerDurationUnitText)
        .replace(
          "{1}",
          this.translateService.instant("GENERAL_TEXTS.CURRENCY.PHP")
        )
        .replace("{2}", this._currentTerm.penaltyAmount);

      let penaltyEffectiveText = this.translateService.instant(
        "TERM_DETAILS_PAGE.PENALTY_DETAILS_GROUP.PENALTY_EFFECTIVE_AFTER_LABEL"
      );
      penaltyEffectiveText = penaltyEffectiveText
        .replace("{0}", this._currentTerm.penaltyEffectiveAfterDurationValue)
        .replace(
          "{1}",
          this._currentTerm.penaltyEffectiveAfterDurationUnitText
        );

      let miscellaneousDueDateText = this.translateService.instant(
        "TERM_DETAILS_PAGE.MISCELLANEOUS_SETTINGS_DETAILS_GROUP.DUE_DATE"
      );
      miscellaneousDueDateText = miscellaneousDueDateText.replace(
        "{0}",
        this._currentTerm.miscellaneousDueDate ===
          MiscellaneousDueDateEnum.SameWithRentalDueDate
          ? this.translateService.instant(
              "GENERAL_TEXTS.MISCELLANEOUS_DUE_DATE_OPTIONS.SAME_WITH_RENTAL_DUE_DATE"
            )
          : this.translateService.instant(
              "GENERAL_TEXTS.MISCELLANEOUS_DUE_DATE_OPTIONS.SAME_WITH_UTILITY_BILL_DUE_DATE"
            )
      );

      this._currentTerm.penaltyAmountText = penaltyAmountText;
      this._currentTerm.penaltyEffectiveText = penaltyEffectiveText;
      this._currentTerm.miscellaneousDueDateText = miscellaneousDueDateText;
    })();
  }

  deleteSelectedTenant() {
    this.dialogService
      .confirm(
        this.translateService.instant(
          "TERM_DETAILS_PAGE.REMOVE_TERM_DIALOG.TITLE"
        ),
        this.translateService.instant(
          "TERM_DETAILS_PAGE.REMOVE_TERM_DIALOG.CONFIRM_MESSAGE"
        )
      )
      .then((res: boolean) => {
        if (res) {
          this._isBusy = true;

          this.termService
            .deleteItem(this._currentTermId)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe({
              next: () => {
                this.dialogService
                  .alert(
                    this.translateService.instant(
                      "TERM_DETAILS_PAGE.REMOVE_TERM_DIALOG.TITLE"
                    ),
                    this.translateService.instant(
                      "TERM_DETAILS_PAGE.REMOVE_TERM_DIALOG.SUCCESS_MESSAGE"
                    )
                  )
                  .then(() => {
                    this.termService.loadTermListFlag.next();
                    this.router.back();
                  });
              },

              error: (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    "TERM_DETAILS_PAGE.REMOVE_TERM_DIALOG.TITLE"
                  ),
                  this.translateService.instant(
                    "TERM_DETAILS_PAGE.REMOVE_TERM_DIALOG.ERROR_MESSAGE"
                  )
                );
              },
            });
        }
      });
  }

  get currentTerm(): Term {
    return this._currentTerm;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }
}

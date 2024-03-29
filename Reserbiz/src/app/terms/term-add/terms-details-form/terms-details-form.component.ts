import {
  Component,
  OnInit,
  ViewChild,
  Output,
  EventEmitter,
  OnDestroy,
} from "@angular/core";
import { RadDataFormComponent } from "nativescript-ui-dataform/angular";
import { DataFormEventData } from "nativescript-ui-dataform";
import { TranslateService } from "@ngx-translate/core";

import { Subscription } from "rxjs";

import { Term } from "~/app/_models/term.model";
import { TermDetailsFormSource } from "~/app/_models/form/term-details-form.model";
import { SpaceTypeOption } from "~/app/_models/options/space-type-option.model";
import { ITermFormValueProvider } from "~/app/_interfaces/value_providers/iterm-form-value-provider.interface";

import { DurationEnum } from "~/app/_enum/duration-unit.enum";
import { MiscellaneousDueDateEnum } from "~/app/_enum/miscellaneous-due-date.enum";
import { ValueTypeEnum } from "~/app/_enum/value-type.enum";
import { YesNoEnum } from "~/app/_enum/yesno-unit.enum";

import { LocalManageTermService } from "~/app/_services/local-manage-term.service";
import { LocalManageTermMiscellaneousService } from "~/app/_services/local-manage-term-miscellaneous.service";
import { SpaceTypeService } from "~/app/_services/space-type.service";
import { TermService } from "~/app/_services/term.service";

import { TermMapper } from "~/app/_helpers/mappers/term-mapper.helper";
import { DurationValueProvider } from "~/app/_helpers/value_providers/duration-value-provider.helper";
import { ValueTypeValueProvider } from "~/app/_helpers/value_providers/value-type-provider.helper";
import { SpaceTypeValueProvider } from "~/app/_helpers/value_providers/space-type-value-provider.helper";
import { DurationRangeValueProvider } from "~/app/_helpers/value_providers/duration-range-value-provider.helper";
import { MiscellaneousValueProvider } from "~/app/_helpers/value_providers/miscellaneous-due-date-provider.helper";
import { YesNoValueProvider } from "~/app/_helpers/value_providers/yesno-value-provider.helper";

import { BaseFormHelper } from "~/app/_helpers/base_helpers/base-form.helper";

@Component({
  selector: "ns-terms-details-form",
  templateUrl: "./terms-details-form.component.html",
  styleUrls: ["./terms-details-form.component.scss"],
})
export class TermsDetailsFormComponent
  extends BaseFormHelper<TermDetailsFormSource>
  implements ITermFormValueProvider, OnInit, OnDestroy
{
  @ViewChild(RadDataFormComponent, { static: false })
  termForm: RadDataFormComponent;

  @Output() onTermDetailsSaved = new EventEmitter<{
    newTerm: Term;
    isFormValid: boolean;
  }>();
  @Output() onCancelTermDetailsSaved = new EventEmitter<boolean>();

  private _newTermDetails: Term;
  private _termDetailsForm: TermDetailsFormSource;
  private _termSavedDetailsSub: Subscription;
  private _cancelTermSavedDetailsSub: Subscription;

  private _durationValueProvider: DurationValueProvider;
  private _durationDetailsRangeValueProvider: DurationRangeValueProvider;
  private _penaltyEffectiveAfterDurationUnitRangeValueProvider: DurationRangeValueProvider;
  private _valueTypeValueProvider: ValueTypeValueProvider;
  private _spaceTypeValueProvider: SpaceTypeValueProvider;
  private _miscellaneousDueDateValueProvider: MiscellaneousValueProvider;
  private _yesNoValueProvider: YesNoValueProvider;

  private _IsBusy = false;

  constructor(
    private localManageTermService: LocalManageTermService,
    private localManageTermMiscellaneous: LocalManageTermMiscellaneousService,
    private spaceTypeService: SpaceTypeService,
    private termService: TermService,
    private translateService: TranslateService
  ) {
    super();
  }

  ngOnInit() {
    this.initTermDetails();
    this.initTermForm();

    this._durationValueProvider = new DurationValueProvider(
      this.translateService
    );

    this._valueTypeValueProvider = new ValueTypeValueProvider(
      this.translateService
    );

    this._yesNoValueProvider = new YesNoValueProvider(this.translateService);

    this._spaceTypeValueProvider = new SpaceTypeValueProvider(
      this.translateService,
      this.spaceTypeService
    );

    this._miscellaneousDueDateValueProvider = new MiscellaneousValueProvider(
      this.translateService
    );

    this._durationDetailsRangeValueProvider = new DurationRangeValueProvider(
      this._termDetailsForm.durationUnit
    );

    this._penaltyEffectiveAfterDurationUnitRangeValueProvider =
      new DurationRangeValueProvider(
        this._termDetailsForm.penaltyEffectiveAfterDurationUnit
      );

    this._termSavedDetailsSub =
      this.localManageTermService.entitySavedDetails.subscribe(() => {
        this.onTermDetailsSavedEmit();
      });

    this._cancelTermSavedDetailsSub =
      this.localManageTermService.entityCancelSaveDetails.subscribe(() => {
        this.onCancelTermDetailsSavedEmit();
      });
  }

  ngOnDestroy() {
    if (this._termSavedDetailsSub) {
      this._termSavedDetailsSub.unsubscribe();
    }

    if (this._cancelTermSavedDetailsSub) {
      this._cancelTermSavedDetailsSub.unsubscribe();
    }
  }

  initTermDetails() {
    this._newTermDetails = new Term();
    return this._newTermDetails;
  }

  initTermForm() {
    this._termDetailsForm = new TermMapper().initFormSource();
  }

  onTermDetailsSavedEmit() {
    if (this.termForm) {
      this.mapFormSourceToEntity();
      (async () => {
        const isFormValid = await this.validateForm();
        this.onTermDetailsSaved.emit({
          newTerm: this._newTermDetails,
          isFormValid,
        });
      })();
    }
  }

  async validateForm(): Promise<boolean> {
    let isCodeValid, isNameValid, isSpaceTypeValid;
    isCodeValid = isNameValid = isSpaceTypeValid = true;

    const dataForm = this.termForm.dataForm;
    const codeProperty = dataForm.getPropertyByName("code");
    const nameProperty = dataForm.getPropertyByName("name");
    const spaceTypeIdProperty = dataForm.getPropertyByName("spaceTypeId");

    // Check and validate code field
    if (this._newTermDetails.code.trim() === "") {
      codeProperty.errorMessage = this.translateService.instant(
        "TERM_ADD_DETAILS_PAGE.FORM_CONTROL.GENERAL_CONTROL_GROUP.CODE_CONTROL.EMPTY_ERROR_MESSAGE"
      );
      isCodeValid = false;
    } else {
      if (this._newTermDetails.code.length > 10) {
        codeProperty.errorMessage = this.translateService.instant(
          "TERM_ADD_DETAILS_PAGE.FORM_CONTROL.GENERAL_CONTROL_GROUP.CODE_CONTROL.MAXLENGTH_ERROR_MESSAGE"
        );
        isCodeValid = false;
      } else {
        // Check if code already exists
        const checkCodeResult = await this.termService.checkTermCodeIfExists(
          0,
          this._newTermDetails.code
        );

        if (checkCodeResult) {
          codeProperty.errorMessage = this.translateService.instant(
            "TERM_ADD_DETAILS_PAGE.FORM_CONTROL.GENERAL_CONTROL_GROUP.CODE_CONTROL.ALREADY_EXIST_ERROR_MESSAGE"
          );
        }
        isCodeValid = !checkCodeResult;
      }
    }

    // Check and validate name field
    if (this._newTermDetails.name.trim() === "") {
      nameProperty.errorMessage = this.translateService.instant(
        "TERM_ADD_DETAILS_PAGE.FORM_CONTROL.GENERAL_CONTROL_GROUP.NAME_CONTROL.EMPTY_ERROR_MESSAGE"
      );
      isNameValid = false;
    } else {
      if (this._newTermDetails.name.length > 100) {
        nameProperty.errorMessage = this.translateService.instant(
          "TERM_ADD_DETAILS_PAGE.FORM_CONTROL.GENERAL_CONTROL_GROUP.NAME_CONTROL.MAXLENGTH_ERROR_MESSAGE"
        );
        isNameValid = false;
      } else {
        isNameValid = true;
      }
    }

    // Check and validate space type field
    if (this._newTermDetails.spaceTypeId === 0) {
      spaceTypeIdProperty.errorMessage = this.translateService.instant(
        "TERM_ADD_DETAILS_PAGE.FORM_CONTROL.GENERAL_CONTROL_GROUP.SPACE_TYPE_CONTROL.EMPTY_ERROR_MESSAGE"
      );
      isSpaceTypeValid = false;
    } else {
      isSpaceTypeValid = true;
    }

    dataForm.notifyValidated("code", isCodeValid);
    dataForm.notifyValidated("name", isNameValid);
    dataForm.notifyValidated("spaceTypeId", isSpaceTypeValid);

    return Boolean(isCodeValid && isNameValid && isSpaceTypeValid);
  }

  onCancelTermDetailsSavedEmit() {
    if (this.termForm) {
      this.mapFormSourceToEntity();

      const hasContent = Boolean(
        this._newTermDetails.hasContent() ||
          this.localManageTermMiscellaneous.entityList.value.length > 0
      );

      this.onCancelTermDetailsSaved.emit(hasContent);
    }
  }

  onPropertyCommitted(args: DataFormEventData) {
    const dataForm = args.object;

    if (args.propertyName === "spaceTypeId") {
      /**
       * When user select a space type,
       * we will give the rate field a value
       * based on the rate of the selected space type as the default value
       * but the user still have the option to update the value
       */
      const rateProperty = dataForm.getPropertyByName("rate");
      const spaceType = this._spaceTypeValueProvider.getItem(
        this._termDetailsForm.spaceTypeId
      );

      // Check if the selected space type is active
      // If not, we will set the dropdown to the default option
      // and set the rate to 0
      if (spaceType.canBeSelected) {
        this._termDetailsForm = this.reloadFormSource(this._termDetailsForm, {
          rate: spaceType.rate,
        });
      } else {
        this._termDetailsForm = this.reloadFormSource(this._termDetailsForm, {
          rate: 0,
          spaceTypeId: 0,
        });
      }
    }

    if (args.propertyName === "durationUnit") {
      this._durationDetailsRangeValueProvider.currentDuration =
        this._termDetailsForm.durationUnit;

      /**
       * Whenever the duration unit is set to "None",
       * we will manually set the advanced and deposit to 0
       */
      if (this._termDetailsForm.durationUnit === DurationEnum.None) {
        this._termDetailsForm = this.reloadFormSource(this._termDetailsForm, {
          advancedPaymentDurationValue: 0,
          depositPaymentDurationValue: 0,
        });
      }
    }

    /**
     * Electric bill amount will be set to 0
     * if exclude electric bill is set to true
     */
    if (args.propertyName === "excludeElectricBill") {
      if (
        !this._termDetailsForm.excludeElectricBill &&
        this._termDetailsForm.electricBillAmount > 0
      ) {
        this._termDetailsForm = this.reloadFormSource(this._termDetailsForm, {
          electricBillAmount: 0,
        });
      }
    }

    /**
     * Water bill amount will be set to 0
     * if exclude water bill is set to true
     */
    if (args.propertyName === "excludeWaterBill") {
      if (
        !this._termDetailsForm.excludeWaterBill &&
        this._termDetailsForm.waterBillAmount > 0
      ) {
        this._termDetailsForm = this.reloadFormSource(this._termDetailsForm, {
          waterBillAmount: 0,
        });
      }
    }

    if (args.propertyName === "penaltyAmountPerDurationUnit") {
      /**
       * Whenever user select a duration unit for penalty amount per duration,
       * we will set the same duration unit for the penalty effective after duration value
       * but still, the user the option to change it with other duration unit.
       */
      this._termDetailsForm = this.reloadFormSource(this._termDetailsForm, {
        penaltyEffectiveAfterDurationUnit:
          this._termDetailsForm.penaltyAmountPerDurationUnit,
      });
    }

    if (args.propertyName === "penaltyEffectiveAfterDurationUnit") {
      /**
       * This is for the duration range value provider to recalculate
       * the minimum and maximum value that can be set
       * depending on the currently selected duration unit for penalty
       * effective after duration field
       */
      this._penaltyEffectiveAfterDurationUnitRangeValueProvider.currentDuration =
        this._termDetailsForm.penaltyEffectiveAfterDurationUnit;
    }

    if (args.propertyName === "penaltyEffectiveAfterDurationValue") {
      /**
       * The purpose of this is to validate if the input value on penalty
       * effective after duration value does not exceed the allowed
       * maximum value of the currently selected duration unit for penalty effective
       * after duration unit, If so, we will set the value based on the maximum allowed value.
       */
      if (
        this._penaltyEffectiveAfterDurationUnitRangeValueProvider.checkIfExceed(
          this._termDetailsForm.penaltyEffectiveAfterDurationValue
        )
      ) {
        this._termDetailsForm = this.reloadFormSource(this._termDetailsForm, {
          penaltyEffectiveAfterDurationValue:
            this._penaltyEffectiveAfterDurationUnitRangeValueProvider.maximum,
        });
      }
    }

    if (args.propertyName === "miscellaneousDueDate") {
      /**
       * If the Miscellaneous Due Date is not same with rental fee
       * then deactivate setting includeMiscellaneousCheckAndCalculateForPenalty.
       */
      if (
        this._termDetailsForm.miscellaneousDueDate !==
        MiscellaneousDueDateEnum.SameWithRentalDueDate
      ) {
        this._termDetailsForm = this.reloadFormSource(this._termDetailsForm, {
          includeMiscellaneousCheckAndCalculateForPenalty: false,
        });
      }
    }
  }

  mapFormSourceToEntity() {
    this._newTermDetails.code = this._termDetailsForm.code;
    this._newTermDetails.name = this._termDetailsForm.name;
    this._newTermDetails.spaceTypeId = this._termDetailsForm.spaceTypeId;
    this._newTermDetails.rate = this._termDetailsForm.rate;
    this._newTermDetails.maximumNumberOfOccupants =
      this._termDetailsForm.maximumNumberOfOccupants;
    this._newTermDetails.durationUnit = this._termDetailsForm.durationUnit;
    this._newTermDetails.advancedPaymentDurationValue =
      this._termDetailsForm.advancedPaymentDurationValue;
    this._newTermDetails.depositPaymentDurationValue =
      this._termDetailsForm.depositPaymentDurationValue;
    this._newTermDetails.excludeElectricBill =
      this._termDetailsForm.excludeElectricBill === YesNoEnum.Yes;
    this._newTermDetails.electricBillAmount =
      this._termDetailsForm.electricBillAmount;
    this._newTermDetails.excludeWaterBill =
      this._termDetailsForm.excludeWaterBill === YesNoEnum.Yes;
    this._newTermDetails.waterBillAmount =
      this._termDetailsForm.waterBillAmount;
    this._newTermDetails.penaltyValue = this._termDetailsForm.penaltyValue;
    this._newTermDetails.penaltyValueType =
      this._termDetailsForm.penaltyValueType;
    this._newTermDetails.penaltyAmountPerDurationUnit =
      this._termDetailsForm.penaltyAmountPerDurationUnit;
    this._newTermDetails.penaltyEffectiveAfterDurationValue =
      this._termDetailsForm.penaltyEffectiveAfterDurationValue;
    this._newTermDetails.penaltyEffectiveAfterDurationUnit =
      this._termDetailsForm.penaltyEffectiveAfterDurationUnit;
    this._newTermDetails.generateAccountStatementDaysBeforeValue =
      this._termDetailsForm.generateAccountStatementDaysBeforeValue;
    this._newTermDetails.autoSendNewAccountStatement =
      this._termDetailsForm.autoSendNewAccountStatement === YesNoEnum.Yes;
    this._newTermDetails.miscellaneousDueDate =
      this._termDetailsForm.miscellaneousDueDate;
    this._newTermDetails.includeMiscellaneousCheckAndCalculateForPenalty =
      this._termDetailsForm.includeMiscellaneousCheckAndCalculateForPenalty ===
      YesNoEnum.Yes;
  }

  get durationOptions(): Array<{ key: DurationEnum; label: string }> {
    return this._durationValueProvider.durationOptions;
  }

  get miscellaneousDueDateOptions(): Array<{
    key: MiscellaneousDueDateEnum;
    label: string;
  }> {
    return this._miscellaneousDueDateValueProvider.dueDateOptions;
  }

  get valueTypeOptions(): Array<{ key: ValueTypeEnum; label: string }> {
    return this._valueTypeValueProvider.valueTypeOptions;
  }

  get spaceTypeOptions(): {
    key: string;
    label: string;
    items: SpaceTypeOption[];
  } {
    return this._spaceTypeValueProvider.spaceTypeOptions;
  }

  get yesNoOptions(): { key: YesNoEnum; label: string }[] {
    return this._yesNoValueProvider.yesNoOptions;
  }

  get termDetailsForm(): TermDetailsFormSource {
    return this._termDetailsForm;
  }

  get durationDetailsRangeValueProvider(): DurationRangeValueProvider {
    return this._durationDetailsRangeValueProvider;
  }

  get IsBusy(): boolean {
    return this._IsBusy;
  }
}

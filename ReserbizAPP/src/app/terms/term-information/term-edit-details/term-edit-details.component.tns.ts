import { Component, OnInit, NgZone, ViewChild } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';
import { PageRoute, RouterExtensions } from 'nativescript-angular/router';
import { RadDataFormComponent } from 'nativescript-ui-dataform/angular';

import { DataFormEventData } from 'nativescript-ui-dataform';

import { finalize } from 'rxjs/operators';

import { BaseFormComponent } from '@src/app/shared/component/base-form.component';

import { DialogService } from '@src/app/_services/dialog.service';
import { SpaceTypeService } from '@src/app/_services/space-type.service';
import { TermService } from '@src/app/_services/term.service';

import { Term } from '@src/app/_models/term.model';
import { TermDetailsFormSource } from '@src/app/_models/term-details-form.model';
import { SpaceTypeOption } from '@src/app/_models/space-type-option.model';

import { TermDto } from '@src/app/_dtos/term.dto';

import { DurationValueProvider } from '@src/app/_helpers/duration-value-provider.helper';
import { DurationRangeValueProvider } from '@src/app/_helpers/duration-range-value-provider.helper';
import { SpaceTypeValueProvider } from '@src/app/_helpers/space-type-value-provider.helper';
import { TermMapper } from '@src/app/_helpers/term-mapper.helper';
import { ValueTypeValueProvider } from '@src/app/_helpers/value-type-provide.helper';

import { IBaseFormComponent } from '@src/app/_interfaces/ibase-form.component.interface';
import { ITermFormValueProvider } from '@src/app/_interfaces/iterm-form-value-provider.interface';

import { DurationEnum } from '@src/app/_enum/duration-unit.enum';
import { ValueTypeEnum } from '@src/app/_enum/value-type.enum';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

@Component({
  selector: 'ns-term-edit-details',
  templateUrl: './term-edit-details.component.html',
  styleUrls: ['./term-edit-details.component.scss'],
})
export class TermEditDetailsComponent
  extends BaseFormComponent<Term, TermDetailsFormSource, TermDto>
  implements IBaseFormComponent, ITermFormValueProvider, OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  termForm: RadDataFormComponent;

  private _durationValueProvider: DurationValueProvider;
  private _durationDetailsRangeValueProvider: DurationRangeValueProvider;
  private _penaltyEffectiveAfterDurationUnitRangeValueProvider: DurationRangeValueProvider;
  private _valueTypeValueProvider: ValueTypeValueProvider;
  private _spaceTypeValueProvider: SpaceTypeValueProvider;
  private _spaceTypeOptions;

  constructor(
    public pageRoute: PageRoute,
    public dialogService: DialogService,
    public ngZone: NgZone,
    public translateService: TranslateService,
    public router: RouterExtensions,
    private termService: TermService,
    private spaceTypeService: SpaceTypeService
  ) {
    super(dialogService, ngZone, router, translateService);
    this._entityService = termService;
    this._entityDtoMapper = new TermMapper();

    this._spaceTypeValueProvider = new SpaceTypeValueProvider(
      this.translateService,
      this.spaceTypeService
    );

    this._spaceTypeOptions = this._spaceTypeValueProvider.spaceTypeOptions;

    this._durationValueProvider = new DurationValueProvider(
      this.translateService
    );

    this._valueTypeValueProvider = new ValueTypeValueProvider(
      this.translateService
    );
  }

  ngOnInit() {
    this.initDialogTexts();

    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        const me = this;
        this._currentFormEntityId = +paramMap.get('termId');

        (async function () {
          me._currentEntity = await me.termService.getTerm(
            me._currentFormEntityId
          );

          me._entityFormSource = me._entityDtoMapper.mapEntityToFormSource(
            me._currentEntity
          );

          me._entityFormSourceOriginal = me._entityFormSource.clone();

          me._spaceTypeValueProvider.setCurrenValue(
            me._currentEntity.spaceTypeId
          );

          me._spaceTypeOptions = me._spaceTypeValueProvider.spaceTypeOptions;

          me._durationDetailsRangeValueProvider = new DurationRangeValueProvider(
            me._entityFormSource.durationUnit
          );

          me._penaltyEffectiveAfterDurationUnitRangeValueProvider = new DurationRangeValueProvider(
            me._entityFormSource.penaltyEffectiveAfterDurationUnit
          );
        })();
      });
    });
  }

  onPropertyCommitted(args: DataFormEventData) {
    const dataForm = args.object;

    if (args.propertyName === 'spaceTypeId') {
      /**
       * When user select a space type,
       * we will give the rate field a value
       * based on the rate of the selected space type as the default value
       * but the user still have the option to update the value
       */
      const rateProperty = dataForm.getPropertyByName('rate');
      const spaceType = this._spaceTypeValueProvider.getItem(
        this._entityFormSource.spaceTypeId
      );

      // Check if the selected space type is active
      // If not, we will set the dropdown to the default option
      // and set the rate to 0
      if (spaceType.canBeSelected) {
        this._entityFormSource = this.reloadFormSource(this._entityFormSource, {
          rate: spaceType.rate,
        });
      } else {
        this._entityFormSource = this.reloadFormSource(this._entityFormSource, {
          rate: 0,
          spaceTypeId: 0,
        });
      }
    }

    if (args.propertyName === 'durationUnit') {
      this._durationDetailsRangeValueProvider.currentDuration = this._entityFormSource.durationUnit;

      /**
       * Whenever the duration unit is set to "None",
       * we will manually set the advanced and deposit to 0
       */
      if (this._entityFormSource.durationUnit === DurationEnum.None) {
        this._entityFormSource = this.reloadFormSource(this._entityFormSource, {
          advancedPaymentDurationValue: 0,
          depositPaymentDurationValue: 0,
        });
      }
    }

    /**
     * Electric bill amount will be set to 0
     * if exclude electric bill is set to true
     */
    if (args.propertyName === 'excludeElectricBill') {
      if (
        !this._entityFormSource.excludeElectricBill &&
        this._entityFormSource.electricBillAmount > 0
      ) {
        this._entityFormSource = this.reloadFormSource(this._entityFormSource, {
          electricBillAmount: 0,
        });
      }
    }

    /**
     * Water bill amount will be set to 0
     * if exclude water bill is set to true
     */
    if (args.propertyName === 'excludeWaterBill') {
      if (
        !this._entityFormSource.excludeWaterBill &&
        this._entityFormSource.waterBillAmount > 0
      ) {
        this._entityFormSource = this.reloadFormSource(this._entityFormSource, {
          waterBillAmount: 0,
        });
      }
    }

    if (args.propertyName === 'penaltyAmountPerDurationUnit') {
      /**
       * Whenever user select a duration unit for penalty amount per duration,
       * we will set the same duration unit for the penalty effective after duration value
       * but still, the user the option to change it with other duration unit.
       */
      this._entityFormSource = this.reloadFormSource(this._entityFormSource, {
        penaltyEffectiveAfterDurationUnit: this._entityFormSource
          .penaltyAmountPerDurationUnit,
      });
    }

    if (args.propertyName === 'penaltyEffectiveAfterDurationUnit') {
      /**
       * This is for the duration range value provider to recalculate
       * the minimum and maximum value that can be set
       * depending on the currently selected duration unit for penalty
       * effective after duration field
       */
      this._penaltyEffectiveAfterDurationUnitRangeValueProvider.currentDuration = this._entityFormSource.penaltyEffectiveAfterDurationUnit;
    }

    if (args.propertyName === 'penaltyEffectiveAfterDurationValue') {
      /**
       * The purpose of this is to validate if the input value on penalty
       * effective after duration value does not exceed the allowed
       * maximum value of the currently selected duration unit for penalty effective
       * after duration unit, If so, we will set the value based on the maximum allowed value.
       */
      if (
        this._penaltyEffectiveAfterDurationUnitRangeValueProvider.checkIfExceed(
          this._entityFormSource.penaltyEffectiveAfterDurationValue
        )
      ) {
        this._entityFormSource = this.reloadFormSource(this._entityFormSource, {
          penaltyEffectiveAfterDurationValue: this
            ._penaltyEffectiveAfterDurationUnitRangeValueProvider.maximum,
        });
      }
    }
  }

  async validateForm(): Promise<boolean> {
    let isCodeValid, isNameValid, isSpaceTypeValid;
    isCodeValid = isNameValid = isSpaceTypeValid = true;

    // Check if nothing has been modified
    if (this._entityFormSource.isSame(this._entityFormSourceOriginal)) {
      return false;
    }

    const dataForm = this.termForm.dataForm;
    const codeProperty = dataForm.getPropertyByName('code');
    const nameProperty = dataForm.getPropertyByName('name');
    const spaceTypeIdProperty = dataForm.getPropertyByName('spaceTypeId');

    // Check and validate code field
    if (this._currentEntity.code.trim() === '') {
      codeProperty.errorMessage = this.translateService.instant(
        'TERM_EDIT_DETAILS_PAGE.FORM_CONTROL.GENERAL_CONTROL_GROUP.CODE_CONTROL.EMPTY_ERROR_MESSAGE'
      );
      isCodeValid = false;
    } else {
      if (this._currentEntity.code.length > 10) {
        codeProperty.errorMessage = this.translateService.instant(
          'TERM_EDIT_DETAILS_PAGE.FORM_CONTROL.GENERAL_CONTROL_GROUP.CODE_CONTROL.MAXLENGTH_ERROR_MESSAGE'
        );
        isCodeValid = false;
      } else {
        // Check if code already exists
        const checkCodeResult = await this.termService.checkTermCodeIfExists(
          this._currentFormEntityId,
          this._currentEntity.code
        );

        if (checkCodeResult) {
          codeProperty.errorMessage = this.translateService.instant(
            'TERM_EDIT_DETAILS_PAGE.FORM_CONTROL.GENERAL_CONTROL_GROUP.CODE_CONTROL.ALREADY_EXIST_ERROR_MESSAGE'
          );
        }
        isCodeValid = !checkCodeResult;
      }
    }

    // Check and validate name field
    if (this._currentEntity.name.trim() === '') {
      nameProperty.errorMessage = this.translateService.instant(
        'TERM_EDIT_DETAILS_PAGE.FORM_CONTROL.GENERAL_CONTROL_GROUP.NAME_CONTROL.EMPTY_ERROR_MESSAGE'
      );
      isNameValid = false;
    } else {
      if (this._currentEntity.name.length > 100) {
        nameProperty.errorMessage = this.translateService.instant(
          'TERM_EDIT_DETAILS_PAGE.FORM_CONTROL.GENERAL_CONTROL_GROUP.NAME_CONTROL.MAXLENGTH_ERROR_MESSAGE'
        );
        isNameValid = false;
      } else {
        isNameValid = true;
      }
    }

    // Check and validate space type field
    if (this._currentEntity.spaceTypeId === 0) {
      spaceTypeIdProperty.errorMessage = this.translateService.instant(
        'TERM_EDIT_DETAILS_PAGE.FORM_CONTROL.GENERAL_CONTROL_GROUP.SPACE_TYPE_CONTROL.EMPTY_ERROR_MESSAGE'
      );
      isSpaceTypeValid = false;
    } else {
      isSpaceTypeValid = true;
    }

    dataForm.notifyValidated('code', isCodeValid);
    dataForm.notifyValidated('name', isNameValid);
    dataForm.notifyValidated('spaceTypeId', isSpaceTypeValid);

    return !!(isCodeValid && isNameValid && isSpaceTypeValid);
  }

  updateInformation() {
    (async () => {
      this._currentEntity = this._entityDtoMapper.mapFormSourceToEntity(
        this._entityFormSource
      );

      const isFormValid = await this.validateForm();

      // Check if form is valid
      if (!isFormValid) {
        return;
      }

      this.dialogService
        .confirm(
          this._updateDialogTexts.title,
          this._updateDialogTexts.confirmMessage
        )
        .then((res) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            const dtoToUpdate = this._entityDtoMapper.mapFormSourceToDto(
              this._entityFormSource
            );

            this._entityService
              .updateEntity({
                id: this._currentFormEntityId,
                dtoEntity: dtoToUpdate,
              })
              .pipe(
                finalize(() => {
                  this.ngZone.run(() => {
                    this._isBusy = false;
                  });
                })
              )
              .subscribe(
                () => {
                  this.dialogService.alert(
                    this._updateDialogTexts.title,
                    this._updateDialogTexts.successMessage,
                    () => {
                      this._entityService.reloadListFlag();
                      this.router.back();
                    }
                  );
                },
                (error: Error) => {
                  this.dialogService.alert(
                    this._updateDialogTexts.title,
                    this._updateDialogTexts.errorMessage
                  );
                }
              );
          }
        });
    })();
  }

  navigateBack() {
    if (this.termForm) {
      // When use attempts to leave the current page,
      // check if there is unsaved changes
      if (!this._entityFormSource.isSame(this._entityFormSourceOriginal)) {
        this.dialogService
          .confirm(
            this.translateService.instant(
              'TERM_EDIT_DETAILS_PAGE.FORM_CONTROL.LEAVE_PAGE_DIALOG.TITLE'
            ),
            this.translateService.instant(
              'TERM_EDIT_DETAILS_PAGE.FORM_CONTROL.LEAVE_PAGE_DIALOG.CONFIRM_MESSAGE'
            )
          )
          .then((result: ButtonOptions) => {
            if (result === ButtonOptions.YES) {
              this.router.back();
            }
          });
      } else {
        this.router.back();
      }
    }
  }

  initDialogTexts() {
    this._updateDialogTexts = {
      title: this.translateService.instant(
        'TERM_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TERM_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TERM_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TERM_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.ERROR_MESSAGE'
      ),
    };
  }

  get durationOptions(): Array<{ key: DurationEnum; label: string }> {
    return this._durationValueProvider.durationOptions;
  }

  get valueTypeOptions(): Array<{ key: ValueTypeEnum; label: string }> {
    return this._valueTypeValueProvider.valueTypeOptions;
  }

  get spaceTypeOptions(): {
    key: string;
    label: string;
    items: SpaceTypeOption[];
  } {
    return this._spaceTypeOptions;
  }

  get durationDetailsRangeValueProvider(): DurationRangeValueProvider {
    return this._durationDetailsRangeValueProvider;
  }
}

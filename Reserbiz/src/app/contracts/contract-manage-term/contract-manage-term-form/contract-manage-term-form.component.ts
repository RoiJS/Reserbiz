import {
  Component,
  OnInit,
  NgZone,
  Output,
  EventEmitter,
  OnDestroy,
} from '@angular/core';

import { DataFormEventData } from 'nativescript-ui-dataform';
import { RouterExtensions } from '@nativescript/angular';
import { TranslateService } from '@ngx-translate/core';

import { Subscription } from 'rxjs';

import { LocalManageTermService } from '../../../_services/local-manage-term.service';
import { BaseFormComponent } from '../../../shared/component/base-form.component';

import { Term } from '../../../_models/term.model';
import { SpaceTypeOption } from '../../../_models/options/space-type-option.model';
import { TermDetailsFormSource } from '../../../_models/form/term-details-form.model';
import { TermDto } from '../../../_dtos/term.dto';
import { SpaceType } from '../../../_models/space-type.model';
import { DialogService } from '../../../_services/dialog.service';
import { SpaceTypeService } from '../../../_services/space-type.service';

import { ITermFormValueProvider } from '../../../_interfaces/value_providers/iterm-form-value-provider.interface';
import { DurationValueProvider } from '../../../_helpers/value_providers/duration-value-provider.helper';

import { DurationRangeValueProvider } from '../../../_helpers/value_providers/duration-range-value-provider.helper';
import { ValueTypeValueProvider } from '../../../_helpers/value_providers/value-type-provider.helper';
import { SpaceTypeValueProvider } from '../../../_helpers/value_providers/space-type-value-provider.helper';
import { MiscellaneousValueProvider } from '../../../_helpers/value_providers/miscellaneous-due-date-provider.helper';

import { TermMapper } from '../../../_helpers/mappers/term-mapper.helper';

import { DurationEnum } from '../../../_enum/duration-unit.enum';
import { MiscellaneousDueDateEnum } from '../../../_enum/miscellaneous-due-date.enum';
import { ValueTypeEnum } from '../../../_enum/value-type.enum';

@Component({
  selector: 'ns-contract-manage-term-form',
  templateUrl: './contract-manage-term-form.component.html',
  styleUrls: ['./contract-manage-term-form.component.scss'],
})
export class ContractManageTermFormComponent
  extends BaseFormComponent<Term, TermDetailsFormSource, TermDto>
  implements ITermFormValueProvider, OnInit, OnDestroy
{
  @Output() onTermDetailsSaved = new EventEmitter<{
    termDetails: Term;
    currentSpaceType: SpaceType;
  }>();
  @Output() onCancelTermDetailsSaved = new EventEmitter<Term>();

  private _termSavedDetailsSub: Subscription;
  private _cancelTermSavedDetailsSub: Subscription;

  private _durationValueProvider: DurationValueProvider;
  private _durationDetailsRangeValueProvider: DurationRangeValueProvider;
  private _penaltyEffectiveAfterDurationUnitRangeValueProvider: DurationRangeValueProvider;
  private _valueTypeValueProvider: ValueTypeValueProvider;
  private _spaceTypeValueProvider: SpaceTypeValueProvider;
  private _miscellaneousDueDateValueProvider: MiscellaneousValueProvider;

  private _spaceTypeOptions;

  private _termDetailsSub: Subscription;
  private _currentSpaceType: SpaceType;

  constructor(
    public dialogService: DialogService,
    public ngZone: NgZone,
    public translateService: TranslateService,
    public router: RouterExtensions,
    private localManageTermService: LocalManageTermService,
    private spaceTypeService: SpaceTypeService
  ) {
    super(dialogService, ngZone, router, translateService);
    this._entityDtoMapper = new TermMapper();

    this._spaceTypeValueProvider = new SpaceTypeValueProvider(
      this.translateService,
      this.spaceTypeService
    );

    this._spaceTypeOptions = this._spaceTypeValueProvider.spaceTypeOptions;

    this._durationValueProvider = new DurationValueProvider(
      this.translateService
    );

    this._miscellaneousDueDateValueProvider = new MiscellaneousValueProvider(
      this.translateService
    );

    this._valueTypeValueProvider = new ValueTypeValueProvider(
      this.translateService
    );
  }

  ngOnInit() {
    this._termSavedDetailsSub =
      this.localManageTermService.entitySavedDetails.subscribe(() => {
        this.onTermDetailsSavedEmit();
      });

    this._cancelTermSavedDetailsSub =
      this.localManageTermService.entityCancelSaveDetails.subscribe(() => {
        this.onCancelTermDetailsSavedEmit();
      });

    this._termDetailsSub = this.localManageTermService.entityDetails.subscribe(
      (term: Term) => {
        const me = this;
        setTimeout(function () {
          me._currentEntity = term;

          me._entityFormSource = me._entityDtoMapper.mapEntityToFormSource(
            me._currentEntity
          );

          me._entityFormSourceOriginal = me._entityFormSource.clone();

          me._spaceTypeValueProvider.setCurrenValue(
            me._currentEntity.spaceTypeId
          );

          me._spaceTypeOptions = me._spaceTypeValueProvider.spaceTypeOptions;

          me._durationDetailsRangeValueProvider =
            new DurationRangeValueProvider(me._entityFormSource.durationUnit);

          me._penaltyEffectiveAfterDurationUnitRangeValueProvider =
            new DurationRangeValueProvider(
              me._entityFormSource.penaltyEffectiveAfterDurationUnit
            );
        }, 500);
      }
    );
  }

  ngOnDestroy() {
    if (this._termDetailsSub) {
      this._termDetailsSub.unsubscribe();
    }

    if (this._termSavedDetailsSub) {
      this._termSavedDetailsSub.unsubscribe();
    }

    if (this._cancelTermSavedDetailsSub) {
      this._cancelTermSavedDetailsSub.unsubscribe();
    }
  }

  onTermDetailsSavedEmit() {
    if (this.formSource) {
      const termId = this._currentEntity.id;
      this._currentEntity = this._entityDtoMapper.mapFormSourceToEntity(
        this._entityFormSource
      );
      this._currentEntity.id = termId;
      this.onTermDetailsSaved.emit({
        termDetails: this._currentEntity,
        currentSpaceType: this._currentSpaceType,
      });
    }
  }

  onCancelTermDetailsSavedEmit() {
    if (this.formSource) {
      this._currentEntity = this._entityDtoMapper.mapFormSourceToEntity(
        this._entityFormSource
      );

      this.onCancelTermDetailsSaved.emit(this._currentEntity);
    }
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
      const spaceType = this._spaceTypeValueProvider.getItem(
        this._entityFormSource.spaceTypeId
      );

      this._currentSpaceType = new SpaceType();
      this._currentSpaceType.id = spaceType.id;
      this._currentSpaceType.name = spaceType.name;

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
      this._durationDetailsRangeValueProvider.currentDuration =
        this._entityFormSource.durationUnit;

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
        penaltyEffectiveAfterDurationUnit:
          this._entityFormSource.penaltyAmountPerDurationUnit,
      });
    }

    if (args.propertyName === 'penaltyEffectiveAfterDurationUnit') {
      /**
       * This is for the duration range value provider to recalculate
       * the minimum and maximum value that can be set
       * depending on the currently selected duration unit for penalty
       * effective after duration field
       */
      this._penaltyEffectiveAfterDurationUnitRangeValueProvider.currentDuration =
        this._entityFormSource.penaltyEffectiveAfterDurationUnit;
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
          penaltyEffectiveAfterDurationValue:
            this._penaltyEffectiveAfterDurationUnitRangeValueProvider.maximum,
        });
      }
    }

    if (args.propertyName === 'miscellaneousDueDate') {
      /**
       * If the Miscellaneous Due Date is not same with rental fee
       * then deactivate setting includeMiscellaneousCheckAndCalculateForPenalty.
       */
      if (
        this._entityFormSource.miscellaneousDueDate !==
        MiscellaneousDueDateEnum.SameWithRentalDueDate
      ) {
        this._entityFormSource = this.reloadFormSource(this._entityFormSource, {
          includeMiscellaneousCheckAndCalculateForPenalty: false,
        });
      }
    }
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
    return this._spaceTypeOptions;
  }

  get durationDetailsRangeValueProvider(): DurationRangeValueProvider {
    return this._durationDetailsRangeValueProvider;
  }
}

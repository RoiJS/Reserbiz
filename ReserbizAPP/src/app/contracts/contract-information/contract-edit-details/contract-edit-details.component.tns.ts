import { Component, NgZone, OnDestroy, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { Subscription } from 'rxjs';
import { skip } from 'rxjs/operators';

import { PageRoute, RouterExtensions } from '@nativescript/angular';
import { ExtendedNavigationExtras } from '@nativescript/angular/router/router-extensions';
import { DataFormEventData } from 'nativescript-ui-dataform';

import { BaseFormComponent } from '@src/app/shared/component/base-form.component';

import { ContractDto } from '@src/app/_dtos/contract-dto';
import { DurationEnum } from '@src/app/_enum/duration-unit.enum';

import { ContractMapper } from '@src/app/_helpers/mappers/contract-mapper.helper';
import { DateFormatter } from '@src/app/_helpers/formatters/date-formatter.helper';
import { DurationValueProvider } from '@src/app/_helpers/value_providers/duration-value-provider.helper';
import { TenantValueProvider } from '@src/app/_helpers/value_providers/tenant-value-provider.helper';
import { TermValueProvider } from '@src/app/_helpers/value_providers/term-value-provider.helper';
import { SpaceValueProvider } from '@src/app/_helpers/value_providers/space-value-provider.helper';

import { IBaseFormComponent } from '@src/app/_interfaces/components/ibase-form.component.interface';
import { IContractFormValueProvider } from '@src/app/_interfaces/value_providers/icontract-form-value-provider.interface';

import { ContractDetailsFormSource } from '@src/app/_models/form/contract-details-form.model';
import { Contract } from '@src/app/_models/contract.model';
import { TenantOption } from '@src/app/_models/options/tenant-option.model';
import { TermOption } from '@src/app/_models/options/term-option.model';
import { SpaceOption } from '@src/app/_models/options/space-option.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

import { ContractService } from '@src/app/_services/contract.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { TermService } from '@src/app/_services/term.service';
import { TenantService } from '@src/app/_services/tenant.service';
import { SpaceTypeService } from '@src/app/_services/space-type.service';
import { LocalManageTermService } from '@src/app/_services/local-manage-term.service';
import { LocalManageTermMiscellaneousService } from '@src/app/_services/local-manage-term-miscellaneous.service';
import { TermMiscellaneousService } from '@src/app/_services/term-miscellaneous.service';
import { SpaceService } from '@src/app/_services/space.service';

@Component({
  selector: 'app-contract-edit-details',
  templateUrl: './contract-edit-details.component.html',
  styleUrls: ['./contract-edit-details.component.scss'],
})
export class ContractEditDetailsComponent
  extends BaseFormComponent<Contract, ContractDetailsFormSource, ContractDto>
  implements IBaseFormComponent, IContractFormValueProvider, OnInit, OnDestroy
{
  private _durationValueProvider: DurationValueProvider;
  private _termValueProvider: TermValueProvider;
  private _tenantValueProvider: TenantValueProvider;
  private _spaceValueProvider: SpaceValueProvider;

  private _hasAccountStatements = false;

  private _termOptions: any;
  private _tenantOptions: any;
  private _spaceOptions: any;

  private currentSpaceTypeSub: Subscription;

  constructor(
    public dialogService: DialogService,
    public ngZone: NgZone,
    public router: RouterExtensions,
    public translateService: TranslateService,
    private contractService: ContractService,
    private termService: TermService,
    private tenantService: TenantService,
    private spaceTypeService: SpaceTypeService,
    private spaceService: SpaceService,
    private pageRoute: PageRoute,
    private localManageTermService: LocalManageTermService,
    private localManageTermMiscellaneousService: LocalManageTermMiscellaneousService,
    private termMiscellaneousService: TermMiscellaneousService
  ) {
    super(dialogService, ngZone, router, translateService);
    this._entityService = contractService;
    this._entityDtoMapper = new ContractMapper();

    this._durationValueProvider = new DurationValueProvider(
      this.translateService
    );

    this._termValueProvider = new TermValueProvider(
      this.translateService,
      this.termService
    );

    this._tenantValueProvider = new TenantValueProvider(
      this.translateService,
      this.tenantService
    );

    this._spaceValueProvider = new SpaceValueProvider(
      this.translateService,
      this.spaceService
    );

    this._termOptions = this._termValueProvider.termOptions;
    this._tenantOptions = this._tenantValueProvider.tenantOptions;
    this._spaceOptions = this._spaceValueProvider.spaceOptions;
  }

  ngOnInit() {
    this.initDialogTexts();

    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        const me = this;
        this._currentFormEntityId = +paramMap.get('contractId');

        (async function () {
          me._currentEntity = await me.contractService.getContract(
            me._currentFormEntityId
          );

          me._entityFormSource = me._entityDtoMapper.mapEntityToFormSource(
            me._currentEntity
          );

          me._hasAccountStatements = me._currentEntity.hasAccountStatements;

          me._entityFormSourceOriginal = me._entityFormSource.clone();

          me._termValueProvider.setCurrenValue(me._currentEntity.termParentId);
          me._tenantValueProvider.setCurrenValue(me._currentEntity.tenantId);
          me._spaceValueProvider.setCurrenValue(me._currentEntity.spaceId);
          me._spaceValueProvider.setCurrentSpaceTypeId(
            me._currentEntity.spaceTypeId
          );

          me._termOptions = me._termValueProvider.termOptions;
          me._tenantOptions = me._tenantValueProvider.tenantOptions;
          me._spaceOptions = me._spaceValueProvider.spaceOptions;

          me._durationValueProvider = new DurationValueProvider(
            me.translateService
          );

          // Subscribes to the latest current space type.
          me.currentSpaceTypeSub = me.spaceTypeService.currentSpaceType
            .pipe(skip(1))
            .subscribe((spaceType: { id: number; name: string }) => {
              me._spaceValueProvider.setCurrentSpaceTypeId(spaceType.id);
              me._spaceOptions = me._spaceValueProvider.spaceOptions;

              me._entityFormSource = me.reloadFormSource(me._entityFormSource, {
                spaceId: 0,
                spaceTypeName: spaceType.name,
              });
            });

          me.preloadTermDetails(me._currentEntity.termId);
        })();
      });
    });
  }

  ngOnDestroy() {
    if (this.currentSpaceTypeSub) {
      this.currentSpaceTypeSub.unsubscribe();
    }
  }

  initDialogTexts() {
    this._updateDialogTexts = {
      title: this.translateService.instant(
        'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.ERROR_MESSAGE'
      ),
    };
  }

  onEditorUpdate(args: DataFormEventData) {
    if (args.propertyName === 'effectiveDate') {
      if (typeof args.editor.setDateFormat !== 'undefined') {
        this.changeDateFormatting(args.editor);
      }
    }
  }

  onPropertyCommitted(args: DataFormEventData) {
    /**
     * If Open contract, reset value for duration unit and duration value
     */
    if (args.propertyName === 'isOpenContract') {
      if (
        this._entityFormSource.isOpenContract &&
        (this._entityFormSource.durationUnit > DurationEnum.None ||
          this._entityFormSource.durationValue > 0)
      ) {
        this._entityFormSource = this.reloadFormSource(this._entityFormSource, {
          durationUnit: DurationEnum.None,
          durationValue: 0,
        });
      }
    }

    if (args.propertyName === 'durationUnit') {
      /**
       * Whenever the duration unit is set to "None",
       * we will manually set the duration value to 0
       */
      if (this._entityFormSource.durationUnit === DurationEnum.None) {
        this._entityFormSource = this.reloadFormSource(this._entityFormSource, {
          durationValue: 0,
        });
      }
    }

    if (args.propertyName === 'termId') {
      this.preloadTermDetails(this._entityFormSource.termId);
    }
  }

  async validateForm(): Promise<boolean> {
    let isCodeValid: boolean,
      isTenantValid: boolean,
      isTermValid: boolean,
      isSpaceTypeNameValid: boolean,
      isSpaceValid: boolean,
      isDurationUnitValid: boolean,
      isDurationValueValid: boolean;
    isCodeValid =
      isTenantValid =
      isTermValid =
      isSpaceTypeNameValid =
      isSpaceValid =
      isDurationUnitValid =
      isDurationValueValid =
        true;

    const dataForm = this.formSource.dataForm;
    const codeProperty = dataForm.getPropertyByName('code');
    const tenandIdProperty = dataForm.getPropertyByName('tenantId');
    const termIdProperty = dataForm.getPropertyByName('termId');
    const spaceIdProperty = dataForm.getPropertyByName('spaceId');
    const spaceTypeNameProperty = dataForm.getPropertyByName('spaceTypeName');
    const durationUnitProperty = dataForm.getPropertyByName('durationUnit');
    const durationValueProperty = dataForm.getPropertyByName('durationValue');

    // Check and validate code field
    if (this._entityFormSource.code.trim() === '') {
      codeProperty.errorMessage = this.translateService.instant(
        'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.GENERAL_INFORMATION_CONTROL_GROUP.CODE_CONTROL.EMPTY_ERROR_MESSAGE'
      );
      isCodeValid = false;
    } else {
      if (this._entityFormSource.code.length > 10) {
        codeProperty.errorMessage = this.translateService.instant(
          'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.CODE_CONTROL.MAXLENGTH_ERROR_MESSAGE'
        );
        isCodeValid = false;
      } else {
        // Check if code already exists
        const checkCodeResult =
          await this.contractService.checkContractCodeIfExists(
            this._currentEntity.id,
            this._entityFormSource.code
          );

        if (checkCodeResult) {
          codeProperty.errorMessage = this.translateService.instant(
            'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.CODE_CONTROL.ALREADY_EXIST_ERROR_MESSAGE'
          );
        }
        isCodeValid = !checkCodeResult;
      }
    }

    // Check and validate tenant id field
    if (this._entityFormSource.tenantId === 0) {
      tenandIdProperty.errorMessage = this.translateService.instant(
        'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.TENANT_CONTROL.EMPTY_ERROR_MESSAGE'
      );
      isTenantValid = false;
    } else {
      isTenantValid = true;
    }

    // Check and validate term id field
    if (this._entityFormSource.termId === 0) {
      termIdProperty.errorMessage = this.translateService.instant(
        'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.TERM_CONTROL.EMPTY_ERROR_MESSAGE'
      );
      isTermValid = false;
    } else {
      isTermValid = true;

      const hasSpaces = this._spaceValueProvider.spaceOptions;
      const hasAvailableSpaces =
        this._spaceValueProvider.spaceOptions.items.find(
          (s) =>
            s.isNotOccupied || s.occupiedByContractId === this._currentEntity.id
        );

      if (!hasSpaces) {
        spaceTypeNameProperty.errorMessage = this.translateService.instant(
          'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.TERM_CONTROL.NO_ASSIGNED_SPACES'
        );
        isSpaceTypeNameValid = false;
      } else {
        if (!hasAvailableSpaces) {
          spaceTypeNameProperty.errorMessage = this.translateService.instant(
            'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.TERM_CONTROL.NO_AVAILABLE_SPACES'
          );
          isSpaceTypeNameValid = false;
        } else {
          isSpaceTypeNameValid = true;
        }
      }
    }

    // Check and validate space id field
    if (this._entityFormSource.spaceId === 0) {
      spaceIdProperty.errorMessage = this.translateService.instant(
        'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.SPACE_CONTROL.EMPTY_ERROR_MESSAGE'
      );
      isSpaceValid = false;
    } else {
      const space = this._spaceValueProvider.getItem(
        this._entityFormSource.spaceId
      );

      // Check if the current selected space is
      // not occupied by other contract
      if (
        !space.isNotOccupied &&
        space.occupiedByContractId !== this._currentEntity.id &&
        space.occupiedByContractId !== 0
      ) {
        spaceIdProperty.errorMessage = this.translateService.instant(
          'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.SPACE_CONTROL.NOT_AVAILABLE_ERROR_MESSAGE'
        );
        isSpaceValid = false;
      } else {
        isSpaceValid = true;
      }
    }

    if (!this._entityFormSource.isOpenContract) {
      if (this._entityFormSource.durationUnit === DurationEnum.None) {
        durationUnitProperty.errorMessage = this.translateService.instant(
          'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.DURATION_UNIT_CONTROL.EMPTY_ERROR_MESSAGE'
        );
        isDurationUnitValid = false;
      } else {
        isDurationUnitValid = true;
      }

      if (this._entityFormSource.durationValue === 0) {
        durationValueProperty.errorMessage = this.translateService.instant(
          'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.DURATION_VALUE_CONTROL.EMPTY_ERROR_MESSAGE'
        );
        isDurationValueValid = false;
      } else {
        isDurationValueValid = true;
      }

      // Needs to validate if the proposed new expiration date is valid.
      // A valid expiration date must not be less than the contract's
      // next due date.
      const validateNewExpirationDate =
        await this.contractService.validateExpirationDate(
          this.currentEntity.id,
          DateFormatter.format(this._entityFormSource.effectiveDate),
          this._entityFormSource.durationUnit,
          this._entityFormSource.durationValue
        );

      if (!validateNewExpirationDate) {
        durationUnitProperty.errorMessage = this.translateService.instant(
          'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.DURATION_UNIT_CONTROL.INVALID_ERROR_MESSAGE'
        );

        durationValueProperty.errorMessage = this.translateService.instant(
          'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.DURATION_VALUE_CONTROL.INVALID_ERROR_MESSAGE'
        );

        this.dialogService.alert(
          this.translateService.instant(
            'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.INVALID_EXPIRATION_DATE_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.INVALID_EXPIRATION_DATE_DIALOG.MESSAGE'
          )
        );

        isDurationUnitValid = false;
        isDurationValueValid = false;
      } else {
        isDurationUnitValid = true;
        isDurationValueValid = true;
      }
    } else {
      isDurationUnitValid = true;
      isDurationValueValid = true;
    }

    dataForm.notifyValidated('code', isCodeValid);
    dataForm.notifyValidated('tenantId', isTenantValid);
    dataForm.notifyValidated('termId', isTermValid);
    dataForm.notifyValidated('spaceTypeName', isSpaceTypeNameValid);
    dataForm.notifyValidated('spaceId', isSpaceValid);
    dataForm.notifyValidated('durationUnit', isDurationUnitValid);
    dataForm.notifyValidated('durationValue', isDurationValueValid);

    return Boolean(
      isCodeValid &&
        isTenantValid &&
        isTermValid &&
        isSpaceTypeNameValid &&
        isSpaceValid &&
        isDurationUnitValid &&
        isDurationValueValid
    );
  }

  navigateToManageTerm() {
    if (!this._currentEntity.termId) {
      this.dialogService.alert(
        this.translateService.instant(
          'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.NO_SELECTED_TERM_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.NO_SELECTED_TERM_DIALOG.WARNING_MESSAGE'
        )
      );

      return;
    }

    setTimeout(() => {
      const routeConfig: ExtendedNavigationExtras = {
        transition: {
          name: 'slideLeft',
        },
      };

      this.router.navigate([`contracts/manage-term`], routeConfig);
    }, 100);
  }

  updateInformation() {
    (async () => {
      const isFormValid = await this.validateForm();

      if (isFormValid) {
        this.dialogService
          .confirm(
            this._updateDialogTexts.title,
            this._updateDialogTexts.confirmMessage
          )
          .then((res) => {
            if (res === ButtonOptions.YES) {
              this._isBusy = true;

              (async () => {
                const contractDetails =
                  this._entityDtoMapper.mapFormSourceToEntity(
                    this._entityFormSource
                  );
                contractDetails.id = this._currentEntity.id;

                const termDetails =
                  this.localManageTermService.entityDetails.value;
                const termMiscellaneousList =
                  this.localManageTermMiscellaneousService.entityList.value;

                const originalTermMiscellaneous =
                  await this.termMiscellaneousService.getTermMiscellaneousList(
                    termDetails.id
                  );
                try {
                  await this.contractService.manageContract(
                    contractDetails,
                    termDetails,
                    termMiscellaneousList,
                    originalTermMiscellaneous
                  );

                  this.dialogService.alert(
                    this._updateDialogTexts.title,
                    this._updateDialogTexts.successMessage,
                    () => {
                      this.localManageTermService.resetEntityDetails();
                      this.localManageTermMiscellaneousService.resetEntityList();

                      this._entityService.reloadListFlag();
                      this.router.back();
                    }
                  );
                } catch {
                  this.dialogService.alert(
                    this._updateDialogTexts.title,
                    this._updateDialogTexts.errorMessage
                  );
                } finally {
                  this._isBusy = false;
                }
              })();
            }
          });
      }
    })();
  }

  preloadTermDetails(termId: number) {
    (async () => {
      this.localManageTermService.resetEntityDetails();
      this.localManageTermMiscellaneousService.resetEntityList();

      const termDetails = await this.termService.getTerm(termId);

      const termMiscellaneousList =
        await this.termMiscellaneousService.getTermMiscellaneousList(termId);

      this.localManageTermService.entityDetails.next(termDetails);
      this.localManageTermMiscellaneousService.entityList.next(
        termMiscellaneousList
      );
    })();
  }

  get durationOptions(): Array<{ key: DurationEnum; label: string }> {
    return this._durationValueProvider.durationOptions;
  }

  get termOptions(): { key: string; label: string; items: TermOption[] } {
    return this._termValueProvider.termOptions;
  }

  get tenantOptions(): { key: string; label: string; items: TenantOption[] } {
    return this._tenantValueProvider.tenantOptions;
  }

  get spaceOptions(): { key: string; label: string; items: SpaceOption[] } {
    return this._spaceValueProvider.spaceOptions;
  }

  get hasAccountStatements(): boolean {
    return this._hasAccountStatements;
  }
}

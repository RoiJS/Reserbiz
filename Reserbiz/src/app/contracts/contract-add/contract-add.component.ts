import { Component, OnInit, NgZone, OnDestroy } from "@angular/core";
import { RouterExtensions } from "@nativescript/angular";
import { ExtendedNavigationExtras } from "@nativescript/angular/lib/legacy/router/router-extensions";

import { TranslateService } from "@ngx-translate/core";
import { Subscription } from "rxjs";

import { DataFormEventData } from "nativescript-ui-dataform";
import { BaseFormComponent } from "~/app/shared/component/base-form.component";
import { ContractDetailsFormSource } from "~/app/_models/form/contract-details-form.model";
import { Contract } from "~/app/_models/contract.model";

import { ContractDto } from "~/app/_dtos/contract-dto";

import { LocalManageTermService } from "~/app/_services/local-manage-term.service";
import { LocalManageTermMiscellaneousService } from "~/app/_services/local-manage-term-miscellaneous.service";
import { ContractService } from "~/app/_services/contract.service";
import { DialogService } from "~/app/_services/dialog.service";
import { SpaceTypeService } from "~/app/_services/space-type.service";
import { TermService } from "~/app/_services/term.service";
import { TermMiscellaneousService } from "~/app/_services/term-miscellaneous.service";
import { TenantService } from "~/app/_services/tenant.service";
import { SpaceService } from "~/app/_services/space.service";

import { IBaseFormComponent } from "~/app/_interfaces/components/ibase-form.component.interface";
import { IContractFormValueProvider } from "~/app/_interfaces/value_providers/icontract-form-value-provider.interface";

import { TermOption } from "~/app/_models/options/term-option.model";
import { TenantOption } from "~/app/_models/options/tenant-option.model";
import { SpaceOption } from "~/app/_models/options/space-option.model";
import { DurationEnum } from "~/app/_enum/duration-unit.enum";
import { YesNoEnum } from "~/app/_enum/yesno-unit.enum";

import { ContractMapper } from "~/app/_helpers/mappers/contract-mapper.helper";
import { DurationValueProvider } from "~/app/_helpers/value_providers/duration-value-provider.helper";
import { TermValueProvider } from "~/app/_helpers/value_providers/term-value-provider.helper";
import { TenantValueProvider } from "~/app/_helpers/value_providers/tenant-value-provider.helper";
import { SpaceValueProvider } from "~/app/_helpers/value_providers/space-value-provider.helper";
import { YesNoValueProvider } from "~/app/_helpers/value_providers/yesno-value-provider.helper";

@Component({
  selector: "ns-contract-add",
  templateUrl: "./contract-add.component.html",
})
export class ContractAddComponent
  extends BaseFormComponent<Contract, ContractDetailsFormSource, ContractDto>
  implements IBaseFormComponent, IContractFormValueProvider, OnInit, OnDestroy
{
  private _durationValueProvider: DurationValueProvider;
  private _termValueProvider: TermValueProvider;
  private _tenantValueProvider: TenantValueProvider;
  private _spaceValueProvider: SpaceValueProvider;
  private _yesNoValueProvider: YesNoValueProvider;

  private _spaceOptions;
  private currentSpaceTypeSub: Subscription;

  constructor(
    public dialogService: DialogService,
    public ngZone: NgZone,
    public router: RouterExtensions,
    public contractService: ContractService,
    public spaceTypeService: SpaceTypeService,
    public spaceService: SpaceService,
    public termService: TermService,
    public termMiscellaneousService: TermMiscellaneousService,
    public tenantService: TenantService,
    public translateService: TranslateService,
    private localManageTermService: LocalManageTermService,
    private localManageTermMiscellaneousService: LocalManageTermMiscellaneousService
  ) {
    super(dialogService, ngZone, router, translateService);
    this._entityService = contractService;
    this._entityDtoMapper = new ContractMapper();
  }

  ngOnInit() {
    this._entityFormSource = this._entityDtoMapper.initFormSource();
    this.initDialogTexts();

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

    this._yesNoValueProvider = new YesNoValueProvider(this.translateService);

    // Subscribes to the latest current space type.
    this.currentSpaceTypeSub = this.spaceTypeService.currentSpaceType.subscribe(
      (spaceType: { id: number; name: string }) => {
        this._spaceValueProvider.setCurrentSpaceTypeId(spaceType.id);
        this._spaceOptions = this._spaceValueProvider.spaceOptions;

        this._entityFormSource = this.reloadFormSource(this._entityFormSource, {
          spaceId: 0,
          spaceTypeName: spaceType.name,
        });
      }
    );
  }

  ngOnDestroy() {
    if (this.currentSpaceTypeSub) {
      this.currentSpaceTypeSub.unsubscribe();
    }
  }

  initDialogTexts() {
    this._saveNewDialogTexts = {
      title: this.translateService.instant(
        "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE"
      ),
      confirmMessage: this.translateService.instant(
        "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.CONFIRM_MESSAGE"
      ),
      successMessage: this.translateService.instant(
        "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.SUCCESS_MESSAGE"
      ),
      errorMessage: this.translateService.instant(
        "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.ERROR_MESSAGE"
      ),
    };
  }

  onPropertyCommitted(args: DataFormEventData) {
    /**
     * If Open contract, reset value for duration unit and duration value
     */
    if (args.propertyName === "isOpenContract") {
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

    if (args.propertyName === "durationUnit") {
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

    if (args.propertyName === "termId") {
      (async () => {
        if (this._entityFormSource.termId !== 0) {
          this.localManageTermService.resetEntityDetails();
          this.localManageTermMiscellaneousService.resetEntityList();

          const termDetails = await this.termService.getTerm(
            this._entityFormSource.termId
          );

          const termMiscellaneousList =
            await this.termMiscellaneousService.getTermMiscellaneousList(
              termDetails.id
            );

          this.localManageTermService.entityDetails.next(termDetails);
          this.localManageTermMiscellaneousService.entityList.next(
            termMiscellaneousList
          );

          this.spaceTypeService.currentSpaceType.next({
            id: termDetails.spaceTypeId,
            name: termDetails.spaceType.name,
          });
        } else {
          this.spaceTypeService.currentSpaceType.next({
            id: 0,
            name: "",
          });
        }
      })();
    }
  }

  onEditorUpdate(args: DataFormEventData) {
    if (args.propertyName === "effectiveDate") {
      if (typeof args.editor.setDateFormat !== "undefined") {
        this.changeDateFormatting(args.editor);
      }
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
    const codeProperty = dataForm.getPropertyByName("code");
    const tenandIdProperty = dataForm.getPropertyByName("tenantId");
    const termIdProperty = dataForm.getPropertyByName("termId");
    const spaceIdProperty = dataForm.getPropertyByName("spaceId");
    const spaceTypeNameProperty = dataForm.getPropertyByName("spaceTypeName");
    const durationUnitProperty = dataForm.getPropertyByName("durationUnit");
    const durationValueProperty = dataForm.getPropertyByName("durationValue");

    // Check and validate code field
    if (this._entityFormSource.code.trim() === "") {
      codeProperty.errorMessage = this.translateService.instant(
        "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.CODE_CONTROL.EMPTY_ERROR_MESSAGE"
      );
      isCodeValid = false;
    } else {
      if (this._entityFormSource.code.length > 10) {
        codeProperty.errorMessage = this.translateService.instant(
          "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.CODE_CONTROL.MAXLENGTH_ERROR_MESSAGE"
        );
        isCodeValid = false;
      } else {
        // Check if code already exists
        const checkCodeResult =
          await this.contractService.checkContractCodeIfExists(
            0,
            this._entityFormSource.code
          );

        if (checkCodeResult) {
          codeProperty.errorMessage = this.translateService.instant(
            "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.CODE_CONTROL.ALREADY_EXIST_ERROR_MESSAGE"
          );
        }
        isCodeValid = !checkCodeResult;
      }
    }

    // Check and validate tenant id field
    if (this._entityFormSource.tenantId === 0) {
      tenandIdProperty.errorMessage = this.translateService.instant(
        "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.TENANT_CONTROL.EMPTY_ERROR_MESSAGE"
      );
      isTenantValid = false;
    } else {
      isTenantValid = true;
    }

    // Check and validate term id field
    if (this._entityFormSource.termId === 0) {
      termIdProperty.errorMessage = this.translateService.instant(
        "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.TERM_CONTROL.EMPTY_ERROR_MESSAGE"
      );
      isTermValid = false;
    } else {
      isTermValid = true;

      const hasSpaces = this._spaceValueProvider.spaceOptions;
      const hasAvailableSpaces =
        this._spaceValueProvider.spaceOptions.items.find(
          (s) =>
            s.isNotOccupied ||
            (!s.isNotOccupied && s.occupiedByContractId === 0)
        );

      if (!hasSpaces) {
        spaceTypeNameProperty.errorMessage = this.translateService.instant(
          "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.TERM_CONTROL.NO_ASSIGNED_SPACES"
        );
        isSpaceTypeNameValid = false;
      } else {
        if (!hasAvailableSpaces) {
          spaceTypeNameProperty.errorMessage = this.translateService.instant(
            "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.TERM_CONTROL.NO_AVAILABLE_SPACES"
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
        "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.SPACE_CONTROL.EMPTY_ERROR_MESSAGE"
      );
      isSpaceValid = false;
    } else {
      const space = this._spaceValueProvider.getItem(
        this._entityFormSource.spaceId
      );

      if (!space.isNotOccupied && space.occupiedByContractId !== 0) {
        spaceIdProperty.errorMessage = this.translateService.instant(
          "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.SPACE_CONTROL.NOT_AVAILABLE_ERROR_MESSAGE"
        );
        isSpaceValid = false;
      } else {
        isSpaceValid = true;
      }
    }

    if (!this._entityFormSource.isOpenContract) {
      if (this._entityFormSource.durationUnit === DurationEnum.None) {
        durationUnitProperty.errorMessage = this.translateService.instant(
          "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.DURATION_UNIT_CONTROL.EMPTY_ERROR_MESSAGE"
        );
        isDurationUnitValid = false;
      } else {
        isDurationUnitValid = true;
      }

      if (this._entityFormSource.durationValue === 0) {
        durationValueProperty.errorMessage = this.translateService.instant(
          "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.GENERAL_INFORMATION_CONTROL_GROUP.DURATION_VALUE_CONTROL.EMPTY_ERROR_MESSAGE"
        );
        isDurationValueValid = false;
      } else {
        isDurationValueValid = true;
      }
    } else {
      isDurationUnitValid = true;
      isDurationValueValid = true;
    }

    dataForm.notifyValidated("code", isCodeValid);
    dataForm.notifyValidated("tenantId", isTenantValid);
    dataForm.notifyValidated("termId", isTermValid);
    dataForm.notifyValidated("spaceTypeName", isSpaceTypeNameValid);
    dataForm.notifyValidated("spaceId", isSpaceValid);
    dataForm.notifyValidated("durationUnit", isDurationUnitValid);
    dataForm.notifyValidated("durationValue", isDurationValueValid);

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

  saveNewInformation() {
    (async () => {
      const isFormValid = await this.validateForm();

      if (isFormValid) {
        this.dialogService
          .confirm(
            this._saveNewDialogTexts.title,
            this._saveNewDialogTexts.confirmMessage
          )
          .then((res: boolean) => {
            if (res) {
              this._isBusy = true;

              (async () => {
                const contractDetails =
                  this._entityDtoMapper.mapFormSourceToEntity(
                    this._entityFormSource
                  );
                const termDetails =
                  this.localManageTermService.entityDetails.value;
                const termMiscellaneousList =
                  this.localManageTermMiscellaneousService.entityList.value;

                this.localManageTermService.resetEntityDetails();
                this.localManageTermMiscellaneousService.resetEntityList();

                try {
                  await this.contractService.manageContract(
                    contractDetails,
                    termDetails,
                    termMiscellaneousList
                  );

                  this.dialogService
                    .alert(
                      this._saveNewDialogTexts.title,
                      this._saveNewDialogTexts.successMessage
                    )
                    .then(() => {
                      this._entityService.reloadListFlag();
                      this.router.back();
                    });
                } catch {
                  this.dialogService.alert(
                    this._saveNewDialogTexts.title,
                    this._saveNewDialogTexts.errorMessage
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

  navigateToManageTerm() {
    if (!this._entityFormSource.termId) {
      this.dialogService.alert(
        this.translateService.instant(
          "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.NO_SELECTED_TERM_DIALOG.TITLE"
        ),
        this.translateService.instant(
          "CONTRACT_MANAGE_DETAILS_PAGE.FORM_CONTROL.NO_SELECTED_TERM_DIALOG.WARNING_MESSAGE"
        )
      );

      return;
    }

    setTimeout(() => {
      const routeConfig: ExtendedNavigationExtras = {
        transition: {
          name: "slideLeft",
        },
      };

      this.router.navigate([`contracts/manage-term`], routeConfig);
    }, 100);
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
    return this._spaceOptions;
  }

  get yesNoOptions(): { key: YesNoEnum; label: string }[] {
    return this._yesNoValueProvider.yesNoOptions;
  }
}

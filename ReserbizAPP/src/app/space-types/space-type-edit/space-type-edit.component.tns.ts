import { Component, OnInit, NgZone } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { PageRoute, RouterExtensions } from 'nativescript-angular/router';
import { Page } from 'tns-core-modules/ui/page/page';

import { finalize, take } from 'rxjs/operators';

import { SpaceType } from '@src/app/_models/space-type.model';
import { SpaceTypeFormSource } from '@src/app/_models/space-type-form.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { SpaceTypeDto } from '@src/app/_dtos/space-type.dto';

import { DialogService } from '@src/app/_services/dialog.service';
import { SpaceTypeService } from '@src/app/_services/space-type.service';
import { ActionItemService } from '@src/app/_services/action-item.service';
import { BaseFormComponent } from '@src/app/shared/component/base-form.component';
import { SpaceTypeMapper } from '@src/app/_helpers/space-type-mapper.helper';
import { IBaseFormComponent } from '@src/app/_interfaces/ibase-form.component.interface';

@Component({
  selector: 'ns-space-type-edit',
  templateUrl: './space-type-edit.component.html',
  styleUrls: ['./space-type-edit.component.scss'],
})
export class SpaceTypeEditComponent
  extends BaseFormComponent<SpaceType, SpaceTypeFormSource, SpaceTypeDto>
  implements IBaseFormComponent, OnInit {
  constructor(
    public actionItemService: ActionItemService,
    public dialogService: DialogService,
    public ngZone: NgZone,
    public page: Page,
    public pageRoute: PageRoute,
    public router: RouterExtensions,
    public spaceTypeService: SpaceTypeService,
    public translateService: TranslateService
  ) {
    super(dialogService, ngZone, router, translateService);
    this._entityService = spaceTypeService;
    this._entityDtoMapper = new SpaceTypeMapper();
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        const spaceTypeId = +paramMap.get('id');

        this.spaceTypeService
          .getSpaceType(spaceTypeId)
          .pipe(take(1))
          .subscribe((spaceType: SpaceType) => {
            this._currentEntity = spaceType;
            this._currentFormEntityId = spaceType.id;

            this._entityFormSource = this._entityDtoMapper.mapEntityToFormSource(
              spaceType
            );

            this._entityFormSourceOriginal = this._entityFormSource.clone();

            this.actionItemService
              .setPage(this.page)
              .setActionItem('deleteSpaceTypeActionItem')
              .enable(this._currentEntity.isDeletable);
          });
      });
    });

    this.initDialogTexts();
  }

  initDialogTexts() {
    this._updateDialogTexts = {
      title: this.translateService.instant(
        'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.ERROR_MESSAGE'
      ),
    };
  }

  updateInformation() {
    (async () => {
      const isFormValid = await this.validateForm();

      if (isFormValid) {
        super.updateInformation();
      }
    })();
  }

  async validateForm(): Promise<boolean> {
    let isNameValid: boolean, isAvailableSlotValid: boolean;
    isNameValid = isAvailableSlotValid = true;

    const dataForm = this.formSource.dataForm;
    const nameProperty = dataForm.getPropertyByName('name');
    const availableSlotProperty = dataForm.getPropertyByName('availableSlot');

    // Check and validate name field
    if (this._entityFormSource.name.trim() === '') {
      nameProperty.errorMessage = this.translateService.instant(
        'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.NAME_CONTROL.HINT_TEXT'
      );
      isNameValid = false;
    } else {
      isNameValid = true;
    }

    // Check and validate available slot field
    const validateProposedNewSlotValue = await this.spaceTypeService.validateSpaceTypeProposedNewAvailableSlot(
      this._currentEntity.id,
      this._entityFormSource.availableSlot
    );

    if (!validateProposedNewSlotValue) {
      availableSlotProperty.errorMessage = this.translateService.instant(
        'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.SLOT_CONTROL.INVALID_AVAILABLE_SLOT'
      );
      isAvailableSlotValid = false;
    } else {
      isAvailableSlotValid = true;
    }

    dataForm.notifyValidated('name', isNameValid);
    dataForm.notifyValidated('availableSlot', isAvailableSlotValid);

    return Boolean(isNameValid && isAvailableSlotValid);
  }

  deleteSelectedSpaceType() {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.REMOVE_SPACE_TYPE_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.REMOVE_SPACE_TYPE_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.spaceTypeService
            .deleteItem(this._currentEntity.id)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.REMOVE_SPACE_TYPE_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.REMOVE_SPACE_TYPE_DIALOG.SUCCESS_MESSAGE'
                  ),
                  () => {
                    this.spaceTypeService.loadSpaceTypesFlag.next();
                    this.router.back();
                  }
                );
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.REMOVE_SPACE_TYPE_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.REMOVE_SPACE_TYPE_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }
}

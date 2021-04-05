import { Component, NgZone, OnInit } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';
import { RouterExtensions } from '@nativescript/angular';

import { BaseFormComponent } from '@src/app/shared/component/base-form.component';

import { SpaceDto } from '@src/app/_dtos/space-dto';

import { SpaceFormSource } from '@src/app/_models/form/space-form.model';
import { Space } from '@src/app/_models/space.model';
import { SpaceTypeOption } from '@src/app/_models/options/space-type-option.model';

import { DialogService } from '@src/app/_services/dialog.service';
import { SpaceService } from '@src/app/_services/space.service';
import { SpaceTypeService } from '@src/app/_services/space-type.service';

import { SpaceMapper } from '@src/app/_helpers/mappers/space-mapper.helper';
import { SpaceTypeValueProvider } from '@src/app/_helpers/value_providers/space-type-value-provider.helper';

@Component({
  selector: 'app-space-add',
  templateUrl: './space-add.component.html',
  styleUrls: ['./space-add.component.scss'],
})
export class SpaceAddComponent
  extends BaseFormComponent<Space, SpaceFormSource, SpaceDto>
  implements OnInit {
  private _spaceTypeValueProvider: SpaceTypeValueProvider;

  constructor(
    public dialogService: DialogService,
    public ngZone: NgZone,
    public router: RouterExtensions,
    public spaceService: SpaceService,
    public spaceTypeService: SpaceTypeService,
    public translateService: TranslateService
  ) {
    super(dialogService, ngZone, router, translateService);
    this._entityService = spaceService;
    this._entityDtoMapper = new SpaceMapper();
  }

  ngOnInit() {
    this._entityFormSource = this._entityDtoMapper.initFormSource();
    this.initDialogTexts();

    this._spaceTypeValueProvider = new SpaceTypeValueProvider(
      this.translateService,
      this.spaceTypeService
    );
  }

  initDialogTexts() {
    this._saveNewDialogTexts = {
      title: this.translateService.instant(
        'SPACE_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'SPACE_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'SPACE_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'SPACE_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.ERROR_MESSAGE'
      ),
    };
  }

  saveNewInformation() {
    this.validateForm().then((isFormValid: boolean) => {
      if (isFormValid) {
        super.saveNewInformation();
      }
    });
  }

  async validateForm(): Promise<boolean> {
    let isDescriptionValid: boolean, isSpaceTypeValid: boolean;
    isDescriptionValid = isSpaceTypeValid = true;

    const dataForm = this.formSource.dataForm;
    const nameProperty = dataForm.getPropertyByName('description');
    const spaceTypeProperty = dataForm.getPropertyByName('spaceTypeId');

    // Check and validate description field
    if (this._entityFormSource.description.trim() === '') {
      nameProperty.errorMessage = this.translateService.instant(
        'SPACE_ADD_DETAILS_PAGE.FORM_CONTROL.DESCRIPTION_CONTROL.EMPTY_DESCRIPTION_ERROR_MESSAGE'
      );
      isDescriptionValid = false;
    } else {
      isDescriptionValid = true;
    }

    // Check and validate space type field
    if (this._entityFormSource.spaceTypeId === 0) {
      spaceTypeProperty.errorMessage = this.translateService.instant(
        'SPACE_ADD_DETAILS_PAGE.FORM_CONTROL.SPACE_TYPE_CONTROL.NO_SELECTED_SPACE_TYPE_ERROR_MESSAGE'
      );
      isSpaceTypeValid = false;
    } else {
      isSpaceTypeValid = true;
    }

    dataForm.notifyValidated('description', isDescriptionValid);
    dataForm.notifyValidated('spaceTypeId', isSpaceTypeValid);

    return Boolean(isDescriptionValid && isSpaceTypeValid);
  }

  get spaceTypeOptions(): {
    key: string;
    label: string;
    items: SpaceTypeOption[];
  } {
    return this._spaceTypeValueProvider.spaceTypeOptions;
  }
}

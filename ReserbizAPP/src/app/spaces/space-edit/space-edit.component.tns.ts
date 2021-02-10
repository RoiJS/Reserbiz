import { Component, NgZone, OnInit } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';
import { finalize, take } from 'rxjs/operators';

import { Page } from '@nativescript/core';
import { PageRoute, RouterExtensions } from '@nativescript/angular';

import { BaseFormComponent } from '@src/app/shared/component/base-form.component';

import { SpaceMapper } from '@src/app/_helpers/space-mapper.helper';
import { SpaceTypeValueProvider } from '@src/app/_helpers/space-type-value-provider.helper';

import { SpaceDto } from '@src/app/_dtos/space-dto';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

import { SpaceFormSource } from '@src/app/_models/space-form.model';
import { Space } from '@src/app/_models/space.model';
import { SpaceTypeOption } from '@src/app/_models/space-type-option.model';

import { DialogService } from '@src/app/_services/dialog.service';
import { SpaceTypeService } from '@src/app/_services/space-type.service';
import { SpaceService } from '@src/app/_services/space.service';

@Component({
  selector: 'app-space-edit',
  templateUrl: './space-edit.component.html',
  styleUrls: ['./space-edit.component.scss'],
})
export class SpaceEditComponent
  extends BaseFormComponent<Space, SpaceFormSource, SpaceDto>
  implements OnInit {
  private _spaceTypeValueProvider: SpaceTypeValueProvider;
  private _spaceTypeOptions;

  constructor(
    public dialogService: DialogService,
    public ngZone: NgZone,
    public page: Page,
    public pageRoute: PageRoute,
    public router: RouterExtensions,
    public spaceService: SpaceService,
    public spaceTypeService: SpaceTypeService,
    public translateService: TranslateService
  ) {
    super(dialogService, ngZone, router, translateService);
    this._entityService = spaceService;
    this._entityDtoMapper = new SpaceMapper();

    this._spaceTypeValueProvider = new SpaceTypeValueProvider(
      this.translateService,
      this.spaceTypeService
    );

    this._spaceTypeOptions = this._spaceTypeValueProvider.spaceTypeOptions;
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        const spaceId = +paramMap.get('id');

        this.spaceService
          .getSpace(spaceId)
          .pipe(take(1))
          .subscribe((spaceType: Space) => {
            this._currentEntity = spaceType;
            this._currentFormEntityId = spaceType.id;

            this._entityFormSource = this._entityDtoMapper.mapEntityToFormSource(
              spaceType
            );

            this._entityFormSourceOriginal = this._entityFormSource.clone();

            this._spaceTypeValueProvider.setCurrenValue(
              this._currentEntity.spaceTypeId
            );
            this._spaceTypeOptions = this._spaceTypeValueProvider.spaceTypeOptions;
          });
      });
    });

    this.initDialogTexts();
  }

  initDialogTexts() {
    this._updateDialogTexts = {
      title: this.translateService.instant(
        'SPACE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'SPACE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'SPACE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'SPACE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.ERROR_MESSAGE'
      ),
    };
  }

  updateInformation() {
    this.validateForm().then((isFormValid: boolean) => {
      if (isFormValid) {
        super.updateInformation();
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
        'SPACE_EDIT_DETAILS_PAGE.FORM_CONTROL.DESCRIPTION_CONTROL.EMPTY_DESCRIPTION_ERROR_MESSAGE'
      );
      isDescriptionValid = false;
    } else {
      isDescriptionValid = true;
    }

    // Check and validate space type field
    if (this._entityFormSource.spaceTypeId === 0) {
      spaceTypeProperty.errorMessage = this.translateService.instant(
        'SPACE_EDIT_DETAILS_PAGE.FORM_CONTROL.SPACE_TYPE_CONTROL.NO_SELECTED_SPACE_TYPE_ERROR_MESSAGE'
      );
      isSpaceTypeValid = false;
    } else {
      isSpaceTypeValid = true;
    }

    dataForm.notifyValidated('description', isDescriptionValid);
    dataForm.notifyValidated('spaceTypeId', isSpaceTypeValid);

    return Boolean(isDescriptionValid && isSpaceTypeValid);
  }

  deleteSelectedSpaceType() {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'SPACE_EDIT_DETAILS_PAGE.FORM_CONTROL.REMOVE_SPACE_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'SPACE_EDIT_DETAILS_PAGE.FORM_CONTROL.REMOVE_SPACE_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.spaceService
            .deleteItem(this._currentEntity.id)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'SPACE_EDIT_DETAILS_PAGE.FORM_CONTROL.REMOVE_SPACE_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'SPACE_EDIT_DETAILS_PAGE.FORM_CONTROL.REMOVE_SPACE_DIALOG.SUCCESS_MESSAGE'
                  ),
                  () => {
                    this.spaceService.loadSpacesFlag.next();
                    this.router.back();
                  }
                );
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'SPACE_EDIT_DETAILS_PAGE.FORM_CONTROL.REMOVE_SPACE_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'SPACE_EDIT_DETAILS_PAGE.FORM_CONTROL.REMOVE_SPACE_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }

  get spaceTypeOptions(): {
    key: string;
    label: string;
    items: SpaceTypeOption[];
  } {
    return this._spaceTypeOptions;
  }
}

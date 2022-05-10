import { Component, OnInit, NgZone } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { PageRoute, RouterExtensions } from '@nativescript/angular';
import { Page } from '@nativescript/core';

import { finalize, take } from 'rxjs/operators';

import { SpaceType } from '../../_models/space-type.model';
import { SpaceTypeFormSource } from '../../_models/form/space-type-form.model';
import { ButtonOptions } from '../../_enum/button-options.enum';
import { SpaceTypeDto } from '../../_dtos/space-type.dto';

import { DialogService } from '../../_services/dialog.service';
import { SpaceTypeService } from '../../_services/space-type.service';
import { SpaceTypeMapper } from '../../_helpers/mappers/space-type-mapper.helper';

import { IBaseFormComponent } from '../../_interfaces/components/ibase-form.component.interface';

import { BaseFormComponent } from '../../shared/component/base-form.component';

@Component({
  selector: 'ns-space-type-edit',
  templateUrl: './space-type-edit.component.html',
  styleUrls: ['./space-type-edit.component.scss'],
})
export class SpaceTypeEditComponent
  extends BaseFormComponent<SpaceType, SpaceTypeFormSource, SpaceTypeDto>
  implements IBaseFormComponent, OnInit {
  constructor(
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
      .subscribe((res: ButtonOptions) => {
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

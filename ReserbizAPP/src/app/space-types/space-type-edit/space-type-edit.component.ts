import { Component, OnInit, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { PageRoute, RouterExtensions } from 'nativescript-angular/router';
import { RadDataFormComponent } from 'nativescript-ui-dataform/angular/dataform-directives';
import { Page } from 'tns-core-modules/ui/page/page';

import { finalize, take } from 'rxjs/operators';

import { SpaceType } from '@src/app/_models/space-type.model';
import { SpaceTypeFormSource } from '@src/app/_models/space-type-form.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { SpaceTypeDto } from '@src/app/_dtos/space-type.dto';

import { DialogService } from '@src/app/_services/dialog.service';
import { SpaceTypeService } from '@src/app/_services/space-type.service';
import { ActionItemService } from '@src/app/_services/action-item.service';

@Component({
  selector: 'ns-space-type-edit',
  templateUrl: './space-type-edit.component.html',
  styleUrls: ['./space-type-edit.component.scss'],
})
export class SpaceTypeEditComponent implements OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  spaceTypeForm: RadDataFormComponent;

  private _currentSpaceType: SpaceType;
  private _spaceTypeFormSource: SpaceTypeFormSource;
  private _spaceTypeFormSourceOriginal: SpaceTypeFormSource;

  private _isBusy = false;

  constructor(
    private actionItemService: ActionItemService,
    private dialogService: DialogService,
    private page: Page,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private spaceTypeService: SpaceTypeService,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        const spaceTypeId = +paramMap.get('id');

        this.spaceTypeService
          .getSpaceType(spaceTypeId)
          .pipe(take(1))
          .subscribe((spaceType: SpaceType) => {
            this._currentSpaceType = spaceType;

            this._spaceTypeFormSource = new SpaceTypeFormSource(
              this._currentSpaceType.name,
              this._currentSpaceType.description,
              this._currentSpaceType.rate,
              this._currentSpaceType.availableSlot
            );

            this._spaceTypeFormSourceOriginal = this._spaceTypeFormSource.clone();

            this.actionItemService
              .setPage(this.page)
              .setActionItem('deleteSpaceTypeActionItem')
              .enable(this._currentSpaceType.isDeletable);
          });
      });
    });
  }

  saveInformation() {
    const isFormInvalid = this.spaceTypeForm.dataForm.hasValidationErrors();
    const isFormHasChanged = !this._spaceTypeFormSource.isSame(
      this._spaceTypeFormSourceOriginal
    );

    if (!isFormInvalid && isFormHasChanged) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .then((res) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            const spaceTypeForCreate = new SpaceTypeDto(
              this._spaceTypeFormSource.name,
              this._spaceTypeFormSource.description,
              this._spaceTypeFormSource.rate,
              this._spaceTypeFormSource.availableSlot
            );

            this.spaceTypeService
              .updateSpaceType(this._currentSpaceType.id, spaceTypeForCreate)
              .pipe(finalize(() => (this._isBusy = false)))
              .subscribe(
                () => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.SUCCESS_MESSAGE'
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
                      'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'SPACE_TYPE_EDIT_DETAILS_PAGE.FORM_CONTROL.EDIT_DIALOG.ERROR_MESSAGE'
                    )
                  );
                }
              );
          }
        });
    }
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
            .deleteSpaceType(this._currentSpaceType.id)
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

  get spaceTypeFormSource(): SpaceTypeFormSource {
    return this._spaceTypeFormSource;
  }

  get currentSpaceType(): SpaceType {
    return this._currentSpaceType;
  }
}

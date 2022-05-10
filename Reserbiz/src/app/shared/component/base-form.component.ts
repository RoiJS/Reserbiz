import { Component, OnInit, ViewChild, NgZone } from '@angular/core';

import { RouterExtensions } from '@nativescript/angular';
import { RadDataFormComponent } from 'nativescript-ui-dataform/angular';

import { finalize } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';

import { IBaseFormSource } from '../../_interfaces/ibase-form-source.interface';
import { IBaseDialogTexts } from '../../_interfaces/ibase-dialog-texts.interface';
import { IBaseDtoEntityMapper } from '../../_interfaces/mappers/ibase-dto-entity-mapper.interface';
import { IBaseService } from '../../_interfaces/services/ibase-service.interface';
import { IBaseDto } from '../../_interfaces/ibase-dto.interface';
import { IEntity } from '../../_interfaces/ientity.interface';

import { ButtonOptions } from '../../_enum/button-options.enum';
import { DialogService } from '../../_services/dialog.service';
import { BaseFormHelper } from '../../_helpers/base_helpers/base-form.helper';

@Component({
  template: ``,
})
export class BaseFormComponent<
    TEntity extends IEntity,
    TFormSource extends IBaseFormSource<TFormSource>,
    TDtoEntity extends IBaseDto
  >
  extends BaseFormHelper<TFormSource>
  implements OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  formSource: RadDataFormComponent;

  protected _currentEntity: TEntity;
  protected _currentFormEntityId: number;

  protected _isBusy = false;

  protected _entityFormSource: TFormSource;
  protected _entityFormSourceOriginal: TFormSource;

  protected _saveNewDialogTexts: IBaseDialogTexts;
  protected _updateDialogTexts: IBaseDialogTexts;

  protected _entityService: IBaseService<TEntity>;
  protected _entityDtoMapper: IBaseDtoEntityMapper<
    TEntity,
    TFormSource,
    TDtoEntity
  >;

  constructor(
    public dialogService: DialogService,
    public ngZone: NgZone,
    public router: RouterExtensions,
    public translateService: TranslateService
  ) {
    super();
  }

  ngOnInit() {}

  saveNewInformation() {
    const isFormInvalid = this.formSource.dataForm.hasValidationErrors();

    if (!isFormInvalid) {
      this.dialogService
        .confirm(
          this._saveNewDialogTexts.title,
          this._saveNewDialogTexts.confirmMessage
        )
        .subscribe((res: ButtonOptions) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            const dtoToCreate = this._entityDtoMapper.mapFormSourceToDto(
              this._entityFormSource
            );

            this._entityService
              .saveNewEntity({
                id: this._currentFormEntityId,
                dtoEntity: dtoToCreate,
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
                    this._saveNewDialogTexts.title,
                    this._saveNewDialogTexts.successMessage,
                    () => {
                      this._entityService.reloadListFlag(true);
                      this.router.back();
                    }
                  );
                },
                (error: Error) => {
                  this.dialogService.alert(
                    this._saveNewDialogTexts.title,
                    this._saveNewDialogTexts.errorMessage
                  );
                }
              );
          }
        });
    }
  }

  updateInformation(onSuccessCallback?: () => void) {
    const isFormInvalid = this.formSource.dataForm.hasValidationErrors();
    const isFormHasChanged = !this._entityFormSource.isSame(
      this._entityFormSourceOriginal
    );

    if (!isFormInvalid && isFormHasChanged) {
      this.dialogService
        .confirm(
          this._updateDialogTexts.title,
          this._updateDialogTexts.confirmMessage
        )
        .subscribe((res: ButtonOptions) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            const dtoForUpdate = this._entityDtoMapper.mapFormSourceToDto(
              this._entityFormSource
            );

            this._entityService
              .updateEntity({
                id: this._currentFormEntityId,
                dtoEntity: dtoForUpdate,
              })
              .pipe(finalize(() => (this._isBusy = false)))
              .subscribe(
                () => {
                  this.dialogService.alert(
                    this._updateDialogTexts.title,
                    this._updateDialogTexts.successMessage,
                    () => {
                      if (onSuccessCallback) {
                        onSuccessCallback();
                      } else {
                        this._entityService.reloadListFlag();
                        this.router.back();
                      }
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
    }
  }

  get entityFormSource(): TFormSource {
    return this._entityFormSource;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }

  get currentEntity(): TEntity {
    return this._currentEntity;
  }
}

import { Component, OnInit, ViewChild } from '@angular/core';

import { RouterExtensions } from 'nativescript-angular/router';
import { RadDataFormComponent } from 'nativescript-ui-dataform/angular/dataform-directives';

import { TranslateService } from '@ngx-translate/core';

import { finalize } from 'rxjs/operators';
import { pipe } from 'rxjs';

import { SpaceTypeService } from '@src/app/_services/space-type.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { SpaceTypeFormSource } from '@src/app/_models/space-type-form.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { SpaceTypeDto } from '@src/app/_dtos/space-type.dto';

@Component({
  selector: 'ns-space-type-add',
  templateUrl: './space-type-add.component.html',
  styleUrls: ['./space-type-add.component.scss'],
})
export class SpaceTypeAddComponent implements OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  spaceTypeForm: RadDataFormComponent;

  private _spaceTypeFormSource: SpaceTypeFormSource;
  private _isBusy = false;

  constructor(
    private dialogService: DialogService,
    private router: RouterExtensions,
    private spaceTypeService: SpaceTypeService,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this._spaceTypeFormSource = new SpaceTypeFormSource('', '', 0, 0);
  }

  saveInformation() {
    const isFormInvalid = this.spaceTypeForm.dataForm.hasValidationErrors();

    if (!isFormInvalid) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'SPACE_TYPE_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'SPACE_TYPE_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.CONFIRM_MESSAGE'
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
              .saveNewSpaceType(spaceTypeForCreate)
              .pipe(finalize(() => (this._isBusy = false)))
              .subscribe(
                () => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'SPACE_TYPE_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'SPACE_TYPE_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.SUCCESS_MESSAGE'
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
                      'SPACE_TYPE_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'SPACE_TYPE_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.ERROR_MESSAGE'
                    )
                  );
                }
              );
          }
        });
    }
  }

  get spaceTypeFormSource(): SpaceTypeFormSource {
    return this._spaceTypeFormSource;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }
}

import { Component, NgZone, OnDestroy, OnInit } from '@angular/core';

import { RouterExtensions } from '@nativescript/angular';
import { TranslateService } from '@ngx-translate/core';

import { Subscription } from 'rxjs';

import { BaseFormComponent } from '@src/app/shared/component/base-form.component';

import { SettingsDto } from '@src/app/_dtos/settings-dto';

import { SettingsMapper } from '@src/app/_helpers/settings-mapper.helper';

import { IBaseFormComponent } from '@src/app/_interfaces/ibase-form.component.interface';

import { SettingsFormSource } from '@src/app/_models/settings-form.model';
import { Settings } from '@src/app/_models/settings.model';

import { DialogService } from '@src/app/_services/dialog.service';
import { SettingsService } from '@src/app/_services/settings.service';

@Component({
  selector: 'ns-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent
  extends BaseFormComponent<Settings, SettingsFormSource, SettingsDto>
  implements IBaseFormComponent, OnInit, OnDestroy {
  private _loadSettingsDetailsFlagSub: Subscription;

  constructor(
    public dialogService: DialogService,
    public ngZone: NgZone,
    public router: RouterExtensions,
    public settingsService: SettingsService,
    public translateService: TranslateService
  ) {
    super(dialogService, ngZone, router, translateService);
    this._entityService = settingsService;
    this._entityDtoMapper = new SettingsMapper();
  }

  ngOnInit() {
    this.initDialogTexts();
    this._loadSettingsDetailsFlagSub = this.settingsService.settings.subscribe(
      (settings: Settings) => {
        this._entityFormSource = this._entityDtoMapper.mapEntityToFormSource(
          settings
        );

        this._entityFormSourceOriginal = this._entityFormSource.clone();
      }
    );
  }

  ngOnDestroy() {
    if (this._loadSettingsDetailsFlagSub) {
      this._loadSettingsDetailsFlagSub.unsubscribe();
    }
  }

  initDialogTexts() {
    this._updateDialogTexts = {
      title: this.translateService.instant('SETTINGS.SAVE_DIALOG.TITLE'),
      confirmMessage: this.translateService.instant(
        'SETTINGS.SAVE_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'SETTINGS.SAVE_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'SETTINGS.SAVE_DIALOG.ERROR_MESSAGE'
      ),
    };
  }

  updateInformation() {
    super.updateInformation(() => {
      const latestSettings = this._entityDtoMapper.mapFormSourceToEntity(
        this._entityFormSource
      );
      this.settingsService.settings.next(latestSettings);
    });
  }
}

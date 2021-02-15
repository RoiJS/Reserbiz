import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { SettingsMapper } from '../_helpers/mappers/settings-mapper.helper';

import { IBaseService } from '../_interfaces/services/ibase-service.interface';
import { IDtoProcess } from '../_interfaces/idto-process.interface';

import { Settings } from '../_models/settings.model';

import { BaseService } from './base.service';

@Injectable({ providedIn: 'root' })
export class SettingsService
  extends BaseService<Settings>
  implements IBaseService<Settings> {
  private _settings = new BehaviorSubject<Settings>(null);
  private _loadSettingsFlag = new BehaviorSubject<boolean>(false);

  constructor(public http: HttpClient) {
    super(new SettingsMapper(), http);
  }

  async getSettingsDetails(): Promise<void> {
    const settingsFromServer = await this.getEntityFromServer(
      `${this._apiBaseUrl}/clientsettings/getSettings`
    ).toPromise();

    this._settings.next(settingsFromServer);
  }

  updateEntity(settingsForUpdateDto: IDtoProcess): Observable<void> {
    return this.updateEntityToServer(
      `${this._apiBaseUrl}/clientsettings/saveSettings`,
      settingsForUpdateDto.dtoEntity
    );
  }

  get settings(): BehaviorSubject<Settings> {
    return this._settings;
  }

  reloadListFlag(reset: boolean) {
    this._loadSettingsFlag.next(reset);
  }
}

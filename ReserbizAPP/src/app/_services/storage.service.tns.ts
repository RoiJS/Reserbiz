import { Injectable } from '@angular/core';
import * as appSettings from 'tns-core-modules/application-settings';

@Injectable({ providedIn: 'root' })
export class StorageService {
  constructor() {}

  storeString(key: string, value: string) {
    appSettings.setString(key, value);
  }

  hasKey(key: string) {
    return appSettings.hasKey(key);
  }

  getString(key: string) {
    return appSettings.getString(key);
  }

  remove(key: string) {
    appSettings.remove(key);
  }
}

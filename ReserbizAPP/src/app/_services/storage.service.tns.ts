import { Injectable } from '@angular/core';
import { ApplicationSettings } from '@nativescript/core';

@Injectable({ providedIn: 'root' })
export class StorageService {
  constructor() {}

  storeString(key: string, value: string) {
    ApplicationSettings.setString(key, value);
  }

  hasKey(key: string) {
    return ApplicationSettings.hasKey(key);
  }

  getString(key: string) {
    return ApplicationSettings.getString(key);
  }

  remove(key: string) {
    ApplicationSettings.remove(key);
  }
}

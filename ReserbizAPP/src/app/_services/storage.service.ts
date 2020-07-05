import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class StorageService {
  constructor() {}

  storeString(key: string, value: string) {
    localStorage.setItem(key, value);
  }

  hasKey(key: string) {
    return !!localStorage.getItem(key);
  }

  getString(key: string) {
    return localStorage.getItem(key);
  }

  remove(key: string) {
    localStorage.removeItem(key);
  }
}

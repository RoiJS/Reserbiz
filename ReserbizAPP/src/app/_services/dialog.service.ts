import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class DialogService {
  constructor() {}

  alert(title: string, message: string) {
    alert(message);
  }
}

import { Injectable } from '@angular/core';
import { RouterExtensions } from 'nativescript-angular/router';

@Injectable({ providedIn: 'root' })
export class RoutingService {
  constructor(private router: RouterExtensions) {}

  replace(commands: any[]) {
    this.router.navigate(commands, { clearHistory: true });
  }
}

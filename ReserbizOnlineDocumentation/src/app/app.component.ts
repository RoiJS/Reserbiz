import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Subscription } from 'rxjs';

import { UIService } from './services/ui.service';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy, AfterViewInit {
  @ViewChild(MatSidenav) sideNav: MatSidenav | undefined;

  private _isHandset: boolean = false;
  private isHandSetSubscription: Subscription | undefined;

  constructor(private uiService: UIService) {}

  ngOnInit() {}

  ngOnDestroy() {
    if (this.isHandSetSubscription) {
      this.isHandSetSubscription?.unsubscribe();
    }
  }

  ngAfterViewInit() {
    this.uiService.setDrawerRef(this.sideNav);

    this.isHandSetSubscription = this.uiService.isHandset.subscribe(
      (result: boolean) => {
        this._isHandset = result;
      }
    );
  }

  get isHandset(): boolean {
    return this._isHandset;
  }
}

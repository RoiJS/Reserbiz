import {
  AfterViewInit,
  Component,
  Input,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { Subscription } from 'rxjs';

import { UIService } from 'src/app/services/ui.service';

@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.scss'],
})
export class ToolBarComponent implements OnInit, OnDestroy, AfterViewInit {
  @Input() title = '';

  private _isHandset: boolean = false;
  private isHandSetSubscription: Subscription | undefined;

  constructor(private uiService: UIService) {}

  ngOnInit() {}

  ngOnDestroy() {
    if (this.isHandSetSubscription) {
      this.isHandSetSubscription?.unsubscribe();
    }
  }

  toggleDrawer() {
    this.uiService.toggleDrawer();
  }

  ngAfterViewInit() {
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

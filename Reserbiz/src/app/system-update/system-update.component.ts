import { Component, OnInit } from '@angular/core';
import { Page } from '@nativescript/core';

@Component({
  selector: 'ns-system-update',
  templateUrl: './system-update.component.html',
  styleUrls: ['./system-update.component.scss'],
})
export class SystemUpdateComponent implements OnInit {
  constructor(private page: Page) {}

  ngOnInit() {
    this.hideActionBar();
  }

  hideActionBar() {
    this.page.actionBarHidden = true;
  }
}

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  private _isBusy: boolean;
  constructor() {
  }

  ngOnInit() {
    this._isBusy = true;
    setTimeout(() => {
      this._isBusy = false;
    }, 1000);
  }

  get isBusy(): boolean {
    return this._isBusy;
  }
}

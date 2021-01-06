import { Component, OnDestroy, OnInit } from '@angular/core';

import { BaseListComponent } from '@src/app/shared/component/base-list.component';

import { Contract } from '@src/app/_models/contract.model';

@Component({
  selector: 'ns-upcoming-contract-due-dates-widget',
  templateUrl: './upcoming-contract-due-dates-widget.component.html',
  styleUrls: ['./upcoming-contract-due-dates-widget.component.scss'],
})
export class UpcomingContractDueDatesWidgetComponent implements OnInit {
  constructor() {}

  ngOnInit() {}
}

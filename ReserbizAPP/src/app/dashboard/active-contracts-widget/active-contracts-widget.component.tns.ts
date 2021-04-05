import { Component, OnInit } from '@angular/core';

import { BaseWidgetComponent } from '@src/app/shared/component/base-widget.component';

import { ContractService } from '@src/app/_services/contract.service';

@Component({
  selector: 'ns-active-contracts-widget',
  templateUrl: './active-contracts-widget.component.html',
  styleUrls: ['./active-contracts-widget.component.scss'],
})
export class ActiveContractsWidgetComponent
  extends BaseWidgetComponent
  implements OnInit {
  constructor(private contractService: ContractService) {
    super();
  }

  ngOnInit() {
    this._isBusy = true;
    setTimeout(() => {
      (async () => {
        this._entityCount = await this.contractService.getActiveContractsCount();
        this._isBusy = false;
      })();
    }, 2000);
  }
}

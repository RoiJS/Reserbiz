import {
  Component,
  OnInit,
  Input,
  OnChanges,
  SimpleChanges,
} from '@angular/core';

import { Contract } from '@src/app/_models/contract.model';

@Component({
  selector: 'ns-contract-details-panel',
  templateUrl: './contract-details-panel.component.html',
  styleUrls: ['./contract-details-panel.component.scss'],
})
export class ContractDetailsPanelComponent implements OnInit, OnChanges {
  @Input() currentContract: Contract;

  constructor() {}

  ngOnInit() {}

  ngOnChanges(args: SimpleChanges) {
    if (args.currentContract.currentValue) {
      // console.log(args.currentContract.currentValue);
    }
  }
}

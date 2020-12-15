import { Component, OnInit } from '@angular/core';

import { PageRoute } from 'nativescript-angular';

@Component({
  selector: 'app-payments',
  templateUrl: './payments.component.html',
  styleUrls: ['./payments.component.scss']
})
export class PaymentsComponent implements OnInit {

  private _currentAccountStatementId: number;
  private _updateAccountStatementListFlag: any;

  constructor(
    private pageRoute: PageRoute
  ) { }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentAccountStatementId = +paramMap.get('accountStatementId');

        console.log(this._currentAccountStatementId);
        // this._updateAccountStatementListFlag = this.accountStatementService.loadAccountStatementListFlag.subscribe(
        //   () => {
        //     this.getAccountStatmentInformation();
        //   }
        // );
      });
    });
  }
}

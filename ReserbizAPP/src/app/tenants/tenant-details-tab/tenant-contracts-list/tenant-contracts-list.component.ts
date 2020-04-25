import { Component, OnInit, ViewChild } from '@angular/core';
import { RouterExtensions, PageRoute } from 'nativescript-angular/router';

import { Page } from 'tns-core-modules/ui/page/page';

import { take, finalize } from 'rxjs/operators';

import { ContractService } from '@src/app/_services/contract.service';
import { Contract } from '@src/app/_models/contract.model';
import { RadListViewComponent } from 'nativescript-ui-listview/angular/listview-directives';
import { ObservableArray } from 'tns-core-modules/data/observable-array/observable-array';

@Component({
  selector: 'ns-tenant-contracts-list',
  templateUrl: './tenant-contracts-list.component.html',
  styleUrls: ['./tenant-contracts-list.component.scss'],
})
export class TenantContractsListComponent implements OnInit {
  @ViewChild(RadListViewComponent, { static: true })
  contractListView: RadListViewComponent;

  private _contractsList: ObservableArray<Contract>;

  private _currentTenantId: number;
  private _isBusy: boolean;

  constructor(
    private contractService: ContractService,
    private router: RouterExtensions,
    private page: Page,
    private pageRoute: PageRoute
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentTenantId = +paramMap.get('tenantId');
        this.getContractList();
      });
    });
    this.page.actionBarHidden = true;
  }

  getContractList() {
    this.contractService
      .getContracts(this._currentTenantId)
      .pipe(
        take(1),
        finalize(() => (this._isBusy = false))
      )
      .subscribe((contracts: Contract[]) => {
        this._contractsList = new ObservableArray<Contract>(contracts);
      });
  }

  get isBusy(): boolean {
    return this._isBusy;
  }

  get contractList(): ObservableArray<Contract> {
    return this._contractsList;
  }
}

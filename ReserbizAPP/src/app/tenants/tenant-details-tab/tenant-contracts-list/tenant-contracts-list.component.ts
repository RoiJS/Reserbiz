import { Component, OnInit } from '@angular/core';
import { RouterExtensions } from 'nativescript-angular/router';
import { Page } from 'tns-core-modules/ui/page/page';

@Component({
  selector: 'ns-tenant-contracts-list',
  templateUrl: './tenant-contracts-list.component.html',
  styleUrls: ['./tenant-contracts-list.component.scss'],
})
export class TenantContractsListComponent implements OnInit {

  constructor(private router: RouterExtensions) {}

  ngOnInit() {}
}

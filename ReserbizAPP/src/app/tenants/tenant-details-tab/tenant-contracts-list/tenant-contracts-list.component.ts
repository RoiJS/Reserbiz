import { Component, OnInit } from '@angular/core';
import { RouterExtensions } from 'nativescript-angular/router';

@Component({
  selector: 'ns-tenant-contracts-list',
  templateUrl: './tenant-contracts-list.component.html',
  styleUrls: ['./tenant-contracts-list.component.scss'],
})
export class TenantContractsListComponent implements OnInit {
  constructor(private router: RouterExtensions) {}

  ngOnInit() {}

  onGoBack() {
    this.router.navigate(['/tenants'], {
      transition: {
        name: 'slideRight',
      },
    });
  }
}

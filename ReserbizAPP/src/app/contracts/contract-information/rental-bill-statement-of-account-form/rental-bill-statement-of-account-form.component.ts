import { Component, OnInit } from '@angular/core';

import { ContractService } from '@src/app/_services/contract.service';

@Component({
  selector: 'app-rental-bill-statement-of-account-form',
  templateUrl: './rental-bill-statement-of-account-form.component.html',
  styleUrls: ['./rental-bill-statement-of-account-form.component.scss'],
})
export class RentalBillStatementOfAccountFormComponent implements OnInit {
  constructor(private contractService: ContractService) {}

  ngOnInit() {}
}

import {
  AfterViewChecked,
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

import { UIService } from 'src/app/services/ui.service';
import { SharedComponent } from 'src/app/shared/components/shared/shared.component';

@Component({
  selector: 'app-payments',
  templateUrl: './payments.component.html',
  styleUrls: ['./payments.component.scss'],
})
export class PaymentsComponent
  extends SharedComponent
  implements OnInit, OnDestroy, AfterViewInit, AfterViewChecked
{
  constructor(
    protected activatedRoute: ActivatedRoute,
    protected uiService: UIService,
    private translateService: TranslateService
  ) {
    super(uiService);
    this.activatedRoute = activatedRoute;
  }

  ngOnInit(): void {
    super.ngOnInit();
    this.setDatasource();
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  ngAfterViewInit(): void {
    super.ngAfterViewInit();
  }

  ngAfterViewChecked(): void {
    super.ngAfterViewChecked();
  }

  setDatasource(): void {
    this.dataSource = [
      {
        name: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_FROM_DEPOSIT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_FROM_DEPOSIT_INFORMATION.DATA_TYPE'
        ),
        definition: `
          ${this.translateService.instant(
            'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_FROM_DEPOSIT_INFORMATION.DESCRIPTION_1'
          )}
          ${this.translateService.instant(
            'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_FROM_DEPOSIT_INFORMATION.DESCRIPTION_2'
          )}
          ${this.translateService.instant(
            'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_FROM_DEPOSIT_INFORMATION.DESCRIPTION_3'
          )}
          ${this.translateService.instant(
            'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_FROM_DEPOSIT_INFORMATION.DESCRIPTION_4'
          )}
          ${this.translateService.instant(
            'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_FROM_DEPOSIT_INFORMATION.DESCRIPTION_5'
          )}
          ${this.translateService.instant(
            'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_FROM_DEPOSIT_INFORMATION.DESCRIPTION_6'
          )}
          ${this.translateService.instant(
            'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_FROM_DEPOSIT_INFORMATION.DESCRIPTION_7'
          )}
          ${this.translateService.instant(
            'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_FROM_DEPOSIT_INFORMATION.DESCRIPTION_8'
          )}
        `,
      },
      {
        name: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.PAYMENT_FOR_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.PAYMENT_FOR_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.PAYMENT_FOR_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_INFORMATION.DATA_TYPE'
        ),
        definition: `
          ${this.translateService.instant(
            'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_INFORMATION.DESCRIPTION_1'
          )}
          ${this.translateService.instant(
            'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_INFORMATION.DESCRIPTION_2'
          )}
          ${this.translateService.instant(
            'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.AMOUNT_INFORMATION.DESCRIPTION_3'
          )}
        `,
      },
      {
        name: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.DATE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.DATE_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.DATE_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.TIME_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.TIME_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.TIME_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.NOTES_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.NOTES_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TABLE_INFORMATION.NOTES_INFORMATION.DESCRIPTION'
        ),
      },
    ];
  }
}

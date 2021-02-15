import { Component, OnInit, NgZone } from '@angular/core';
import { PageRoute, RouterExtensions } from '@nativescript/angular';
import { TranslateService } from '@ngx-translate/core';

import { DialogService } from '@src/app/_services/dialog.service';
import { TermMiscellaneousService } from '@src/app/_services/term-miscellaneous.service';

import { BaseFormComponent } from '@src/app/shared/component/base-form.component';

import { TermMiscellaneous } from '@src/app/_models/term-miscellaneous.model';
import { TermMiscellaneousFormSource } from '@src/app/_models/form/term-miscellaneous-form.model';

import { TermMiscellaneousDto } from '@src/app/_dtos/term-miscellaneous.dto';

import { IBaseFormComponent } from '@src/app/_interfaces/components/ibase-form.component.interface';

import { TermMiscellaneousMapper } from '@src/app/_helpers/mappers/term-miscellaneous-mapper.helper';
import { take, finalize } from 'rxjs/operators';

@Component({
  selector: 'ns-term-miscellaneous-edit',
  templateUrl: './term-miscellaneous-edit.component.html',
  styleUrls: ['./term-miscellaneous-edit.component.scss'],
})
export class TermMiscellaneousEditComponent
  extends BaseFormComponent<
    TermMiscellaneous,
    TermMiscellaneousFormSource,
    TermMiscellaneousDto
  >
  implements IBaseFormComponent, OnInit {
  constructor(
    public dialogService: DialogService,
    public ngZone: NgZone,
    public pageRoute: PageRoute,
    public router: RouterExtensions,
    public translateService: TranslateService,
    public termMiscellaneousService: TermMiscellaneousService
  ) {
    super(dialogService, ngZone, router, translateService);
    this._entityService = termMiscellaneousService;
    this._entityDtoMapper = new TermMiscellaneousMapper();
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentFormEntityId = +paramMap.get('termMiscellaneousId');

        this.termMiscellaneousService
          .getTermMiscellaneous(this._currentFormEntityId)
          .pipe(
            take(1),
            finalize(() => (this._isBusy = false))
          )
          .subscribe((termMiscellaneous: TermMiscellaneous) => {
            this._entityFormSource = this._entityDtoMapper.mapEntityToFormSource(
              termMiscellaneous
            );

            this._entityFormSourceOriginal = this._entityFormSource.clone();
          });
      });
    });
    this.initDialogTexts();
  }

  initDialogTexts() {
    this._updateDialogTexts = {
      title: this.translateService.instant(
        'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.ERROR_MESSAGE'
      ),
    };
  }
}

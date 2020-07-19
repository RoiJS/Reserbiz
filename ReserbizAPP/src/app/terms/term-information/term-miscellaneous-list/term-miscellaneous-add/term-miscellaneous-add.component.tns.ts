import { Component, OnInit, NgZone } from '@angular/core';

import { RouterExtensions, PageRoute } from 'nativescript-angular/router';

import { TranslateService } from '@ngx-translate/core';

import { BaseFormComponent } from '@src/app/shared/component/base-form.component';

import { DialogService } from '@src/app/_services/dialog.service';
import { TermMiscellaneousService } from '@src/app/_services/term-miscellaneous.service';

import { IBaseFormComponent } from '@src/app/_interfaces/ibase-form.component.interface';
import { TermMiscellaneous } from '@src/app/_models/term-miscellaneous.model';
import { TermMiscellaneousFormSource } from '@src/app/_models/term-miscellaneous-form.model';
import { TermMiscellaneousDto } from '@src/app/_dtos/term-miscellaneous.dto';
import { TermMiscellaneousMapper } from '@src/app/_helpers/term-miscellaneous-mapper.helper';

@Component({
  selector: 'ns-term-miscellaneous-add',
  templateUrl: './term-miscellaneous-add.component.html',
  styleUrls: ['./term-miscellaneous-add.component.scss'],
})
export class TermMiscellaneousAddComponent
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
        this._currentFormEntityId = +paramMap.get('termId');
        this._entityFormSource = this._entityDtoMapper.initFormSource();
      });
    });

    this.initDialogTexts();
  }

  initDialogTexts() {
    this._saveNewDialogTexts = {
      title: this.translateService.instant(
        'TERM_MISCELLANEOUS_ADD_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TERM_MISCELLANEOUS_ADD_PAGE.FORM_CONTROL.ADD_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TERM_MISCELLANEOUS_ADD_PAGE.FORM_CONTROL.ADD_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TERM_MISCELLANEOUS_ADD_PAGE.FORM_CONTROL.ADD_DIALOG.ERROR_MESSAGE'
      ),
    };
  }
}

import {
  TranslateService,
  TranslateModule,
  TranslateLoader,
} from '@ngx-translate/core';
import { TestBed, getTestBed } from '@angular/core/testing';

import { HttpClientModule } from '@angular/common/http';

import { Contract } from '@src/app/_models/contract.model';
import { Observable, of } from 'rxjs';
import { Injector } from '@angular/core';

import * as en from '../assets/i18n/en.json';

class JsonTranslationLoader implements TranslateLoader {
  getTranslation(codes: string): Observable<any> {
    return of((<any>en).default);
  }
}

describe('Contract Model Test Suite', () => {
  let translateService: TranslateService;
  let injector: Injector;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
        TranslateModule.forRoot({
          loader: { provide: TranslateLoader, useClass: JsonTranslationLoader },
        }),
      ],
      providers: [TranslateService],
    });

    injector = getTestBed();
    translateService = injector.get(TranslateService);
    translateService.setDefaultLang('en');
  });

  const contractList: Contract[] = [];
  it('should convert duration before contract ends to string format', () => {
    expect('and').toEqual(translateService.instant('GENERAL_TEXTS.AND'));
  });
});

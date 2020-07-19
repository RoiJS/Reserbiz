import {
  TranslateService,
  TranslateModule,
  TranslateLoader,
} from '@ngx-translate/core';
import { TestBed, getTestBed } from '@angular/core/testing';

import {
  BrowserDynamicTestingModule,
  platformBrowserDynamicTesting,
} from '@angular/platform-browser-dynamic/testing';
import { Injector } from '@angular/core';

import { NS_COMPILER_PROVIDERS } from 'nativescript-angular/platform';

import { Observable, of } from 'rxjs';

import { Contract } from '@src/app/_models/contract.model';

import * as en from '../assets/i18n/en.json';

class JsonTranslationLoader implements TranslateLoader {
  getTranslation(codes: string): Observable<any> {
    return of((<any>en).default);
  }
}

describe('Contract Model Test Suite', () => {
  let testContract: Contract;
  let translateService: TranslateService;
  let injector: Injector;

  beforeEach((done) => {
    TestBed.resetTestEnvironment();

    TestBed.initTestEnvironment(
      BrowserDynamicTestingModule,
      platformBrowserDynamicTesting(NS_COMPILER_PROVIDERS)
    );

    TestBed.configureTestingModule({
      imports: [
        TranslateModule.forRoot({
          loader: { provide: TranslateLoader, useClass: JsonTranslationLoader },
        }),
      ],
      providers: [TranslateService],
    });

    TestBed.compileComponents()
      .then(() => done())
      .catch((e) => {
        console.log(`Failed to instantiate test component with error: ${e}`);
        console.log(e.stack);
        done();
      });

    injector = getTestBed();
    translateService = injector.get(TranslateService);
    translateService.setDefaultLang('en');
  });

  it('should convert duration before contract ends to string format when contract.contractDurationBeforeContractEnds contains year, month and day format', () => {
    // Arrange
    testContract = new Contract();
    testContract.code = 'CT-0015';
    testContract.contractDurationBeforeContractEnds = [
      {
        durationValue: 1,
        durationUnitText: 'Year',
      },
      {
        durationValue: 1,
        durationUnitText: 'Month',
      },
      {
        durationValue: 1,
        durationUnitText: 'Day',
      },
    ];

    // Act
    testContract.convertDurationBeforeContractEndsToString(translateService);
    const actualResult = testContract.contractDurationBeforeContractEndsText;

    expect(actualResult).toEqual('1 Year 1 Month and 1 Day');
  });

  it('should convert duration before contract ends to string format when contract.contractDurationBeforeContractEnds contains years, months and days format', () => {
    // Arrange
    testContract = new Contract();
    testContract.code = 'CT-0015';
    testContract.contractDurationBeforeContractEnds = [
      {
        durationValue: 2,
        durationUnitText: 'Year',
      },
      {
        durationValue: 10,
        durationUnitText: 'Month',
      },
      {
        durationValue: 15,
        durationUnitText: 'Day',
      },
    ];

    // Act
    testContract.convertDurationBeforeContractEndsToString(translateService);
    const actualResult = testContract.contractDurationBeforeContractEndsText;

    expect(actualResult).toEqual('2 Years 10 Months and 15 Days');
  });

  it('should convert duration before contract ends to string format when contract.contractDurationBeforeContractEnds contains month and day format', () => {
    // Arrange
    testContract = new Contract();
    testContract.code = 'CT-0015';
    testContract.contractDurationBeforeContractEnds = [
      {
        durationValue: 1,
        durationUnitText: 'Month',
      },
      {
        durationValue: 1,
        durationUnitText: 'Day',
      },
    ];

    // Act
    testContract.convertDurationBeforeContractEndsToString(translateService);
    const actualResult = testContract.contractDurationBeforeContractEndsText;

    expect(actualResult).toEqual('1 Month and 1 Day');
  });

  it('should convert duration before contract ends to string format when contract.contractDurationBeforeContractEnds contains months and days format', () => {
    // Arrange
    testContract = new Contract();
    testContract.code = 'CT-0015';
    testContract.contractDurationBeforeContractEnds = [
      {
        durationValue: 5,
        durationUnitText: 'Month',
      },
      {
        durationValue: 23,
        durationUnitText: 'Day',
      },
    ];

    // Act
    testContract.convertDurationBeforeContractEndsToString(translateService);
    const actualResult = testContract.contractDurationBeforeContractEndsText;

    expect(actualResult).toEqual('5 Months and 23 Days');
  });

  it('should convert duration before contract ends to string format when contract.contractDurationBeforeContractEnds contains day format', () => {
    // Arrange
    testContract = new Contract();
    testContract.code = 'CT-0015';
    testContract.contractDurationBeforeContractEnds = [
      {
        durationValue: 1,
        durationUnitText: 'Day',
      },
    ];

    // Act
    testContract.convertDurationBeforeContractEndsToString(translateService);
    const actualResult = testContract.contractDurationBeforeContractEndsText;

    expect(actualResult).toEqual('1 Day');
  });

  it('should convert duration before contract ends to string format when contract.contractDurationBeforeContractEnds contains days format', () => {
    // Arrange
    testContract = new Contract();
    testContract.code = 'CT-0015';
    testContract.contractDurationBeforeContractEnds = [
      {
        durationValue: 17,
        durationUnitText: 'Day',
      },
    ];

    // Act
    testContract.convertDurationBeforeContractEndsToString(translateService);
    const actualResult = testContract.contractDurationBeforeContractEndsText;

    expect(actualResult).toEqual('17 Days');
  });
});

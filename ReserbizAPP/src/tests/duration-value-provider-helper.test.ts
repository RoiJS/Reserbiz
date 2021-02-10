import * as en from '../assets/i18n/en.json';
import {
  TranslateLoader,
  TranslateService,
  TranslateModule,
} from '@ngx-translate/core';

import { Observable, of } from 'rxjs';
import { Injector } from '@angular/core';
import { TestBed, getTestBed } from '@angular/core/testing';
import {
  BrowserDynamicTestingModule,
  platformBrowserDynamicTesting,
} from '@angular/platform-browser-dynamic/testing';

import { DurationValueProvider } from '@src/app/_helpers/duration-value-provider.helper';

class JsonTranslationLoader implements TranslateLoader {
  getTranslation(codes: string): Observable<any> {
    return of((<any>en).default);
  }
}

describe('Duration Value Provider Helper Test Suite', () => {
  let translateService: TranslateService;
  let injector: Injector;

  beforeEach((done) => {
    TestBed.resetTestEnvironment();

    TestBed.initTestEnvironment(
      BrowserDynamicTestingModule,
      platformBrowserDynamicTesting()
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

  it('should return duration unit name "Day"', () => {
    // Arrange
    const durationValueProvider = new DurationValueProvider(translateService);

    // Act
    const actualResult = durationValueProvider.getDurationName(1, 'Day');

    // Assert
    expect(actualResult).toEqual('Day');
  });

  it('should return duration unit name "Days"', () => {
    // Arrange
    const durationValueProvider = new DurationValueProvider(translateService);

    // Act
    const actualResult = durationValueProvider.getDurationName(2, 'Day');

    // Assert
    expect(actualResult).toEqual('Days');
  });

  it('should return duration unit name "Week"', () => {
    // Arrange
    const durationValueProvider = new DurationValueProvider(translateService);

    // Act
    const actualResult = durationValueProvider.getDurationName(1, 'Week');

    // Assert
    expect(actualResult).toEqual('Week');
  });

  it('should return duration unit name "Weeks"', () => {
    // Arrange
    const durationValueProvider = new DurationValueProvider(translateService);

    // Act
    const actualResult = durationValueProvider.getDurationName(2, 'Week');

    // Assert
    expect(actualResult).toEqual('Weeks');
  });

  it('should return duration unit name "Month"', () => {
    // Arrange
    const durationValueProvider = new DurationValueProvider(translateService);

    // Act
    const actualResult = durationValueProvider.getDurationName(1, 'Month');

    // Assert
    expect(actualResult).toEqual('Month');
  });

  it('should return duration unit name "Months"', () => {
    // Arrange
    const durationValueProvider = new DurationValueProvider(translateService);

    // Act
    const actualResult = durationValueProvider.getDurationName(2, 'Month');

    // Assert
    expect(actualResult).toEqual('Months');
  });

  it('should return duration unit name "Quarter"', () => {
    // Arrange
    const durationValueProvider = new DurationValueProvider(translateService);

    // Act
    const actualResult = durationValueProvider.getDurationName(1, 'Quarter');

    // Assert
    expect(actualResult).toEqual('Quarter');
  });

  it('should return duration unit name "Quarters"', () => {
    // Arrange
    const durationValueProvider = new DurationValueProvider(translateService);

    // Act
    const actualResult = durationValueProvider.getDurationName(2, 'Quarter');

    // Assert
    expect(actualResult).toEqual('Quarters');
  });

  it('should return duration unit name "Year"', () => {
    // Arrange
    const durationValueProvider = new DurationValueProvider(translateService);

    // Act
    const actualResult = durationValueProvider.getDurationName(1, 'Year');

    // Assert
    expect(actualResult).toEqual('Year');
  });

  it('should return duration unit name "Years"', () => {
    // Arrange
    const durationValueProvider = new DurationValueProvider(translateService);

    // Act
    const actualResult = durationValueProvider.getDurationName(2, 'Year');

    // Assert
    expect(actualResult).toEqual('Years');
  });
});

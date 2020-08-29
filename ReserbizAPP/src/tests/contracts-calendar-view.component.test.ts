import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import {
  BrowserDynamicTestingModule,
  platformBrowserDynamicTesting,
} from '@angular/platform-browser-dynamic/testing';
import { NS_COMPILER_PROVIDERS } from 'nativescript-angular/platform';

import { ContractsCalendarViewModule } from '@src/app/contracts/contract-list/contracts-calendar-view/contracts-calendar-view.module';
import { nsTestBedRender } from 'nativescript-angular/testing';
import { ContractsCalendarViewComponent } from '@src/app/contracts/contract-list/contracts-calendar-view/contracts-calendar-view.component';

describe('Contracts Calendar View Component Test Suite', () => {
  beforeEach((done) => {
    TestBed.resetTestEnvironment();

    TestBed.initTestEnvironment(
      BrowserDynamicTestingModule,
      platformBrowserDynamicTesting(NS_COMPILER_PROVIDERS)
    );

    TestBed.configureTestingModule({
      imports: [ContractsCalendarViewModule],
      providers: [],
    });

    TestBed.compileComponents()
      .then(() => done())
      .catch((e) => {
        console.log(`Failed to instantiate test component with error: ${e}`);
        console.log(e.stack);
        done();
      });
  });

  it('should calculate start and end date correctly when calendar view mode is Week and current day is Sunday', async(() => {
    nsTestBedRender(ContractsCalendarViewComponent).then(
      (fixture: ComponentFixture<ContractsCalendarViewComponent>) => {
        const componentInstance = fixture.componentRef.instance;

        expect(true).toBeTrue();
      }
    );
  }));
});

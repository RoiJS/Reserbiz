import {
  Component,
  OnInit,
  ViewChild,
  AfterViewInit,
  OnDestroy,
  ChangeDetectorRef,
} from '@angular/core';

import {
  CalendarViewMode,
  CalendarViewModeChangedEventData,
  CalendarNavigationEventData,
  CalendarEvent,
  RadCalendar,
  CalendarSelectionEventData,
} from 'nativescript-ui-calendar';

import { RadCalendarComponent } from 'nativescript-ui-calendar/angular';
import { BehaviorSubject, Subscription } from 'rxjs';
import { skip } from 'rxjs/operators';

import { ContractService } from '@src/app/_services/contract.service';
import { ContractFilter } from '@src/app/_models/filters/contract-filter.model';
import { ContractCalendarEvent } from '@src/app/_models/contract-calendar-event.model';
import { ContractPaginationList } from '@src/app/_models/pagination_list/contract-pagination-list.model';
import { Contract } from '@src/app/_models/contract.model';

@Component({
  selector: 'ns-contracts-calendar-view',
  templateUrl: './contracts-calendar-view.component.html',
  styleUrls: ['./contracts-calendar-view.component.scss'],
})
export class ContractsCalendarViewComponent
  implements OnInit, OnDestroy, AfterViewInit {
  @ViewChild('contractCalendar', { static: false })
  contractCalendarView: RadCalendarComponent;

  private _viewMode: CalendarViewMode = CalendarViewMode.Month;
  private _contractFilter: ContractFilter;

  private _startDate: Date;
  private _endDate: Date;

  private _loadContractItems = new BehaviorSubject<void>(null);
  private _loadContractItemsSub: Subscription;

  private _contractCalendarItems: Array<CalendarEvent>;
  private _contractEventListItems: Contract[] = [];

  constructor(
    private contractService: ContractService,
    private changeDetectorRef: ChangeDetectorRef
  ) {
    this._contractFilter = new ContractFilter();
  }

  ngOnInit() {
    this._loadContractItemsSub = this._loadContractItems
      .pipe(skip(1))
      .subscribe(() => {
        this.getAllContractItems(() => {
          this.selectEventsByDate(
            this.contractCalendarView.calendar.displayedDate
          );
        });
      });
  }

  ngOnDestroy() {
    if (this._loadContractItemsSub) {
      this._loadContractItemsSub.unsubscribe();
    }
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.calculateStartAndEndDate();
    }, 0);
  }

  changeCalendarViewModeToWeek() {
    this._viewMode = CalendarViewMode.Week;
  }

  changeCalendarViewModeToMonth() {
    this._viewMode = CalendarViewMode.Month;
  }

  changeCalendarViewModeToYear() {
    this._viewMode = CalendarViewMode.MonthNames;
  }

  getAllContractItems(callback: () => void) {
    this.contractService
      .getPaginatedEntities(this._contractFilter)
      .subscribe((contractPaginationList: ContractPaginationList) => {
        const contractItems = contractPaginationList.items;

        this._contractCalendarItems = new Array<CalendarEvent>();
        contractItems.map((contract: Contract) => {
          this._contractCalendarItems.push(new ContractCalendarEvent(contract));
        });

        if (callback) {
          this.changeDetectorRef.detectChanges();
          callback();
        }
      });
  }

  onViewModeChangedEvent(args: CalendarViewModeChangedEventData) {
    const calendar = <RadCalendar>args.object;
    if (calendar.viewMode === CalendarViewMode.Year) {
      return;
    }
    this.calculateStartAndEndDate();
  }

  onNavigatedToDate(args: CalendarNavigationEventData) {
    this.calculateStartAndEndDate();
  }

  onDateSelected(args: CalendarSelectionEventData) {
    const date: Date = args.date;
    this.selectEventsByDate(date);
  }

  selectEventsByDate(date: Date) {
    const events: Array<CalendarEvent> = this.contractCalendarView.calendar.getEventsForDate(
      date
    );

    this._contractEventListItems = events.map(
      (contractEvent: ContractCalendarEvent) => contractEvent.contract
    );
  }

  calculateStartAndEndDate() {
    const calendarView = this.contractCalendarView.calendar;
    const calendarDisplayedDate = calendarView.displayedDate;
    const calendarViewMode = calendarView.viewMode;

    switch (calendarViewMode) {
      case CalendarViewMode.Week:
        this._startDate = new Date(
          calendarDisplayedDate.getFullYear(),
          calendarDisplayedDate.getMonth(),
          calendarDisplayedDate.getDate() - calendarDisplayedDate.getDay()
        );

        this._endDate = new Date(
          calendarDisplayedDate.getFullYear(),
          calendarDisplayedDate.getMonth(),
          this._startDate.getDate() + 6
        );
        break;
      case CalendarViewMode.Month:
        this._startDate = new Date(
          calendarDisplayedDate.getFullYear(),
          calendarDisplayedDate.getMonth(),
          1
        );

        this._endDate = new Date(
          calendarDisplayedDate.getFullYear(),
          calendarDisplayedDate.getMonth() + 1,
          0
        );
        break;
      case CalendarViewMode.Year:
        this._startDate = new Date(calendarDisplayedDate.getFullYear(), 0, 1);

        this._endDate = new Date(calendarDisplayedDate.getFullYear() + 1, 0, 0);
        break;
    }

    // console.log('Start Date: ', this._startDate);
    // console.log('End Date: ', this._endDate);

    this._contractFilter.nextDueDateFromFilter = this._startDate;
    this._contractFilter.nextDueDateToFilter = this._endDate;

    this._loadContractItems.next();
  }

  get viewMode() {
    return this._viewMode;
  }

  get contractCalendarItems(): Array<CalendarEvent> {
    return this._contractCalendarItems;
  }

  get contractEventListItems(): Contract[] {
    return this._contractEventListItems;
  }
}

import { Component, OnInit, Input, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';
import { take } from 'rxjs/operators';

import { TermMiscellaneousService } from '@src/app/_services/term-miscellaneous.service';
import { RouterExtensions } from 'nativescript-angular/router';
import { TermMiscellaneous } from '@src/app/_models/term-miscellaneous.model';
import { EntityFilter } from '@src/app/_models/entity-filter.model';

@Component({
  selector: 'ns-term-miscellaneous-list-panel',
  templateUrl: './term-miscellaneous-list-panel.component.html',
  styleUrls: ['./term-miscellaneous-list-panel.component.scss'],
})
export class TermMiscellaneousListPanelComponent implements OnInit, OnDestroy {
  @Input() termId: number;

  private _termMiscellaneousList: TermMiscellaneous[];
  private _updateTermMiscellaneousListFlag: Subscription;

  constructor(
    private router: RouterExtensions,
    private termMiscellaneousService: TermMiscellaneousService
  ) {}

  ngOnInit() {
    this._updateTermMiscellaneousListFlag = this.termMiscellaneousService.loadTermMiscellaneousListFlag.subscribe(
      () => {
        this.getTermMiscellaneousList();
      }
    );
  }

  ngOnDestroy() {
    if (this._updateTermMiscellaneousListFlag) {
      this._updateTermMiscellaneousListFlag.unsubscribe();
    }
  }

  getTermMiscellaneousList() {
    setTimeout(() => {
      const filter = new EntityFilter();
      filter.parentId = this.termId;

      this.termMiscellaneousService
        .getEntities(filter)
        .pipe(take(1))
        .subscribe((termMiscellaneous: TermMiscellaneous[]) => {
          this._termMiscellaneousList = termMiscellaneous;
        });
    }, 500);
  }

  onNavigateToManageTermMiscellaneousList() {
    this.router.navigate([`/terms/${this.termId}/term-miscellaneous-list`], {
      transition: {
        name: 'slideLeft',
      },
    });
  }

  get termMiscellaneousList(): TermMiscellaneous[] {
    return this._termMiscellaneousList;
  }
}

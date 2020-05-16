import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';

import { Subscription } from 'rxjs';
import { finalize, take } from 'rxjs/operators';

import { TranslateService } from '@ngx-translate/core';

import { ObservableArray } from 'tns-core-modules/data/observable-array/observable-array';
import { isAndroid, View } from 'tns-core-modules/ui/page/page';

import { RadListViewComponent } from 'nativescript-ui-listview/angular/listview-directives';
import {
  ListViewEventData,
  SwipeActionsEventData,
} from 'nativescript-ui-listview';
import { RouterExtensions } from 'nativescript-angular/router';

import { SpaceTypeService } from '@src/app/_services/space-type.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { SpaceType } from '@src/app/_models/space-type.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

@Component({
  selector: 'ns-space-types-list',
  templateUrl: './space-types-list.component.html',
  styleUrls: ['./space-types-list.component.scss'],
})
export class SpaceTypesListComponent implements OnInit, OnDestroy {
  @ViewChild('tenantListView', { static: false })
  spaceTypesListView: RadListViewComponent;

  private _spaceTypeList: ObservableArray<SpaceType>;

  private _multipleSelectionActive = false;

  private _isBusy = false;

  private _currentSpaceType: SpaceType;

  private _currentSpaceTypeDeletable = true;

  private _loadSpaceTypesSub: Subscription;

  private _navigateBackSub: any;

  private _isNotNavigateToOtherPage = true;

  private _spaceTypeListHasLoaded = false;

  constructor(
    private dialogService: DialogService,
    private location: Location,
    private router: RouterExtensions,
    private spaceTypeService: SpaceTypeService,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this._loadSpaceTypesSub = this.spaceTypeService.loadSpaceTypesFlag.subscribe(
      () => {
        this.getSpaceTypes();
      }
    );

    this._navigateBackSub = this.location.subscribe(() => {
      this._isNotNavigateToOtherPage = true;
    });
  }

  ngOnDestroy() {
    if (this._loadSpaceTypesSub) {
      this._loadSpaceTypesSub.unsubscribe();
    }

    if (this._navigateBackSub) {
      this._navigateBackSub.unsubscribe();
    }
  }

  getSpaceTypes(name: string = '', onGetListFinished?: () => void) {
    this._isBusy = true;
    setTimeout(() => {
      this.spaceTypeService
        .getSpaceTypes(name)
        .pipe(
          take(1),
          finalize(() => (this._isBusy = false))
        )
        .subscribe((spaceTypes: SpaceType[]) => {
          this._spaceTypeList = new ObservableArray<SpaceType>(spaceTypes);

          this._multipleSelectionActive = false;

          this._isNotNavigateToOtherPage = true;

          this._spaceTypeListHasLoaded = true;

          if (onGetListFinished) {
            onGetListFinished();
          }
        });
    }, 500);
  }

  activateDeactivateMultipleSelection() {
    if (this._spaceTypeList.length === 0) {
      return;
    }

    this._multipleSelectionActive = !this._multipleSelectionActive;

    if (!this._multipleSelectionActive) {
      this._spaceTypeList.map((spaceType) => {
        spaceType.isSelected = false;
      });

      this.spaceTypesListView.listView.deselectAll();
    }
  }

  deleteSelectedSpaceTypes() {
    const me = this;
    const selectedSpaceTypes = this.spaceTypesListView.listView.getSelectedItems();
    if (selectedSpaceTypes.length > 0) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPES_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPES_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .then((res: ButtonOptions) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            this.spaceTypeService
              .deleteMultipleSpaceTypes(selectedSpaceTypes)
              .pipe(finalize(() => (this._isBusy = false)))
              .subscribe(
                () => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPES_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPES_DIALOG.SUCCESS_MESSAGE'
                    )
                  );

                  me._spaceTypeList = new ObservableArray<SpaceType>([]);
                  me.getSpaceTypes();
                },
                (error: Error) => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPES_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPES_DIALOG.ERROR_MESSAGE'
                    )
                  );
                }
              );
          }
        });
    }
  }

  deleteSelectedSpaceType(event: any) {
    const selectedSpaceTypeIndex = (<any>(
      this.spaceTypesListView.listView
    )).getIndexOf(this._currentSpaceType);

    this.dialogService
      .confirm(
        this.translateService.instant(
          'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPE_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPE_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.spaceTypeService
            .deleteSpaceType(this._currentSpaceType.id)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPE_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPE_DIALOG.SUCCESS_MESSAGE'
                  ),
                  () => {
                    (<any>this._spaceTypeList).splice(
                      selectedSpaceTypeIndex,
                      1
                    );
                  }
                );
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPE_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPE_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }

  goSpaceTypeDetails(args: ListViewEventData) {
    const selectedSpaceType = this._spaceTypeList.getItem(args.index);

    if (!this._multipleSelectionActive) {
      this._isNotNavigateToOtherPage = false;

      setTimeout(() => {
        this.router.navigate(
          [`/space-types/space-type-edit/${selectedSpaceType.id}`],
          {
            transition: {
              name: 'slideLeft',
            },
          }
        );
      }, 100);
    } else {
      // This will prevent the item from being selected
      if (!selectedSpaceType.isDeletable) {
        this.spaceTypesListView.listView.deselectItemAt(args.index);
        return;
      }
      selectedSpaceType.isSelected = true;
    }
  }

  deselectedItem(args: ListViewEventData) {
    const selectedSpaceType = this._spaceTypeList.getItem(args.index);
    selectedSpaceType.isSelected = false;
  }

  goToAddSpaceTypesPage() {
    this.router.navigate(['space-types/space-type-add'], {
      transition: {
        name: 'slideLeft',
      },
    });
  }

  onSwipeCellStarted(args: ListViewEventData) {
    this._currentSpaceType = (<SwipeActionsEventData>args).mainView
      .bindingContext as SpaceType;
    this._currentSpaceTypeDeletable = this._currentSpaceType.isDeletable;
    const swipeLimits = args.data.swipeLimits;
    const swipeView = args['object'];
    const tenantSwipeActions = swipeView.getViewById<View>('swipeActions');
    swipeLimits.right = tenantSwipeActions.getMeasuredWidth();
    swipeLimits.threshold = tenantSwipeActions.getMeasuredWidth();
  }

  onPullToRefreshInitiated(args: ListViewEventData) {
    this.getSpaceTypes('', () => {
      args.object.notifyPullToRefreshFinished();
    });
  }

  searchBarLoaded(args: any) {
    const searchbar = args.object;
    if (isAndroid) {
      searchbar.android.clearFocus();
    }
  }

  onClearSearchText(args: any) {
    if (this._spaceTypeListHasLoaded) {
      args.object.text = '';
      this.getSpaceTypes(args.object.text);
    }
  }

  onSubmitSearchText(args: any) {
    this.getSpaceTypes(args.object.text);
  }

  get isBusy(): boolean {
    return this._isBusy;
  }

  get spaceTypeList(): ObservableArray<SpaceType> {
    return this._spaceTypeList;
  }

  get multipleSelectionActive(): boolean {
    return this._multipleSelectionActive;
  }

  get IsBusy(): boolean {
    return this._isBusy;
  }

  get selectedCount(): string {
    return `${
      this.spaceTypesListView.listView.getSelectedItems().length
    } ${this.translateService.instant(
      'SPACE_TYPE_LIST_PAGE.SELECTED_SPACE_TYPES'
    )}`;
  }

  get isNotNavigateToOtherPage(): boolean {
    return this._isNotNavigateToOtherPage;
  }

  get currentSpaceTypeDeletable(): boolean {
    return this._currentSpaceTypeDeletable;
  }
}

import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  AfterViewInit,
  ChangeDetectorRef,
  ViewContainerRef,
} from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

import { RouterExtensions } from '@nativescript/angular';
import { RadSideDrawerComponent } from 'nativescript-ui-sidedrawer/angular';
import {
  RadSideDrawer,
  DrawerTransitionBase,
  SlideInOnTopTransition,
} from 'nativescript-ui-sidedrawer';
import { Application } from '@nativescript/core';
import { ios } from '@nativescript/core/application';
import { ad } from '@nativescript/core/utils/utils';

import { TranslateService } from '@ngx-translate/core';

import { filter } from 'rxjs/operators';
import { Subscription } from 'rxjs';

import { AuthService } from './_services/auth.service';
import { SideDrawerService } from './_services/side-drawer.service';
import { UIService } from './_services/ui.service';

import { MainMenu } from './_models/main-menu.model';
import { SettingsService } from './_services/settings.service';

@Component({
  selector: 'ns-app',
  moduleId: module.id,
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild(RadSideDrawerComponent, { static: false })
  drawerComponent: RadSideDrawerComponent;

  mainMenuList: MainMenu[];

  private drawer: RadSideDrawer;
  private drawerSub: Subscription;
  private currentFullnameSub: Subscription;
  private currentUsernameSub: Subscription;

  private _currentFullname: string;
  private _currentUsername: string;

  private activatedUrl: string;
  private _sideDrawerTransition: DrawerTransitionBase;

  constructor(
    private authService: AuthService,
    private changeDetectionRef: ChangeDetectorRef,
    private router: Router,
    private routerExtensions: RouterExtensions,
    private settingsService: SettingsService,
    private sideDrawerService: SideDrawerService,
    private uiService: UIService,
    private vcRef: ViewContainerRef,
    private translate: TranslateService
  ) {
    this.translate.setDefaultLang('en');
  }

  ngOnInit(): void {
    (async () => {
      this.currentFullnameSub = this.authService.currentFullname.subscribe(
        (currentFullname: string) => {
          this._currentFullname = currentFullname;
        }
      );

      this.currentUsernameSub = this.authService.currentUsername.subscribe(
        (currentUsername: string) => {
          this._currentUsername = currentUsername;
        }
      );

      this.drawerSub = this.uiService.drawerState.subscribe(() => {
        if (this.drawer) {
          this.drawer.toggleDrawerState();
          this.hideKeyboard();
        }
      });
      this.uiService.setRootVCRef(this.vcRef);

      this.activatedUrl = '/dashboard';
      this.mainMenuList = this.sideDrawerService.mainMenu;
      this._sideDrawerTransition = new SlideInOnTopTransition();

      this.router.events
        .pipe(filter((event: any) => event instanceof NavigationEnd))
        .subscribe(
          (event: NavigationEnd) =>
            (this.activatedUrl = event.urlAfterRedirects)
        );

      await this.settingsService.getSettingsDetails();
    })();
  }

  ngOnDestroy() {
    if (this.drawerSub) {
      this.drawerSub.unsubscribe();
    }

    if (this.currentFullnameSub) {
      this.currentFullnameSub.unsubscribe();
    }

    if (this.currentUsernameSub) {
      this.currentUsernameSub.unsubscribe();
    }
  }

  ngAfterViewInit() {
    this.drawer = this.drawerComponent.sideDrawer;
    this.changeDetectionRef.detectChanges();
  }

  onSignout() {
    this.uiService.toggleDrawer();
    this.authService.logout();
  }

  isComponentSelected(url: string): boolean {
    return this.activatedUrl.indexOf(url) > -1;
  }

  onNavItemTap(navItemRoute: string): void {
    this.routerExtensions.navigate([navItemRoute], {
      transition: {
        name: 'slideLeft',
      },
    });

    const sideDrawer = <RadSideDrawer>(<any>Application.getRootView());
    sideDrawer.closeDrawer();
  }

  hideKeyboard = () => {
    if (ios) {
      ios.nativeApp.sendActionToFromForEvent(
        'resignFirstResponder',
        null,
        null,
        null
      );
    } else {
      ad.dismissSoftInput();
    }
  }

  get currentUserFullname(): string {
    return this._currentFullname;
  }

  get currentUsername(): string {
    return this._currentUsername;
  }

  get sideDrawerTransition(): DrawerTransitionBase {
    return this._sideDrawerTransition;
  }
}

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

import { RouterExtensions } from 'nativescript-angular/router';
import { RadSideDrawerComponent } from 'nativescript-ui-sidedrawer/angular/side-drawer-directives';
import {
  RadSideDrawer,
  DrawerTransitionBase,
  SlideInOnTopTransition,
} from 'nativescript-ui-sidedrawer';
import * as app from 'tns-core-modules/application';
import { TranslateService } from '@ngx-translate/core';

import { filter } from 'rxjs/operators';
import { Subscription } from 'rxjs';

import { AuthService } from './_services/auth.service';
import { SideDrawerService } from './_services/side-drawer.service';
import { UIService } from './_services/ui.service';

import { MainMenu } from './_models/main-menu.model';

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
  private sideDrawerTransition: DrawerTransitionBase;

  constructor(
    private authService: AuthService,
    private changeDetectionRef: ChangeDetectorRef,
    private router: Router,
    private routerExtensions: RouterExtensions,
    private sideDrawerService: SideDrawerService,
    private uiService: UIService,
    private vcRef: ViewContainerRef,
    private translate: TranslateService
  ) {
    this.translate.setDefaultLang('en');
  }

  ngOnInit(): void {
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
      }
    });
    this.uiService.setRootVCRef(this.vcRef);

    this.activatedUrl = '/dashboard';
    this.mainMenuList = this.sideDrawerService.mainMenu;
    this.sideDrawerTransition = new SlideInOnTopTransition();

    this.router.events
      .pipe(filter((event: any) => event instanceof NavigationEnd))
      .subscribe(
        (event: NavigationEnd) => (this.activatedUrl = event.urlAfterRedirects)
      );
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
    return this.activatedUrl === url;
  }

  onNavItemTap(navItemRoute: string): void {
    this.routerExtensions.navigate([navItemRoute], {
      transition: {
        name: 'slideLeft',
      },
    });

    const sideDrawer = <RadSideDrawer>app.getRootView();
    sideDrawer.closeDrawer();
  }

  get currentUserFullname(): string {
    return this._currentFullname;
  }

  get currentUsername(): string {
    return this._currentUsername;
  }
}

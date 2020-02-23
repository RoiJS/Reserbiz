import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  AfterViewInit,
  ChangeDetectorRef,
  ViewContainerRef
} from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

import { RouterExtensions } from 'nativescript-angular/router';
import { RadSideDrawerComponent } from 'nativescript-ui-sidedrawer/angular/side-drawer-directives';
import {
  RadSideDrawer,
  DrawerTransitionBase,
  SlideInOnTopTransition
} from 'nativescript-ui-sidedrawer';
import * as app from 'tns-core-modules/application';

import { filter } from 'rxjs/operators';
import { Subscription } from 'rxjs';

import { AuthService } from './_services/auth.service';
import { SideDrawerService } from './_services/side-drawer.service';
import { UIService } from './_services/ui.service';

import { MainMenu } from './_models/main-menu.model';
import { User } from './_models/user.model';
import { GenderEnum } from './_enum/gender.enum';

@Component({
  selector: 'ns-app',
  moduleId: module.id,
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild(RadSideDrawerComponent, { static: false })
  drawerComponent: RadSideDrawerComponent;

  mainMenuList: MainMenu[];

  private drawer: RadSideDrawer;
  private drawerSub: Subscription;
  private currentSub: Subscription;

  private _currentUser: User = new User('', '', '', '', GenderEnum.Male);

  private activatedUrl: string;
  private sideDrawerTransition: DrawerTransitionBase;

  constructor(
    private authService: AuthService,
    private changeDetectionRef: ChangeDetectorRef,
    private router: Router,
    private routerExtensions: RouterExtensions,
    private sideDrawerService: SideDrawerService,
    private uiService: UIService,
    private vcRef: ViewContainerRef
  ) {}

  ngOnInit(): void {
    this.currentSub = this.authService.user.subscribe((currentUser: User) => {
      if (currentUser) {
        this._currentUser = currentUser;
      }
    });

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

    if (this.currentSub) {
      this.currentSub.unsubscribe();
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
        name: 'fade'
      }
    });

    const sideDrawer = <RadSideDrawer>app.getRootView();
    sideDrawer.closeDrawer();
  }

  get currentUser(): User {
    return this._currentUser;
  }
}

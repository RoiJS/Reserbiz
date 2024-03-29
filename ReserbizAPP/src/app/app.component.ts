import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  AfterViewInit,
  ChangeDetectorRef,
  ViewContainerRef,
  NgZone,
} from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

import {
  connectionType,
  getConnectionType,
  startMonitoring,
  stopMonitoring,
} from '@nativescript/core/connectivity';

import { RouterExtensions } from '@nativescript/angular';
import { RadSideDrawerComponent } from 'nativescript-ui-sidedrawer/angular';
import {
  RadSideDrawer,
  DrawerTransitionBase,
  SlideInOnTopTransition,
} from 'nativescript-ui-sidedrawer';
import { Application, Utils } from '@nativescript/core';

import { TranslateService } from '@ngx-translate/core';

import { SignalrCore } from 'nativescript-signalr-core';

import { delay, filter } from 'rxjs/operators';
import { Subscription } from 'rxjs';

import { AuthService } from './_services/auth.service';
import { AppVersionService } from './_services/app-version.service';
import { CheckConnectionService } from './_services/check-connection.service';
import { DialogService } from './_services/dialog.service';
import { GeneralInformationService } from './_services/general-information.service';
import { PushNotificationService } from './_services/push-notification.service';
import { SideDrawerService } from './_services/side-drawer.service';
import { SettingsService } from './_services/settings.service';
import { UIService } from './_services/ui.service';
import { StorageService } from './_services/storage.service';
import { UserNotificationService } from './_services/user-notification.service';

import { MainMenu } from './_models/main-menu.model';
import { Settings } from './_models/settings.model';

import { environment } from '@src/environments/environment';

// Import Websocket to be able to use SignalR
declare var require;
const WebSocket = require('nativescript-websockets');

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
  private currentBusinessNameSub: Subscription;
  private checkConnectionSub: Subscription;

  private _currentFullname: string;
  private _currentBusinessName: string;

  private activatedUrl: string;
  private _sideDrawerTransition: DrawerTransitionBase;

  private signalrCore: SignalrCore;
  private _userNotificationUnreadCount = 0;

  constructor(
    private authService: AuthService,
    private appVersionService: AppVersionService,
    private checkConnectionService: CheckConnectionService,
    private changeDetectionRef: ChangeDetectorRef,
    private dialogService: DialogService,
    private generalInformationService: GeneralInformationService,
    private pushNotificationService: PushNotificationService,
    private router: Router,
    private routerExtensions: RouterExtensions,
    private settingsService: SettingsService,
    private sideDrawerService: SideDrawerService,
    private storageService: StorageService,
    private uiService: UIService,
    private vcRef: ViewContainerRef,
    private translate: TranslateService,
    private userNotificationService: UserNotificationService,
    private zone: NgZone
  ) {
    this.signalrCore = new SignalrCore();
    this.translate.setDefaultLang('en');
  }

  ngOnInit(): void {
    (async () => {
      this.uiService.setRootVCRef(this.vcRef);
      this.initRadDrawerSubscription();
      this.initRouterEvents();

      this.activatedUrl = '/dashboard';
      this.mainMenuList = this.sideDrawerService.mainMenu;
      this._sideDrawerTransition = new SlideInOnTopTransition();

      this.initUserInfoSubscriptions();
      await this.settingsService.getSettingsDetails();
      await this.setUserNotificationUnreadCount();
    })();

    this.monitorInternetConnectivity();
    this.initMonitorConnectivityRedirection();

    this.connectToSignalRServer();
    this.initPushNotification();
  }

  ngOnDestroy() {
    if (this.drawerSub) {
      this.drawerSub.unsubscribe();
    }

    if (this.currentFullnameSub) {
      this.currentFullnameSub.unsubscribe();
    }

    if (this.currentBusinessNameSub) {
      this.currentBusinessNameSub.unsubscribe();
    }

    if (this.checkConnectionSub) {
      this.checkConnectionSub.unsubscribe();
    }

    stopMonitoring();
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

  onOpenOnlineDocumentation() {
    Utils.openUrl(environment.reserbizOnlineDocumentationURL);
  }

  private connectToSignalRServer() {
    this.initBroadCastSystemUpdateStatus();
    this.initBroadCastValidateLoginAccount();
    this.signalrCore
      .start(`${environment.reserbizAPIEndPointWebsocket}/reserbizMainHub`)
      .then(
        (isConnected: boolean) => {
          console.log('SignalR Connection Status: ', isConnected);
        },
        () => {
          console.error('Error on connecting to ReserbizMainHub.');
        }
      );
  }

  private async setUserNotificationUnreadCount() {
    this.userNotificationService.unreadNotificationCount.subscribe(
      (count: number) => {
        this._userNotificationUnreadCount = count;
      }
    );

    this.userNotificationService.checkNotificationUpdate();
  }

  private initBroadCastSystemUpdateStatus() {
    this.signalrCore.on('BroadCastSystemUpdateStatus', () => {
      this.zone.run(() => {
        const currentConnectionType = getConnectionType();

        this.checkConnectionService.currentConnectionType.next(
          currentConnectionType
        );
      });
    });
  }

  private initBroadCastValidateLoginAccount() {
    this.signalrCore.on(
      'BroadCastValidateLogin',
      (data: { arguments: any }) => {
        this.zone.run(() => {
          const loggedInUserIdFromOtherDevice = parseInt(data.arguments[0]);
          const loggedInUserNameFromOtherDevice = data.arguments[1];
          const currentLoggedInUser = this.authService.user.getValue();
          const userData = this.storageService.getString('userData');
          let currentLoggedInUserId = 0;

          if (currentLoggedInUser) {
            currentLoggedInUserId = this.authService.userId;
          }

          // Check if account is also currently logged in
          // on different device, if so, auto logout the account
          // from the other device.
          if (
            userData &&
            currentLoggedInUser &&
            currentLoggedInUserId === loggedInUserIdFromOtherDevice &&
            currentLoggedInUser.username === loggedInUserNameFromOtherDevice
          ) {
            this.dialogService.alert(
              this.translate.instant(
                'AUTH_PAGE.SIMULTANEOUS_LOGIN_DIALOG.TITLE'
              ),
              this.translate.instant(
                'AUTH_PAGE.SIMULTANEOUS_LOGIN_DIALOG.MESSAGE'
              )
            );
            this.authService.logout();
            this.drawer.closeDrawer();
          }
        });
      }
    );
  }

  private initUserInfoSubscriptions() {
    this.currentFullnameSub = this.authService.currentFullname.subscribe(
      (currentFullname: string) => {
        if (currentFullname) {
          this._currentFullname = currentFullname;
        }
      }
    );

    this.currentBusinessNameSub = this.settingsService.settings.subscribe(
      (settings: Settings) => {
        if (settings) {
          this._currentBusinessName = settings.businessName;
        }
      }
    );
  }

  private initRadDrawerSubscription() {
    this.drawerSub = this.uiService.drawerState.subscribe(() => {
      if (this.drawer) {
        this.drawer.toggleDrawerState();
        this.uiService.hideKeyboard();
      }
    });
  }

  private initRouterEvents() {
    this.router.events
      .pipe(filter((event: any) => event instanceof NavigationEnd))
      .subscribe(
        (event: NavigationEnd) => (this.activatedUrl = event.urlAfterRedirects)
      );
  }

  private monitorInternetConnectivity() {
    const currentConnectionType = getConnectionType();

    this.checkConnectionService.currentConnectionType.next(
      currentConnectionType
    );

    startMonitoring((type) => {
      this.checkConnectionService.currentConnectionType.next(type);
    });
  }

  private initMonitorConnectivityRedirection() {
    this.checkConnectionSub = this.checkConnectionService.currentConnectionType
      .pipe(delay(2000))
      .subscribe((currentConnection: connectionType) => {
        this.zone.run(() => {
          if (currentConnection === connectionType.none) {
            // Auto-logout the user during system update.
            this.authService.logout('/no-connection');
          } else {
            (async () => {
              const generalInformation =
                await this.generalInformationService.getGeneralInformation();

              // Check if the system is currently under system update
              if (generalInformation.systemUpdateStatus) {
                this.authService.logout('/system-update');
              }
            })();
          }
        });
      });
  }

  private initPushNotification() {
    this.pushNotificationService.subscribe();
  }

  get currentUserFullname(): string {
    return this._currentFullname;
  }

  get currentBusinessName(): string {
    return this._currentBusinessName;
  }

  get sideDrawerTransition(): DrawerTransitionBase {
    return this._sideDrawerTransition;
  }

  get copyRightText(): string {
    return this.appVersionService.copyRightText;
  }

  get appVersion(): string {
    return this.appVersionService.appVersion;
  }

  get userNotificationUnreadCount(): string {
    return this._userNotificationUnreadCount > 99
      ? '99+'
      : this._userNotificationUnreadCount.toString();
  }
}

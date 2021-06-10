import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

import { NavigationEnd, Router } from '@angular/router';
import { MatSidenav } from '@angular/material/sidenav';
import { Title } from '@angular/platform-browser';

import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';

import { IMenu } from './_interfaces/imenu';

import { UIService } from './services/ui.service';
import { SidedrawerService } from './services/sidedrawer.service';

import { SharedComponent } from './shared/components/shared/shared.component';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent
  extends SharedComponent
  implements OnInit, OnDestroy, AfterViewInit
{
  @ViewChild(MatSidenav) sideNav: MatSidenav | undefined;

  treeControl = new NestedTreeControl<IMenu>((node) => node.children);
  menuDataSource = new MatTreeNestedDataSource<IMenu>();

  private activatedUrl = '';

  constructor(
    private router: Router,
    private sideDrawerService: SidedrawerService,
    private translateService: TranslateService,
    private title: Title,
    protected uiService: UIService
  ) {
    super(uiService);
    this.menuDataSource.data = this.sideDrawerService.mainMenu;
    this.translateService.setDefaultLang('en');
  }

  ngOnInit(): void {
    this.initRouterEvents();
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  onNavItemTap(name: string, url: string, fragment: string): void {
    this.title.setTitle(this.translateService.instant(name));
    this.router.navigate([url], { fragment });

    if (this.isHandset) {
      this.uiService.toggleDrawer();
    }
  }

  ngAfterViewInit(): void {
    this.uiService.setDrawerRef(this.sideNav);

    super.ngAfterViewInit();
  }

  isPageSelected(url: string, fragment: string = ''): boolean {
    let currentUrl = url;

    if (fragment) {
      currentUrl = `${url}#${fragment}`;
    }

    return this.activatedUrl === currentUrl;
  }

  private initRouterEvents(): void {
    this.router.events
      .pipe(filter((event: any) => event instanceof NavigationEnd))
      .subscribe(
        (event: NavigationEnd) => (this.activatedUrl = event.urlAfterRedirects)
      );
  }

  hasChild = (_: number, node: IMenu) =>
    !!node.children && node.children.length > 0;
}

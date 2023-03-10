import {
  Component,
  HostListener,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import { NavigationEnd, Router, RouterStateSnapshot } from '@angular/router';
import { Store } from '@ngrx/store';
import { TooltipDirective } from '@progress/kendo-angular-tooltip';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { FTSMain } from './base/ftsmain';
import { commonFunction } from './common/commonFunction';
import { EventManager } from './common/eventManager';
import { LocalStorage } from './common/local-storage';
import { ResourceManager } from './common/resource-manager';
import { FtsDialogService } from './controls/fts-dialog/fts-dialog.service';
import { MenuTable } from './controls/navigation/navigation.component';
import { clearTokenAction } from './model/login/action';
import { selectAuthStatus } from './model/login/selector';
import { SystemService } from './model/other/system-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class AppComponent implements OnInit, OnDestroy {
  //#region  property
  @ViewChild(TooltipDirective) tooltipDirective!: TooltipDirective;

  isAuthen: boolean = false;
  loadStarted: boolean = false;

  miniMenu: boolean = false;
  viewControlName: boolean = false;

  showUserMenu: boolean = false;
  clickOutUserMenu: any;

  menuActived?: MenuTable;

  private routerStateSnapshot?: RouterStateSnapshot;
  private onDestroy$: Subject<void> = new Subject<void>();

  //#endregion

  //#region lifecycle
  /**
   * constructor
   */
  constructor(
    private store: Store,
    private router: Router,
    public resourceManager: ResourceManager,
    private localStorage: LocalStorage,
    private eventManager: EventManager,
    private ftsDialogService: FtsDialogService,
    public ftsMain: FTSMain,
    private systemService: SystemService
  ) {
    commonFunction.setDarkMode(localStorage.DarkMode);
    commonFunction.setFontSize(localStorage.FontSize);
  }
  ngOnDestroy(): void {
    this.onDestroy$.next();
  }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    //log c???nh b??o n???u m??? devtools
    this.showWanningShowDevTools();

    //????ng k?? & x??? l?? khi chuy???n trang
    this.subscribeRouterChange();

    //????ng k?? & x??? l?? s??? thay ?????i tr???ng th??i authen
    this.subscribeAuthStateChange();

    this.ftsMain.MenusChange.pipe(takeUntil(this.onDestroy$)).subscribe(() => {
      this.changeMenuActive();
    });
  }

  //#endregion

  //#region method

  /**
   * Show/Hide hi???u ???ng load data app
   * @param isLoading
   */
  showHideLoading(isLoading: boolean) {
    const appLoadingEl = document.getElementById('appLoading');
    if (appLoadingEl) {
      appLoadingEl.hidden = !isLoading;
    }
  }

  /**
   * log c???nh b??o n???u m??? devtools
   */
  showWanningShowDevTools() {
    if (environment.production) {
      console.log(
        '%cD???ng l???i!',
        'color:red;font-size:3.5rem;-webkit-text-stroke: 1px black;font-weight:bold;line-height: 100px;'
      );
      console.log(
        `%c????y l?? m???t t??nh n??ng c???a tr??nh duy???t d??nh cho c??c nh?? ph??t tri???n. N???u ai ???? b???o b???n sao ch??p - d??n n???i dung n??o ???? v??o ????y ????? b???t m???t t??nh n??ng ho???c "hack" t??i kho???n c???a ng?????i kh??c, th?? ???? l?? h??nh vi l???a ?????o v?? s??? khi???n h??? c?? th??? truy c???p v??o t??i kho???n c???a b???n. `,
        'font-size:1rem;line-height: 24x;'
      );
    }
  }

  /**
   * ????ng k?? & x??? l?? s??? ki???n chuy???n trang
   */
  subscribeRouterChange() {
    this.router.events.pipe(takeUntil(this.onDestroy$)).subscribe((e) => {
      if (e instanceof NavigationEnd) {
        if (!this.isAuthen) {
          if (
            !['login', 'page-not-found', '**'].includes(
              this.router.routerState.snapshot.root.firstChild?.routeConfig
                ?.path || ''
            )
          ) {
            this.router.navigateByUrl('/login');
          }
        } else {
          this.routerStateSnapshot = this.router.routerState.snapshot;
          this.changeMenuActive();
        }
      }
    });
  }

  changeMenuActive() {
    if (this.routerStateSnapshot && this.ftsMain.Menus?.length > 0) {
      let menu = this.ftsMain.Menus.find(
        (x) =>
          x.Href.toLowerCase() == this.routerStateSnapshot?.url?.toLowerCase()
      );

      if (menu) {
        this.menuActived = {
          ...menu,
          UrlActive: this.routerStateSnapshot.url,
          PathActive:
            this.routerStateSnapshot.root.firstChild?.routeConfig?.path || '',
          QueryParams: this.routerStateSnapshot.root.queryParams,
        };
      } else {
        this.menuActived = undefined;
      }

      this.setTitle();
    }
  }

  /**
   * load d??? li???u ban ?????u
   * @returns Promise
   */
  loadDataStart(): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      this.showHideLoading(true);
      this.localStorage.LoadedGetStarted = false;
      this.systemService
        .getStart()
        .then((data) => {
          this.localStorage.LoadedGetStarted = true;
          this.ftsMain.SystemVars.systemVars = data.systemVars || [];
          this.ftsMain.SysTables.sysTables = data.sysTable || [];
          this.ftsMain.UserInfo = data.userInfo || {};
          this.ftsMain.Menus = data.menu || [];
          this.ftsMain.WorkingYear = this.ftsMain.UserInfo.WorkingYear;
          this.ftsMain.MainCurrency = <string>(this.ftsMain.SystemVars.GetSystemVars('MAIN_CURRENCY'));
          this.ftsMain.SecondCurrency = <string>(this.ftsMain.SystemVars.GetSystemVars('SECOND_CURRENCY'));
          this.ftsMain.TPSL = Number(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_QTY'));
          this.ftsMain.quantityFormat = "n" + <string>(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_QTY'));
          this.ftsMain.TPDGVND = Number(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_PRICE'));
          this.ftsMain.unitPriceFormat = "n" + <string>(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_PRICE'));
          this.ftsMain.TPDGNTE = Number(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_PRICE_ORIG'));
          this.ftsMain.unitPriceOrigFormat = "n" + <string>(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_PRICE_ORIG'));
          this.ftsMain.TPSTVND = Number(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_AMOUNT'));
          this.ftsMain.amountFormat = "n" + <string>(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_AMOUNT'));
          this.ftsMain.TPSTNTE = Number(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_AMOUNT_ORIG'));
          this.ftsMain.amountOrigFormat = "n" + <string>(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_AMOUNT_ORIG'));
          this.ftsMain.TPSTEXTRA = Number(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_AMOUNT_EXTRA'));
          this.ftsMain.exRateFormat = "n" + <string>(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_EXCHANGE_RATE'));
          this.ftsMain.TPEXRATE = Number(this.ftsMain.SystemVars.GetSystemVars('DECIMAL_EXCHANGE_RATE'));
          this.ftsMain.DayStartOfFirstYear = new Date(this.ftsMain.SystemVars.GetSystemVars('DAY_START_YEAR'));

          this.showHideLoading(false);

          resolve();
        })
        .catch((err) => {
          if (err) {
            this.ftsDialogService.alert.show({
              icon: 'warning',
              minWidth: 300,
              maxWidth: 350,
              content: this.ftsMain.ExceptionManager.processException(err),
            });
          }
          reject(err);
        });
    });
  }

  /**
   * ????ng k?? & x??? l?? s??? thay ?????i tr???ng th??i authen
   */
  subscribeAuthStateChange() {
    //Nh???n gi?? tr??? authen t??? store
    this.store
      .select(selectAuthStatus)
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((state) => {
        this.loadStarted = false;
        this.isAuthen = state;
        //n???u ???? authen load d??? li???u ban ?????u
        if (state) {
          setTimeout(() => {
            this.loadDataStart().then(() => {
              this.loadStarted = true;
            });
          }, 1);
        } else {
          this.showHideLoading(false);
          this.loadStarted = true;
        }
      });
  }

  toggleUserMenu($event: Event): void {
    this.showUserMenu = !this.showUserMenu;
    if (!this.clickOutUserMenu) {
      const fn = function (this: AppComponent, $event: any) {
        if (!$event?.target?.closest('.user-content')) {
          this.showUserMenu = false;
        }
      };
      this.clickOutUserMenu = fn.bind(this);
    }
    document.removeEventListener('click', this.clickOutUserMenu);
    document.addEventListener('click', this.clickOutUserMenu);
  }

  logout($event: Event): void {
    this.ftsDialogService.confirm
      .show({
        icon: 'question',
        content:
          this.resourceManager.CommonResource.MyResource.ConfirmBeforLogout,
      })
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((state) => {
        if (state == 'yes') {
          this.store.dispatch(clearTokenAction());
        }
      });
  }

  @HostListener('window:keydown', ['$event'])
  handleKeyDown(e: KeyboardEvent) {
    if (!e.ctrlKey && e.key == 'F4') {
      this.tooltipDirective?.hide();
      this.viewControlName = !this.viewControlName;
    }
    this.eventManager.EmitKeyDown(e);
  }

  public setTitle() {
    var tagTitle = document.getElementsByTagName('title')?.[0];
    if (tagTitle) {
      tagTitle.text = `${this.resourceManager.CommonResource.MyResource?.[
        this.menuActived?.Id || ''
      ] ||
        this.menuActived?.Name ||
        'Ship qu???c t???'
        }`;
    }
  }
  //#endregion
}

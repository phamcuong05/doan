import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  Renderer2,
} from '@angular/core';
import { NavigationEnd, Router, RouterStateSnapshot } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FTSMain } from 'src/app/base/ftsmain';
import { commonFunction } from 'src/app/common/commonFunction';
import { LocalStorage } from 'src/app/common/local-storage';
import { ResourceManager } from 'src/app/common/resource-manager';
@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
})
export class NavigationComponent implements OnInit, OnDestroy {
  @Input() miniMenu: boolean = false;

  menus!: Array<MenuItem>;
  private _listMenu: Array<MenuTable> = [];
  public get listMenu(): Array<MenuTable> {
    return this._listMenu;
  }
  public set listMenu(v: Array<MenuTable>) {
    this._listMenu = v;
    this.setMenus();
    this.changeMenuActive();
  }

  routerChange: EventEmitter<MenuTable> = new EventEmitter();
  expandChange: EventEmitter<string> = new EventEmitter();

  viewSeting: {
    isShow: boolean;
    fontSize: number;
    darkMode: boolean;
  } = {
    isShow: false,
    fontSize: 12,
    darkMode: false,
  };

  private onDestroy$ = new Subject<void>();

  constructor(
    public resourceManager: ResourceManager,
    private renderer: Renderer2,
    private localStorage: LocalStorage,
    private ftsMain: FTSMain,
    private router: Router
  ) {}

  ngOnInit(): void {
    //#region view setting
    this.viewSeting.fontSize = this.localStorage.FontSize;
    this.viewSeting.darkMode = this.localStorage.DarkMode;
    //#endregion

    this.docClickSubscription = this.renderer.listen(
      'document',
      'click',
      this.onDocumentClick.bind(this)
    );

    //load menu
    this.listMenu = this.ftsMain.Menus;
    this.ftsMain.MenusChange.pipe(takeUntil(this.onDestroy$)).subscribe(
      (menuConfigs) => {
        this.listMenu = menuConfigs;
      }
    );

    this.router.events.pipe(takeUntil(this.onDestroy$)).subscribe((e) => {
      if (e instanceof NavigationEnd) {
        this.routerStateSnapshot = this.router.routerState.snapshot;
        this.changeMenuActive();
      }
    });
  }

  private routerStateSnapshot?: RouterStateSnapshot;
  changeMenuActive() {
    if (this.routerStateSnapshot && this.listMenu?.length > 0) {
      let menu = this.listMenu.find(
        (x) =>
          x.Href.toLowerCase() == this.routerStateSnapshot?.url?.toLowerCase()
      );

      if (menu) {
        menu = {
          ...menu,
          UrlActive: this.routerStateSnapshot.url,
          PathActive:
            this.routerStateSnapshot.root.firstChild?.routeConfig?.path || '',
          QueryParams: this.routerStateSnapshot.root.queryParams,
        };
        this.routerChange.emit(menu);
        this.changeMenuId.emit(menu.Id);
      }
    }
  }

  ngOnDestroy(): void {
    if (this.docClickSubscription) this.docClickSubscription();
  }

  setMenus() {
    let menuConfigs = [...this.listMenu];
    this.menus = [];
    menuConfigs = menuConfigs.sort((x, y) => {
      if (x.GroupId != y.GroupId) {
        return (x.GroupOrder || 0) - (y.GroupOrder || 0);
      }

      if (x.GroupId2 != y.GroupId2) {
        return (x.Group2Order || 0) - (y.Group2Order || 0);
      }

      return x.Order - y.Order;
    });

    let group1: MenuItem;
    let group2: MenuItem;
    menuConfigs.forEach((item) => {
      let queryParams: any = {};
      let href = item.Href;
      if (item.Href && item.Href.includes('?')) {
        queryParams = commonFunction.parseQueryString(item.Href);
        href = item.Href.slice(0, item.Href.indexOf('?'));
      }

      let menu: MenuItem = new MenuItem(this.routerChange, this.expandChange);
      let menu1: MenuItem = new MenuItem(this.routerChange, this.expandChange);
      let menu2: MenuItem = new MenuItem(this.routerChange, this.expandChange);
      if (!item.GroupId) {
        menu.Id = item.Id;
        menu.Name = item.Name;
        menu.IconCls = item.IconCls;
        menu.Href = href;
        menu.MapPath = item.MapPath?.split(',');
        menu.QueryParams = queryParams;
        this.menus.push(menu);
      } else {
        if (!group1 || group1.Id != item.GroupId) {
          menu.Id = item.GroupId;
          menu.Name = item.GroupName;
          menu.IconCls = item.GroupIconCls;
          menu.IsGroup = true;
          menu.ExpandType = item.ExpandType;
          menu.MapPath = item.MapPath?.split(',');
          menu.Href = href;
          menu.QueryParams = queryParams;
          group1 = menu;
          this.menus.push(group1);
        }

        if (!item.GroupId2) {
          menu1.Id = item.Id;
          menu1.Name = item.Name;
          menu1.IconCls = item.IconCls;
          menu1.Href = href;
          menu1.MapPath = item.MapPath?.split(',');
          menu1.Level = 2;
          menu1.QueryParams = queryParams;
          group1.Items.push(menu1);
          group1.MapPath = group1.MapPath.concat(item.MapPath);
        } else {
          if (!group2 || group2.Id != item.GroupId2) {
            menu1.Id = item.GroupId2;
            menu1.Name = item.GroupName2;
            menu1.IconCls = item.Group2IconCls;
            menu1.IsGroup = true;
            menu1.ExpandType = item.ExpandType;
            menu1.MapPath = item.MapPath?.split(',');
            menu1.ExpandByRouter = false;
            menu1.Expand = true;
            menu1.Href = href;
            menu1.QueryParams = queryParams;
            group2 = menu1;
            group2.Level = 2;
            group1.Items.push(group2);
          }

          menu2.Id = item.Id;
          menu2.Name = item.Name;
          menu2.IconCls = item.IconCls;
          menu2.Href = href;
          menu2.MapPath = menu2.MapPath.concat(item.MapPath);
          menu2.Level = 3;
          menu2.QueryParams = queryParams;
          group1.MapPath = group1.MapPath.concat(item.MapPath);
          group2.Items.push(menu2);
        }
      }
    });
  }

  private docClickSubscription: any;
  private onDocumentClick(e: any): void {
    if (!e.target.closest('.main-left')) {
      this.expandChange.emit(undefined);
    }

    if (
      !e.target.closest('.popup-view-settings') &&
      !e.target.closest('.box-view-setting .btn')
    ) {
      this.viewSeting.isShow = false;
    }
  }

  menu_Click(menu: MenuItem) {
    if (!menu.IsGroup) {
      this.expandChange.emit(menu.Id);
    }
  }

  menuGroup_click(menu: MenuItem) {
    if (menu?.ExpandType == 'popup' && !menu?.Expand) {
      const txtSearch = document.getElementById(`txtSearchGroup_${menu.Id}`);
      if (txtSearch) {
        setTimeout(() => {
          txtSearch.focus();
        }, 100);
      }
    }
    this.expandChange.emit(menu.Id);
  }

  darkModeChange(value: boolean) {
    this.localStorage.DarkMode = value;
    commonFunction.setDarkMode(value);
  }

  fontSizeChange(value: any) {
    debugger
    this.localStorage.FontSize = value;
    commonFunction.setFontSize(value);
  }
  @Output() changeMenuId: EventEmitter<string> = new EventEmitter();

  txtSearchMenu_Keyup(e: Event, menus: MenuItem[]) {
    if (e.target) {
      const value = commonFunction
        .removeVietnameseTones((e.target as any).value || '')
        .toLowerCase()
        .trim();
      for (let i = 0; i < menus.length; i++) {
        const item = menus[i];
        let title = '';

        item.IsShow = false;
        item.Expand = true;
        if (item.Items && item.Items.length > 0) {
          for (let j = 0; j < item.Items.length; j++) {
            const itemChil = item.Items[j];
            title = commonFunction
              .removeVietnameseTones(
                this.resourceManager.CommonResource.MyResource?.[itemChil.Id] ||
                  itemChil.Name
              )
              .toLowerCase();
            itemChil.IsShow = false;
            if (title.indexOf(value) >= 0) {
              itemChil.IsShow = true;
              item.IsShow = true;
            }
          }
        }
      }
    }
  }
}

export interface MenuTable {
  Id: string;
  Name: string;
  IconCls: string;
  Href: string;
  MapPath: string;
  Order: number | any;
  GroupId: string | any;
  GroupName: string;
  GroupIconCls: string;
  GroupOrder: number | any;
  GroupId2: string | any;
  GroupName2: string;
  Group2IconCls: string;
  Group2Order: number | any;
  ExpandType: 'down' | 'popup';
  QueryParams: any;
  UrlActive: string;
  PathActive: string;
}

export class MenuItem {
  Id!: string;
  Name!: string;
  IconCls!: string;
  Href!: string;
  MapPath: string[] = [];
  IsShow: boolean = true;
  Actived: boolean = false;
  Expand: boolean = false;
  IsGroup: boolean = false;
  Items: MenuItem[] = [];
  Level: number = 1;
  QueryParams: any = {};
  ExpandType: 'down' | 'popup' = 'popup';

  ExpandByRouter: boolean = true;
  _EventRouterChange!: EventEmitter<MenuTable>;
  _EventExpandChange!: EventEmitter<string>;
  constructor(v: EventEmitter<MenuTable>, epchange: EventEmitter<string>) {
    this._EventRouterChange = v;
    this._EventExpandChange = epchange;

    this._EventRouterChange?.subscribe((menu) => {
      if (
        this.MapPath.includes(menu.PathActive) &&
        ((this.IsGroup &&
          (this.Id == menu.GroupId || this.Id == menu.GroupId2)) ||
          this.Id == menu.Id)
      ) {
        this.Actived = true;
        if (this.ExpandByRouter) {
          this.Expand = this.IsGroup && this.ExpandType == 'down';
        }
      } else {
        this.Actived = false;
        if (this.ExpandByRouter) {
          this.Expand = false;
        }
      }
    });

    this._EventExpandChange?.subscribe((id) => {
      if (this.IsGroup) {
        if (id) {
          if (this.Id == id) {
            this.Expand = !this.Expand;
          } else {
            if (this.ExpandByRouter) {
              this.Expand = false;
            }
          }
        } else {
          if (this.Level == 1 && this.ExpandType == 'popup') {
            this.Expand = false;
          }
        }
      }
    });
  }
}

import {
  Component,
  ElementRef,
  EventEmitter,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewContainerRef,
  ViewEncapsulation,
} from '@angular/core';
import { WindowRef } from '@progress/kendo-angular-dialog';
import {
  DataStateChangeEvent,
  GridDataResult,
} from '@progress/kendo-angular-grid';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FTSMain } from 'src/app/base/ftsmain';
import { commonFunction } from 'src/app/common/commonFunction';
import { EventManager } from 'src/app/common/eventManager';
import { ResourceManager } from 'src/app/common/resource-manager';
import { BaseService } from 'src/app/model/base/BaseService';
import { Filter, FilterGroup } from 'src/app/model/base/paging/filter';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { FtsDialogService } from '../fts-dialog/fts-dialog.service';
import { FtsGridComponent } from '../fts-grid/fts-grid.component';
import { MaskLoadService } from '../mask-load/mask-load.service';

@Component({
  template: '',
  encapsulation: ViewEncapsulation.None,
})
export abstract class FtsTextLookupSelectorBase implements OnInit, OnDestroy {
  @ViewChild(FtsGridComponent) grid!: FtsGridComponent;
  @ViewChild('txtSearch') txtSearchEl!: ElementRef;
  windowRef!: WindowRef;
  gridData: GridDataResult = {
    data: [],
    total: 0,
  };
  itemSelected: any;
  id = commonFunction.newGuid();
  /**Số ký tự tối thiểu bắt đầu tìm kiếm */
  characterLimitBySearch: number = 3;
  /**Load Data khi mở Selector */
  autoLoadData: boolean = false;
  /**Các field áp dụng tìm kiếm*/
  abstract searchFields: string[];

  abstract tableName: string;

  public onDestroy$ = new Subject<void>();
  public onDestroyInitWindow$ = new Subject<void>();

  constructor(
    public resourceManager: ResourceManager,
    public myService: BaseService<any>,
    public maskLoad: MaskLoadService,
    public viewContainerRef: ViewContainerRef,
    public el: ElementRef,
    public ftsDialogService: FtsDialogService,
    public eventManager: EventManager,
    public ftsMain: FTSMain
  ) {}
  ngOnInit(): void {}

  abstract get formTitle(): string;

  /**
   * mask load
   */
  mask(show: boolean): void {
    if (show) {
      this.maskLoad.showInEl(this.viewContainerRef, this.el.nativeElement);
    } else this.maskLoad.hide();
  }

  initWindow() {
    this.onDestroyInitWindow$.next();
    this.windowRef.window.instance.cssClass = 'hide-maximize hide-minimize';
    this.windowRef.window.instance.resizable = false;
    this.windowRef.window.instance.title = `${this.resourceManager.CommonResource.MyResource.Select} - ${this.formTitle}`;
    if (this.txtSearchEl) {
      setTimeout(() => {
        this.txtSearchEl.nativeElement.focus();
        this.txtSearchEl.nativeElement.select();
      }, 100);
    }

    this.grid?.toolbarActionEvent
      .pipe(takeUntil(this.onDestroyInitWindow$))
      .subscribe((state) => {
        if (state.action == 'REFRESH') {
          if (this.grid) {
            this.grid.filter = {
              filters: [],
              logic: 'and',
            };
          }
        }
      });

    this.grid?.dataState_Change
      .pipe(takeUntil(this.onDestroyInitWindow$))
      .subscribe((state) => {
        this.loadData(state);
      });

    if (this.tableName) {
      const tb = this.ftsMain.SysTables.sysTables.find(
        (x) => x.TABLE_NAME?.toLowerCase() == this.tableName.toLowerCase()
      );

      if (tb) {
        this.autoLoadData = tb.LOAD_BY_SEARCH;
      }
    }

    //Load data khi txtSearchEl có giá trị
    if (this.txtSearchEl?.nativeElement?.value) {
      this.loadData();
    } else {
      if (!this.gridData?.data?.length) {
        if (this.autoLoadData) {
          this.loadData();
        }
      }
    }
    this.handleKeyDown();
  }

  setParamBeforLoad(param: PagingParam): PagingParam {
    return param;
  }

  loadData(state?: DataStateChangeEvent) {
    let param: PagingParam = {
      PageIndex: 1,
      FilterGroups: [],
      Sorts: undefined,
      PageSize: this.ftsMain.PageSize,
      FilterFields: undefined,
      SumaryFields: undefined,
      TranId: '',
    };

    if (this.searchFields?.length > 0) {
      let filters: Filter[] = [];
      this.searchFields.forEach((field) => {
        filters.push({
          Field: field,
          Operator: 'contains',
          Value: this.txtSearchEl.nativeElement.value,
        });
      });

      param.FilterGroups?.push({
        Logic: 'or',
        Filters: filters,
      });
    }

    if (state) {
      if (state.filter?.filters) {
        let filterGroup: FilterGroup = {
          Filters: [],
          Logic: state.filter.logic,
        };
        state.filter?.filters.forEach((item: any) => {
          filterGroup.Filters.push({
            Field: item.field,
            Operator: item.operator,
            Value: item.value,
          });
        });
        param.FilterGroups?.push(filterGroup);
      }

      if (state.sort?.length) {
        param.Sorts = [
          {
            Field: state.sort[0].field,
            Dir: state.sort[0].dir || 'asc',
          },
        ];
      }

      param.PageIndex = state.skip / state.take + 1;
      param.PageSize = state.take;
    }

    param = this.setParamBeforLoad(param);

    this.mask(true);
    this.myService
      .loadDataPaging(param)
      .then((respon) => {
        if (respon) {
          this.gridData = {
            data: respon.Data || [],
            total: respon.RecordCount || 0,
          };
        }
      })
      .catch((err) => {
        this.ftsDialogService.alert.show({
          icon: 'warning',
          maxWidth: 300,
          content: this.ftsMain.ExceptionManager.processException(err),
        });
      })
      .finally(() => {
        this.mask(false);
      });
  }

  public onSelectionChange(item: any) {
    this.itemSelected = item;
  }

  public dblClickRow() {
    this.selectItem.emit(this.itemSelected);
  }

  selectItem: EventEmitter<any> = new EventEmitter();

  ngAfterViewInit(): void {}

  ngOnDestroy(): void {
    this.onDestroy$.next();
    this.onDestroyInitWindow$.next();
  }

  unHandleKeyDown() {
    this.eventManager.UnSubcriberKeyDown(this.id);
  }

  /**
   * Đăng ký event key up trên window
   */
  handleKeyDown() {
    const that = this;
    that.eventManager.SubcriberKeyDown(that.id, (e: KeyboardEvent) => {
      const strKey = e.key.toUpperCase();
      let stopEvt = false;

      //esc
      if (strKey == 'ESCAPE') {
        this.windowRef.close();
        stopEvt = true;
      }

      //
      if (stopEvt) {
        e.preventDefault();
      }
    });
  }

  txtKeySearch_KeyDown(e: KeyboardEvent) {
    if (e.keyCode == 8) {
      e.target?.dispatchEvent(new Event('keypress'));
    }
  }

  searchTimeout!: any;
  txtKeySearch_KeyPress(e: KeyboardEvent) {
    const that = this;
    if (this.searchTimeout) {
      clearTimeout(this.searchTimeout);
    }

    if (e.keyCode === 13) {
      that.loadData();
    } else {
      const that = this;
      this.searchTimeout = setTimeout(function () {
        if (
          that.txtSearchEl &&
          that.txtSearchEl.nativeElement &&
          (!that.txtSearchEl.nativeElement.value ||
            that.txtSearchEl.nativeElement.value.length >=
              that.ftsMain.SystemVars.GetSystemVars(
                'CHARACTER_LIMIT_BY_SEARCH'
              ))
        ) {
          that.loadData();
        }
      }, 1000);
    }
  }
}

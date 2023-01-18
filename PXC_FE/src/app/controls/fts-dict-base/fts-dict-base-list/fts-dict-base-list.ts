import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { Subject, Subscription } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { commonFunction } from 'src/app/common/commonFunction';
import { EnumLoadingState } from 'src/app/common/enum';
import { ActionType } from 'src/app/common/types';
import { BaseService } from 'src/app/model/base/BaseService';
import { FilterGroup } from 'src/app/model/base/paging/filter';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { FtsColumn } from '../../fts-grid/fts-grid.component';
import { FTSDictBaseDetail } from '../fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseListInject } from './fts-dict-base-list-inject';
import { FtsDictBaseListComponent } from './fts-dict-base-list.component';

/**
 * Code xử lý chung trên DictBaseList
 * Không để chung trong base component để không bị gắn vào component
 * Các trang khi dùng base cần kế thừa class này
 * Created by: MTLUC - 02/11/2021
 */
@Component({
  template: '',
})
export abstract class FtsDictBaseList implements OnInit, OnDestroy {
  @ViewChild(FtsDictBaseListComponent)
  dictBaseListcomponent!: FtsDictBaseListComponent;
  constructor(
    public myService: BaseService<any>,
    public myInject: FtsDictBaseListInject
  ) {}

  /**
   * Cấu hình danh sách cột trên grid
   */
  abstract columns: Array<FtsColumn>;
  abstract idField: string;
  abstract nameField: string;
  abstract tableName: string;

  public autoLoadData: boolean = true;

  ftsDictBaseDetailComponent!: FTSDictBaseDetail;

  id: string = commonFunction.newGuid();

  public onDestroy$ = new Subject<void>();

  ngOnInit() {
    this.handleKeyDown();
  }
  ngAfterViewInit(): void {
    this.loadDm();
    if (this.autoLoadData) {
      this.loadData();
    }
    this.myInject.dictBaseListStore.loadingState$
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((loadingState) => {
        if (loadingState == EnumLoadingState.Loading) this.mask(true);
        else this.mask(false);
      });
    this.myInject.dictBaseListStore.setColumns(this.columns);

    //Bắt các action
    this.myInject.dictBaseListStore.actionClick
      .pipe(takeUntil(this.onDestroy$))
      .subscribe(([actionType, loadingState]) => {
        if (loadingState == EnumLoadingState.Loading) {
          switch (actionType) {
            case 'VIEW':
              this.view();
              break;
            case 'ADD':
              this.add();
              break;
            case 'EDIT':
              this.edit();
              break;
            case 'DELETE':
              this.delete();
              break;
            case 'REFRESH':
              this.refresh();
              break;
            case 'IMPORT_EXCEL':
              this.importExcel();
              break;
            case 'EXPORT_EXCEL':
              this.exportExcel();
              break;
            default:
              this.myInject.ftsDialogService.alert.show({
                icon: 'warning',
                content:
                  this.myInject.resourceManager.CommonResource.MyResource
                    .MessageFunctionUnderDevelop,
              });
              break;
          }
        }
      });

    this.handleBaseDetailResult();

    this.myInject.dictBaseListStore.dataStateChange
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((state) => {
        this.loadData(state);
      });
  }
  ngOnDestroy(): void {
    this.myInject.eventManager.UnSubcriberKeyDown(this.id);
    this.onDestroy$.next();
  }

  toolbarActionHandler($event: { action: ActionType; record: any }) {
    this.myInject.dictBaseListStore.actionClick.emit([
      $event.action,
      EnumLoadingState.Loading,
    ]);
  }

  handleKeyDown() {
    const that = this;
    that.myInject.eventManager.SubcriberKeyDown(that.id, (e: KeyboardEvent) => {
      const strKey = e.key.toUpperCase();
      let stopEvt = false;
      let currentRow = {};
      this.myInject.dictBaseListStore.currentRow$
        .pipe(take(1))
        .subscribe((_currentRow) => {
          currentRow = _currentRow;
        });

      //tổ hợp phím ctr
      if (e.ctrlKey) {
        switch (strKey) {
          case 'I':
            if (this.dictBaseListcomponent.showToolbarAdd) {
              this.toolbarActionHandler({ action: 'ADD', record: currentRow });
            }
            stopEvt = true;
            break;
          case 'E':
            if (this.dictBaseListcomponent.showToolbarEdit) {
              this.toolbarActionHandler({ action: 'EDIT', record: currentRow });
            }
            stopEvt = true;
            break;
          case 'D':
            if (this.dictBaseListcomponent.showToolbarDelete) {
              this.toolbarActionHandler({
                action: 'DELETE',
                record: currentRow,
              });
            }
            stopEvt = true;
            break;
        }
      }

      if (stopEvt) {
        e.preventDefault();
      }
    });
  }

  /**
   * hiệu ứng loading
   * @param show
   */
  mask(show: boolean) {
    if (show) this.myInject.maskLoad.show(this.myInject.viewContainerRef);
    else this.myInject.maskLoad.hide();
  }

  /**
   * Lấy currentRow hiện tại
   */
  public getCurrentRow(): any {
    let currentRow = null;
    this.myInject.dictBaseListStore.currentRow$
      .pipe(take(1))
      .subscribe((_currentRow) => {
        currentRow = _currentRow;
      });
    return currentRow;
  }

  /**
   * Clear sort
   */
  clearSort(): void {
    this.myInject.dictBaseListStore.setSort([]);
  }

  clearFilters(): void {
    this.myInject.dictBaseListStore.setFilter({
      logic: 'and',
      filters: [],
    });
  }

  setParamBeforLoad(param: PagingParam): PagingParam {
    return param;
  }

  /**
   * load dữ liệu
   */
  loadData(state?: DataStateChangeEvent): void {
    let param: PagingParam = {
      PageIndex: 1,
      FilterGroups: [],
      Sorts: undefined,
      PageSize: this.myInject.FTSMain.PageSize,
      FilterFields: undefined,
      SumaryFields: undefined,
      TranId: '',
    };

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

    this.myInject.dictBaseListStore.setLoadingState(EnumLoadingState.Loading);
    this.myService
      .loadDataPaging(param)
      .then((respon) => {
        this.myInject.dictBaseListStore.loadDataComplete({
          gridData: { data: respon.Data || [], total: respon.RecordCount || 0 },
          error: undefined,
        });
      })
      .catch((err) => {
        this.myInject.ftsDialogService.alert.show({
          icon: 'warning',
          maxWidth: 300,
          content: this.myInject.FTSMain.ExceptionManager.processException(err),
        });

        this.myInject.dictBaseListStore.loadDataComplete({
          gridData: { data: [], total: 0 },
          error: err,
        });
      })
      .finally(() => {
        this.myInject.dictBaseListStore.setLoadingState(EnumLoadingState.Init);
      });
  }

  /**
   * refresh
   */
  refresh(): void {
    this.clearSort();
    this.clearFilters();
    this.myInject.dictBaseListStore.setCurrentRow(undefined);
  }
  /**
   * Load danh mục
   */
  loadDm(): void {}

  /**
   * View item
   */
  view() {
    let currentRow: any = this.getCurrentRow();
    if (currentRow && currentRow[this.idField]) {
      this.openEditor('VIEW');
    }
  }

  /**
   * Xóa bản ghi
   */
  delete(): void {
    let _currentRow: any = this.getCurrentRow();

    if (_currentRow?.[this.idField]) {
      this.myInject.ftsDialogService.confirm
        .show({
          icon: 'question',
          content: commonFunction.stringFormat(
            this.myInject.resourceManager.CommonResource.MyResource
              .ConfirmBeforDelete,
            `${_currentRow[this.nameField]} - ${_currentRow[this.idField]}`
          ),
        })
        .pipe(take(1))
        .subscribe((state) => {
          if (state == 'yes') {
            this.myInject.dictBaseListStore.setLoadingState(
              EnumLoadingState.Loading
            );
            this.myService
              .Delete(_currentRow[this.idField])
              .then(() => {
                this.myInject.dictBaseListStore.setLoadingState(
                  EnumLoadingState.Complete
                );

                this.myInject.dictBaseListStore.gridData$
                  .pipe(take(1))
                  .subscribe((gridData) => {
                    this.myInject.dictBaseListStore.setGridData({
                      data: gridData.data.filter(
                        (x) => x[this.idField] != _currentRow[this.idField]
                      ),
                      total: gridData.total - 1,
                    });
                  });

                this.myInject.dictBaseListStore.setCurrentRow(undefined);
                this.dictBaseListcomponent.grdListing.itemSelected = undefined;

                this.myInject.notificationService.show({
                  content:
                    this.myInject.resourceManager.CommonResource.MyResource
                      .DeleteSuccess,
                  hideAfter: 1000,
                  position: { horizontal: 'right', vertical: 'bottom' },
                  animation: { type: 'fade', duration: 400 },
                  type: { style: 'success', icon: true },
                });
              })
              .catch((err: HttpErrorResponse) => {
                this.myInject.dictBaseListStore.setLoadingState(
                  EnumLoadingState.Complete
                );
                this.myInject.ftsDialogService.alert.show({
                  content:
                    this.myInject.FTSMain.ExceptionManager.processException(
                      err
                    ),
                  icon: 'warning',
                  maxWidth: 300,
                });
              });
          }
        });
    }
  }

  /**
   * Nhấn sửa
   */
  edit() {
    this.openEditor('EDIT');
  }

  /**
   * Nhấn thêm
   */
  add() {
    this.openEditor('ADD');
  }

  actionResultSubscription!: Subscription;
  /**
   * Xử lý khi detail thêm/sửa/xóa thành công
   */
  handleBaseDetailResult() {
    const listComponent: FtsDictBaseListComponent = this.dictBaseListcomponent;
    if (listComponent?.dictBaseDetailDirective) {
      this.ftsDictBaseDetailComponent =
        listComponent.dictBaseDetailDirective.component;

      this.actionResultSubscription?.unsubscribe();
      //subscribe nếu thêm sửa xóa thành công thì load lại list
      this.actionResultSubscription =
        this.ftsDictBaseDetailComponent.myInject.detailStore.actionResult
          .pipe(takeUntil(this.onDestroy$))
          .subscribe((state) => {
            if (state.success) {
              if (state.actionType == 'EDIT') {
                const currentRow = { ...this.getCurrentRow() };
                for (const key in currentRow) {
                  if (Object.prototype.hasOwnProperty.call(currentRow, key)) {
                    currentRow[key] = state.data[key];
                  }
                }
                Object.assign(this.getCurrentRow(), currentRow);
              } else if (state.actionType == 'ADD') {
                this.myInject.dictBaseListStore.gridData$
                  .pipe(take(1))
                  .subscribe((gridData) => {
                    this.myInject.dictBaseListStore.setGridData({
                      data: [...gridData.data, state.data],
                      total: gridData.total + 1,
                    });
                  });
                this.myInject.dictBaseListStore.setCurrentRow(state.data);
                this.dictBaseListcomponent.grdListing.itemSelected = state.data;
              } else if (state.actionType == 'DELETE') {
                this.myInject.dictBaseListStore.gridData$
                  .pipe(take(1))
                  .subscribe((gridData) => {
                    this.myInject.dictBaseListStore.setGridData({
                      data: gridData.data.filter(
                        (x) => x[this.idField] != state.data[this.idField]
                      ),
                      total: gridData.total - 1,
                    });
                  });
                this.myInject.dictBaseListStore.setCurrentRow(undefined);
                this.dictBaseListcomponent.grdListing.itemSelected = undefined;
              }
            }
          });
    }
  }

  /**
   * Show popup editor
   * @param actionType Actiontype
   */
  openEditor(actionType: ActionType) {
    let _currentRow = undefined;
    if (actionType != 'ADD') {
      _currentRow = { ...this.getCurrentRow() };
    }

    if (_currentRow || actionType == 'ADD') {
      if (this.ftsDictBaseDetailComponent) {
        this.ftsDictBaseDetailComponent?.open(_currentRow || {}, actionType);
      } else {
        this.myInject.ftsDialogService.alert.show({
          icon: 'warning',
          content: 'dictBaseDetail Directive not exists!',
        });
      }
    }
  }

  /**
   * Export excel
   */
  exportExcel() {
    this.myInject.ftsDialogService.confirm
      .show({
        icon: 'question',
        content: commonFunction.stringFormat(
          this.myInject.resourceManager.CommonResource.MyResource
            .ConfirmBeforExportExcel
        ),
      })
      .pipe(take(1))
      .subscribe((state) => {
        if (state == 'yes') {
        }
      });
  }

  importedSubscription!: Subscription;
  /**
   * Import excel
   */
  importExcel() {
    if (this.dictBaseListcomponent.importExcel && this.tableName.length > 0) {
      this.dictBaseListcomponent.importExcel.service = this.myService;
      this.dictBaseListcomponent.importExcel.tableName = this.tableName;
      this.dictBaseListcomponent.importExcel.open();
      this.importedSubscription?.unsubscribe();
      this.importedSubscription =
        this.dictBaseListcomponent.importExcel.importedEvent
          .pipe(takeUntil(this.onDestroy$))
          .subscribe((x) => {
            if (x) {
              this.loadData();
            }
          });
    } else {
      this.myInject.ftsDialogService.alert.show({
        icon: 'warning',
        content:
          this.myInject.resourceManager.CommonResource.MyResource
            .MessageFunctionUnderDevelop,
      });
    }
  }
}

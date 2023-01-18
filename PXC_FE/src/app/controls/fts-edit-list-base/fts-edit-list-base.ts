import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';
import {
  ColumnComponent,
  DataStateChangeEvent,
} from '@progress/kendo-angular-grid';
import { TooltipDirective } from '@progress/kendo-angular-tooltip';
import { Subject, Subscription } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { commonFunction } from 'src/app/common/commonFunction';
import { EnumLoadingState } from 'src/app/common/enum';
import { BaseService } from 'src/app/model/base/BaseService';
import { FilterGroup } from 'src/app/model/base/paging/filter';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { FtsColumn } from '../fts-grid/fts-grid.component';
import { FtsEditListBaseInject } from './fts-edit-list-base-inject';
import { FtsEditListBaseComponent } from './fts-edit-list-base.component';

@Component({
  template: '',
})
export abstract class FtsEditListBase implements OnInit, OnDestroy {
  id: string = commonFunction.newGuid();
  abstract tableName: string;
  abstract columns: FtsColumn[];
  abstract idField: string;
  abstract nameField: string;

  get itemSelected(): any {
    return this.editListBaseComponent?.grdListing?.itemSelected || undefined;
  }

  set itemSelected(v: any) {
    if (this.editListBaseComponent?.grdListing?.itemSelected) {
      this.editListBaseComponent.grdListing.itemSelected = v;
    }
  }

  validateBeforSave(): boolean {
    return this.validateGrid();
  }

  defaultRecord: any;

  /**
   * Khởi tạo các giá trị cho record khi thêm mới
   */
  getNewRecord(): object {
    return { ...this.defaultRecord };
  }

  getDefauldRecord() {
    return new Promise<any>((resolve, reject) => {
      this.myInject.editListBaseStore.setLoadingState(EnumLoadingState.Loading);
      this.myService
        .AddNewData()
        .then((item: any) => {
          this.defaultRecord = item;
          resolve(item);
        })
        .catch((err) => {
          this.myInject.ftsDialogService.alert.show({
            content:
              this.myInject.FTSMain.ExceptionManager.processException(err),
            icon: 'warning',
            maxWidth: 300,
          });
          reject(err);
        })
        .finally(() => {
          this.myInject.editListBaseStore.setLoadingState(
            EnumLoadingState.Complete
          );
        });
    });
  }

  saveAndReload: boolean = false;

  oldData: {
    RecordCount: number;
    Data: any[];
    SummaryData: any;
  } = { RecordCount: 0, Data: [], SummaryData: null };

  @ViewChild(FtsEditListBaseComponent)
  editListBaseComponent!: FtsEditListBaseComponent;

  public recordIdChanges: Array<{
    recordId: any;
    editMode: 'ADD' | 'EDIT' | 'DELETE' | 'NONE';
  }> = [];

  get tooltipDir(): TooltipDirective {
    return this.editListBaseComponent.tooltipDir;
  }

  public onDestroy$ = new Subject<void>();
  public onDestroyForm$ = new Subject<void>();

  constructor(
    public myService: BaseService<any>,
    public myInject: FtsEditListBaseInject
  ) {}
  ngOnDestroy(): void {
    this.onDestroy$.next();
    this.onDestroyForm$.next();
  }
  ngOnInit(): void {
    this.formGroupEditRow = this.createFormGroup();
  }
  ngAfterViewInit(): void {
    this.myInject.editListBaseStore.loadingState$
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((loadingState) => {
        if (loadingState == EnumLoadingState.Loading) this.mask(true);
        else this.mask(false);
      });
    this.getDefauldRecord();
    this.loadData();

    this.myInject.editListBaseStore.setColumns(this.columns);
    this.myInject.editListBaseStore.setIdField(this.idField);

    if (this.editListBaseComponent?.grdListing) {
      this.editListBaseComponent.grdListing.getNewRecord =
        this.getNewRecord.bind(this);

      this.editListBaseComponent.grdListing.recordChange
        .pipe(takeUntil(this.onDestroy$))
        .subscribe((state) => {
          this.onRecordChange(state);
        });

      this.myInject.editListBaseStore.dataStateChange
        .pipe(takeUntil(this.onDestroy$))
        .subscribe((state) => {
          this.editListBaseComponent.grdListing.closeCell();
          if (this.recordIdChanges.length > 0) {
            this.myInject.ftsDialogService.confirm
              .show({
                icon: 'question',
                content:
                  this.myInject.resourceManager.CommonResource.MyResource
                    .MessageCloseDataChange,
              })
              .pipe(take(1))
              .subscribe((value) => {
                if (value == 'yes') {
                  this.saveAndReload = true;
                  this.myInject.editListBaseStore.actionClick.emit([
                    'SAVE',
                    EnumLoadingState.Loading,
                  ]);
                } else if (value == 'no') {
                  this.loadData(state);
                }
              });
          } else {
            this.loadData(state);
          }
        });

      this.myInject.editListBaseStore.actionClick
        .pipe(takeUntil(this.onDestroy$))
        .subscribe(([actionType, loadingState]) => {
          if (loadingState == EnumLoadingState.Loading) {
            switch (actionType) {
              case 'REFRESH':
                this.refresh();
                break;
              case 'IMPORT_EXCEL':
                this.importExcel();
                break;
              case 'EXPORT_EXCEL':
                this.exportExcel();
                break;
              case 'SAVE':
                this.commitChange();
                break;
              case 'UNDO':
                this.undoChange();
                break;
              case 'DUPLICATE':
                this.duplicateRow();
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
    }
  }

  /**
   * show thông báo lỗi validate theo control
   */
  showValidateMessage(control: AbstractControl, controlEl: any): void {
    if (control && controlEl) {
      let message = '';
      if (control?.errors?.required) {
        message = `Trường này không được để trống`;
      } else if (control?.errors?.maxlength) {
        message = `Trường này tối đa ${control?.errors?.maxlength.requiredLength} ký tự`;
      } else if (control?.errors?.minlength) {
        message = `Trường này tối thiểu ${control?.errors?.minlength.requiredLength} ký tự`;
      } else if (control?.errors?.min) {
        message = `Trường này phải lớn hơn hoặc bằng ${control?.errors?.min.min}`;
      } else if (control?.errors?.max) {
        message = `Trường này phải nhỏ hơn hoặc bằng ${control?.errors?.max.max}`;
      } else if (control?.errors?.email) {
        message = `Email không đúng định dạng`;
      }

      controlEl.setAttribute('data-tooltip', message);
      const _tooltipDir = this.tooltipDir;

      if (controlEl.timeout) {
        clearTimeout(controlEl.timeout);
      }
      controlEl.timeout = setTimeout(() => {
        _tooltipDir?.toggle(controlEl, false);
      }, 3000);

      _tooltipDir?.hide();
      _tooltipDir?.show(controlEl);
    }
  }

  validateGrid(): boolean {
    const grid = this.editListBaseComponent.grdListing;
    if (grid) {
      grid.closeCell();
      const datas = grid.getDatas() || [];
      const cols = grid.grid.columnList.filter((x) => x.isVisible);
      for (let i = 0; i < datas.length; i++) {
        const row = datas[i];
        const formGroup = this.createFormGroup();
        formGroup.reset(row);
        if (formGroup.invalid) {
          for (let j = 0; j < cols.length; j++) {
            const col = cols[j] as ColumnComponent;
            if (col.field) {
              const formControl = formGroup.get(col.field);
              if (formControl?.invalid) {
                grid.itemSelected = row;
                grid.editCell(datas.indexOf(row), col);
                setTimeout(() => {
                  const controlEl = grid.el.nativeElement.querySelector(
                    `.k-grid-edit-cell [controlname="${col?.field}"],.k-grid-edit-cell [formcontrolname="${col?.field}"]`
                  );
                  if (controlEl) {
                    this.showValidateMessage(formControl, controlEl);
                  }
                }, 100);
                return false;
              }
            }
          }
        }
      }
    }
    return true;
  }

  importedSubscription!: Subscription;
  importExcel() {
    if (this.editListBaseComponent.importExcel) {
      this.editListBaseComponent.importExcel.service = this.myService;
      this.editListBaseComponent.importExcel.tableName = this.tableName;
      this.editListBaseComponent.importExcel.open();
      this.importedSubscription?.unsubscribe();
      this.importedSubscription =
        this.editListBaseComponent.importExcel.importedEvent
          .pipe(takeUntil(this.onDestroy$))
          .subscribe((x) => {
            if (x) {
              this.loadData();
            }
          });
    }
  }

  onRecordChange(state: {
    record: any;
    editMode: 'ADD' | 'EDIT' | 'DELETE' | 'NONE';
  }) {
    const { record, editMode } = state;
    const idx = this.recordIdChanges.findIndex(
      (x) => x.recordId == record[this.idField]
    );
    switch (editMode) {
      case 'ADD':
      case 'EDIT':
        if (idx >= 0) {
          this.recordIdChanges[idx] = {
            ...this.recordIdChanges[idx],
            editMode,
          };
        } else {
          this.recordIdChanges.push({
            recordId: record[this.idField],
            editMode: editMode,
          });
        }
        break;
      case 'DELETE':
        if (record.editMode != 'ADD') {
          if (idx >= 0) {
            this.recordIdChanges[idx] = {
              ...this.recordIdChanges[idx],
              editMode,
            };
          } else {
            this.recordIdChanges.push({
              recordId: record[this.idField],
              editMode: editMode,
            });
          }
        } else if (idx >= 0) {
          this.recordIdChanges.splice(idx, 1);
        }
        // if (record.EditMode != 'ADD') {
        //   this.delete({ ...record });
        // } else if (idx >= 0) {
        //   this.recordIdChanges.splice(idx, 1);
        // }
        break;
      case 'NONE':
        if (idx >= 0) {
          this.recordIdChanges.splice(idx, 1);
        }
        break;
    }

    this.setShowHideToolbar();
  }

  setShowHideToolbar() {
    this.editListBaseComponent.showToolbarSave =
      this.recordIdChanges.length > 0;
    this.editListBaseComponent.grdListing.showToolbarImport =
      !this.editListBaseComponent.showToolbarSave;
    this.editListBaseComponent.grdListing.showToolbarExcel =
      !this.editListBaseComponent.showToolbarSave;
  }

  delete(record: any) {
    if (record) {
      this.myInject.ftsDialogService.confirm
        .show({
          icon: 'question',
          content: commonFunction.stringFormat(
            this.myInject.resourceManager.CommonResource.MyResource
              .ConfirmBeforDelete,
            `${record[this.nameField]} - ${record[this.idField]}`
          ),
        })
        .pipe(take(1))
        .subscribe((state) => {
          if (state == 'yes') {
            this.myInject.editListBaseStore.setLoadingState(
              EnumLoadingState.Loading
            );

            this.myService
              .Delete(record[this.idField])
              .then(() => {
                this.myInject.editListBaseStore.gridData$
                  .pipe(take(1))
                  .subscribe((gridData) => {
                    this.myInject.editListBaseStore.setGridData({
                      data: gridData.data.filter(
                        (x) => x[this.idField] != record[this.idField]
                      ),
                      total: gridData.total - 1,
                    });
                  });

                const idx = this.recordIdChanges.findIndex(
                  (x) => x.recordId == record[this.idField]
                );

                if (idx >= 0) {
                  this.recordIdChanges.splice(idx, 1);
                }

                this.editListBaseComponent.grdListing.closeCell();
                this.myInject.editListBaseStore.setCurrentRow(undefined);
                this.itemSelected = undefined;

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
              .catch((err: any) => {
                this.myInject.ftsDialogService.alert.show({
                  content:
                    this.myInject.FTSMain.ExceptionManager.processException(
                      err
                    ),
                  icon: 'warning',
                  maxWidth: 300,
                });
              })
              .finally(() => {
                this.myInject.editListBaseStore.setLoadingState(
                  EnumLoadingState.Complete
                );
              });
          }
        });
    }
  }

  setParamBeforLoad(param: PagingParam): PagingParam {
    return param;
  }

  /**
   * load dữ liệu
   * Created by: MTLUC - 03/11/2021
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

    this.myInject.editListBaseStore.setLoadingState(EnumLoadingState.Loading);
    this.myService
      .loadDataPaging(param)
      .then((respon) => {
        this.recordIdChanges = [];
        this.myInject.editListBaseStore.loadDataComplete({
          gridData: { data: respon.Data || [], total: respon.RecordCount || 0 },
          error: undefined,
        });

        let datas: any[] = [];
        if (respon.Data.length) {
          respon.Data.forEach((item) => {
            datas.push({ ...item });
          });
        }

        this.oldData = {
          RecordCount: respon.RecordCount,
          Data: datas,
          SummaryData: { ...respon.SummaryData },
        };
      })
      .catch((err) => {
        this.myInject.ftsDialogService.alert.show({
          icon: 'warning',
          maxWidth: 300,
          content: this.myInject.FTSMain.ExceptionManager.processException(err),
        });

        this.myInject.editListBaseStore.loadDataComplete({
          gridData: { data: [], total: 0 },
          error: err,
        });
      })
      .finally(() => {
        this.saveAndReload = false;
        this.setShowHideToolbar();
        this.myInject.editListBaseStore.setLoadingState(EnumLoadingState.Init);
      });
  }

  /**
   * hiệu ứng loading
   * @param show
   * Created by: MTLUC - 03/11/2021
   */
  mask(show: boolean) {
    if (show) this.myInject.maskLoad.show(this.myInject.viewContainerRef);
    else this.myInject.maskLoad.hide();
  }

  formGroupEditRow!: FormGroup;
  abstract createFormGroup(): FormGroup;

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

  refresh(): void {
    this.clearSort();
    this.clearFilters();
    this.myInject.editListBaseStore.setCurrentRow(undefined);
  }

  clearSort(): void {
    this.myInject.editListBaseStore.setSort([]);
  }

  clearFilters(): void {
    this.myInject.editListBaseStore.setFilter({
      logic: 'and',
      filters: [],
    });
  }

  commitChange(): void {
    if (this.validateBeforSave()) {
      this.saveData(this.recordIdChanges);
    }
  }

  undoChange() {
    this.recordIdChanges = [];
    this.editListBaseComponent.grdListing.closeCell();
    this.itemSelected = undefined;
    this.myInject.editListBaseStore.loadDataComplete({
      gridData: {
        data: [...this.oldData.Data] || [],
        total: this.oldData.RecordCount || 0,
      },
      error: undefined,
    });
    this.setShowHideToolbar();
  }

  duplicateRow() {
    if (this.itemSelected) {
      const grdListing = this.editListBaseComponent.grdListing;
      const datas: any[] = grdListing.getDatas();
      let newRecord = {
        ...grdListing.itemSelected,
        [this.idField]: (this.getNewRecord() as any)[this.idField],
        editMode: 'ADD',
      };
      datas.splice(datas.indexOf(grdListing.itemSelected) + 1, 0, newRecord);
      grdListing.itemSelected = newRecord;
      grdListing.editRow(datas.indexOf(grdListing.itemSelected), newRecord);

      grdListing.recordChange?.emit({
        record: newRecord,
        editMode: 'ADD',
        field: '',
      });
    }
  }

  saveData(
    recordIdChanges: Array<{
      recordId: any;
      editMode: 'ADD' | 'EDIT' | 'DELETE' | 'NONE';
    }>
  ) {
    let _recordIdChanges = [...recordIdChanges];
    if (_recordIdChanges.length) {
      const that = this;
      const datas = that.editListBaseComponent.grdListing.getDatas();
      let fn = (idx: number) => {
        if (idx <= _recordIdChanges.length) {
          let recordIdChange = _recordIdChanges[idx];
          let promise: Promise<any>;
          let jmp = 0;
          const _record = datas.find(
            (x) => x[that.idField] == recordIdChange.recordId
          );
          if (recordIdChange.editMode == 'ADD') {
            promise = that.myService.CreateData(_record);
            jmp = 1;
          } else if (recordIdChange.editMode == 'DELETE') {
            promise = that.myService.Delete(recordIdChange.recordId);
            jmp = -1;
          } else {
            promise = that.myService.UpdateData(_record);
          }

          promise
            .then((item) => {
              if (_record) {
                delete _record.editMode;
                Object.assign(_record, item);
              }

              that.myInject.editListBaseStore.gridData$
                .pipe(take(1))
                .subscribe((gridData) => {
                  that.myInject.editListBaseStore.setGridData({
                    data: gridData.data,
                    total: gridData.total + jmp,
                  });
                });

              that.recordIdChanges = that.recordIdChanges.filter(
                (x) => x.recordId != recordIdChange.recordId
              );

              this.setShowHideToolbar();

              if (idx == _recordIdChanges.length - 1) {
                that.myInject.editListBaseStore.setLoadingState(
                  EnumLoadingState.Complete
                );

                that.myInject.editListBaseStore.setCurrentRow(undefined);
                that.editListBaseComponent.grdListing.itemSelected = undefined;

                this.myInject.notificationService.show({
                  content:
                    this.myInject.resourceManager.CommonResource.MyResource
                      .SaveSuccess,
                  hideAfter: 1000,
                  position: { horizontal: 'right', vertical: 'bottom' },
                  animation: { type: 'fade', duration: 400 },
                  type: { style: 'success', icon: true },
                });
                if (this.saveAndReload) {
                  this.loadData();
                }
              } else {
                fn(idx + 1);
              }
            })
            .catch((error) => {
              this.myInject.editListBaseStore.setLoadingState(
                EnumLoadingState.Complete
              );

              const recordChange = datas.find(
                (x) => x[that.idField] == recordIdChange.recordId
              );
              that.myInject.editListBaseStore.setCurrentRow(recordChange);
              that.editListBaseComponent.grdListing.itemSelected = recordChange;
              if (recordChange) {
                that.editListBaseComponent.grdListing.editRow(
                  datas.indexOf(recordChange),
                  recordChange
                );
              }
              that.myInject.ftsDialogService.alert.show({
                icon: 'warning',
                maxWidth: 300,
                content:
                  that.myInject.FTSMain.ExceptionManager.processException(
                    error
                  ),
              });
            });
        }
      };

      this.myInject.editListBaseStore.setLoadingState(EnumLoadingState.Loading);
      fn(0);
    }
  }

  insert(record: any): void {
    let data = { ...record };
    /**TAN.VU: Set tạm UpperCase cho idField */
    data[this.idField] = data[this.idField].toUpperCase();

    this.myInject.editListBaseStore.setLoadingState(EnumLoadingState.Loading);

    this.myService
      .CreateData(data)
      .then((item: any) => {
        this.myInject.notificationService.show({
          content:
            this.myInject.resourceManager.CommonResource.MyResource.SaveSuccess,
          hideAfter: 1000,
          position: { horizontal: 'right', vertical: 'bottom' },
          animation: { type: 'fade', duration: 400 },
          type: { style: 'success', icon: true },
        });
      })
      .catch((error) => {
        this.myInject.ftsDialogService.alert.show({
          icon: 'warning',
          maxWidth: 300,
          content:
            this.myInject.FTSMain.ExceptionManager.processException(error),
        });
      })
      .finally(() => {
        this.myInject.editListBaseStore.setLoadingState(
          EnumLoadingState.Complete
        );
      });
  }

  /**
   * Cập nhật bản ghi
   */
  update(record: any): void {
    let data = { ...record };

    this.myInject.editListBaseStore.setLoadingState(EnumLoadingState.Loading);

    this.myService
      .UpdateData(data)
      .then((item: any) => {
        this.myInject.notificationService.show({
          content:
            this.myInject.resourceManager.CommonResource.MyResource.SaveSuccess,
          hideAfter: 1000,
          position: { horizontal: 'right', vertical: 'bottom' },
          animation: { type: 'fade', duration: 400 },
          type: { style: 'success', icon: true },
        });
      })
      .catch((error) => {
        this.myInject.ftsDialogService.alert.show({
          icon: 'warning',
          maxWidth: 300,
          content:
            this.myInject.FTSMain.ExceptionManager.processException(error),
        });
      })
      .finally(() => {
        this.myInject.editListBaseStore.setLoadingState(
          EnumLoadingState.Complete
        );
      });
  }

  handleKeyDown() {
    const that = this;
    that.myInject.eventManager.SubcriberKeyDown(that.id, (e: KeyboardEvent) => {
      const strKey = e.key.toUpperCase();
      let stopEvt = false;

      //tổ hợp phím ctr
      if (e.ctrlKey) {
        switch (strKey) {
          case 'I':
            this.editListBaseComponent.grdListing.addRow();
            stopEvt = true;
            break;
          case 'D':
            this.editListBaseComponent.grdListing.removeRow();
            stopEvt = true;
            break;
        }
      }

      if (stopEvt) {
        e.preventDefault();
      }
    });
  }
}

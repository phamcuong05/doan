import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Optional,
  Output,
  Self,
  ViewChild,
} from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { GridDataResult } from '@progress/kendo-angular-grid';
import {
  CompositeFilterDescriptor,
  SortDescriptor,
} from '@progress/kendo-data-query';
import { FTSMain } from 'src/app/base/ftsmain';
import { commonFunction } from 'src/app/common/commonFunction';
import { EventManager } from 'src/app/common/eventManager';
import { ResourceManager } from 'src/app/common/resource-manager';
import { ActionType } from 'src/app/common/types';
import { BaseService } from 'src/app/model/base/BaseService';
import { FilterGroup } from 'src/app/model/base/paging/filter';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { FtsDialogService } from '../fts-dialog/fts-dialog.service';
import { FtsGridComponent } from '../fts-grid/fts-grid.component';

@Component({
  selector: 'fts-popup-multi-selector',
  templateUrl: './fts-popup-multi-selector.component.html',
  styleUrls: ['./fts-popup-multi-selector.component.scss'],
})
export class FtsPopupMultiSelectorComponent
  implements ControlValueAccessor, OnInit
{
  //------------------------------------------
  id: string = commonFunction.newGuid();
  @ViewChild('grdFrom') grdFrom!: FtsGridComponent;
  @ViewChild('grdTo') grdTo!: FtsGridComponent;

  @Input() service!: BaseService<any>;
  @Input() formTitle!: string;

  @Input() colId: string = 'ID';
  @Input() colName: string = 'Name';
  @Input() titleColId!: string;
  @Input() titleColName!: string;

  @Input() popupWidth: number = 900;
  @Input() popupHeight: number = 450;

  set fromItemSelected(v: any) {
    if (this.grdFrom) {
      this.grdFrom.itemSelected = v;
    }
  }
  get fromItemSelected(): any {
    return this.grdFrom?.itemSelected;
  }

  set toItemSelected(v: any) {
    if (this.grdTo) {
      this.grdTo.itemSelected = v;
    }
  }
  get toItemSelected(): any {
    return this.grdTo?.itemSelected;
  }

  public filter: CompositeFilterDescriptor = {
    logic: 'and',
    filters: [],
  };
  public sort: SortDescriptor[] = [];

  //#region show
  private _show: boolean = false;
  @Input() set show(v: boolean) {
    if (v !== this.show) {
      this._show = v;
      this.showChange.emit(v);
      if (v) {
        this.initDataSource();
      }
    }
    if (v) {
      this.handleKeyDown();
    } else {
      this.eventManager.UnSubcriberKeyDown(this.id);
    }
  }
  get show(): boolean {
    return this._show;
  }
  @Output() showChange = new EventEmitter<boolean>();
  //#endregion

  private onChanged: Function = () => {};
  private onTouched: Function = () => {};
  private updateOn: 'change' | 'blur' | 'submit' = 'change';

  dataSource: GridDataResult = {
    data: [],
    total: 0,
  };

  fromDataSource: GridDataResult = {
    data: [],
    total: 0,
  };

  toDataSource: any[] = [];
  //------------------------------------------
  constructor(
    public resourceManager: ResourceManager,
    public ftsDialog: FtsDialogService,
    public ftsMain: FTSMain,
    public eventManager: EventManager,
    @Optional() @Self() public ngControl: NgControl
  ) {
    if (ngControl != null) {
      ngControl.valueAccessor = this;
    }
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {}

  ngOnDestroy(): void {
    this.eventManager.UnSubcriberKeyDown(this.id);
  }

  writeValue(obj: any): void {
    this.value = obj;
    if (!obj) {
      obj = 0;
    }
  }

  registerOnChange(fn: any): void {
    this.onChanged = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  /**
   * Bắt event keydown window.
   */
  handleKeyDown() {
    const that = this;
    that.eventManager.SubcriberKeyDown(that.id, (e: KeyboardEvent) => {
      const strKey = e.key.toUpperCase();
      let stopEvt = false;
      //esc
      if (strKey == 'ESCAPE') {
        this.show = false;
        stopEvt = true;
      }

      //
      if (stopEvt) {
        e.preventDefault();
      }
    });
  }

  values: string[] = [];

  initDataSource() {
    this.dataSource = {
      data: [],
      total: 0,
    };

    this.fromDataSource = {
      data: [],
      total: 0,
    };

    this.toDataSource = [];

    this.values = this.value?.split(',')?.filter((x) => x) || [];

    this.isShowMask = true;
    Promise.all([this.loadFromData(), this.loadToData()])
      .then(([fromData, toData]) => {
        this.dataSource = {
          data: fromData.Data || [],
          total: fromData.RecordCount || 0,
        };

        this.fromDataSource = {
          data: this.dataSource.data.filter(
            (x) => this.values.findIndex((y) => y == x[this.colId]) < 0
          ),
          total: this.dataSource.total,
        };

        this.toDataSource = toData.Data || [];
      })
      .catch((error) => {
        this.ftsDialog.alert.show({
          icon: 'warning',
          maxWidth: 300,
          content: this.ftsMain.ExceptionManager.processException(error),
        });
      })
      .finally(() => {
        this.isShowMask = false;
      });
  }

  loadFromData(): Promise<{
    RecordCount: number;
    Data: any[];
    SummaryData: any;
  }> {
    const grdState = this.grdFrom?.state;
    let param: PagingParam = {
      FilterFields: [this.colId, this.colName],
      FilterGroups: [],
      PageIndex: 1,
      PageSize: 50,
      Sorts: [],
      SumaryFields: [],
      TranId: '',
    };

    if (grdState) {
      param.PageIndex = (grdState.skip || 0) / (grdState.take || 50) + 1;
      param.PageSize = grdState.take || 50;

      if (grdState.sort?.length) {
        param.Sorts = [
          {
            Field: grdState.sort[0].field,
            Dir: grdState.sort[0].dir || 'asc',
          },
        ];
      }

      if (grdState.filter?.filters) {
        let filterGroup: FilterGroup = {
          Filters: [],
          Logic: grdState.filter.logic,
        };
        grdState.filter?.filters.forEach((item: any) => {
          filterGroup.Filters.push({
            Field: item.field,
            Operator: item.operator,
            Value: item.value,
          });
        });
        param.FilterGroups?.push(filterGroup);
      }
    }

    // if (this.values?.length) {
    //   let filterGroup: FilterGroup = {
    //     Filters: [],
    //     Logic: 'and',
    //   };
    //   this.values.forEach((item) => {
    //     filterGroup.Filters.push({
    //       Field: this.fieldId,
    //       Operator: 'neq',
    //       Value: item,
    //     });
    //   });
    //   param.FilterGroups?.push(filterGroup);
    // }

    return new Promise<{
      RecordCount: number;
      Data: any[];
      SummaryData: any;
    }>((resolve, reject) => {
      this.service
        .loadDataPaging(param)
        .then((data) => {
          resolve(data);
        })
        .catch((error) => {
          reject(error);
        });
    });
  }

  onDataStateChange() {
    this.isShowMask = true;
    this.loadFromData()
      .then((data) => {
        this.dataSource = {
          data: data.Data || [],
          total: data.RecordCount || 0,
        };

        this.fromDataSource = {
          data: this.dataSource.data.filter(
            (x) => this.values.findIndex((y) => y == x[this.colId]) < 0
          ),
          total: this.dataSource.total,
        };
      })
      .catch((error) => {
        this.ftsDialog.alert.show({
          icon: 'warning',
          maxWidth: 300,
          content: this.ftsMain.ExceptionManager.processException(error),
        });
      })
      .finally(() => {
        this.isShowMask = false;
      });
  }

  toolbarActionHandler({
    action,
    record,
  }: {
    action: ActionType;
    record: any;
  }) {
    if (action == 'REFRESH') {
      this.refreshFromData();
    }
  }

  loadToData(): Promise<{
    RecordCount: number;
    Data: any[];
    SummaryData: any;
  }> {
    if (this.values?.length) {
      let param: PagingParam = {
        FilterFields: [this.colId, this.colName],
        FilterGroups: [],
        PageIndex: 1,
        PageSize: 1000000000,
        Sorts: [],
        SumaryFields: [],
        TranId: '',
      };

      let filterGroup: FilterGroup = {
        Filters: [],
        Logic: 'or',
      };
      this.values.forEach((item) => {
        filterGroup.Filters.push({
          Field: this.colId,
          Operator: 'eq',
          Value: item,
        });
      });
      param.FilterGroups?.push(filterGroup);

      return new Promise<{
        RecordCount: number;
        Data: any[];
        SummaryData: any;
      }>((resolve, reject) => {
        this.service
          .loadDataPaging(param)
          .then((data) => {
            resolve(data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    }

    return new Promise<{
      RecordCount: number;
      Data: any[];
      SummaryData: any;
    }>((resolve, reject) => {
      resolve({ RecordCount: 0, Data: [], SummaryData: {} });
    });
  }
  //----------------

  public value!: string;
  isShowMask: boolean = false;

  onSelectItem(e?: Event) {
    if (this.fromItemSelected) {
      const item = { ...this.fromItemSelected };

      this.values.push(item[this.colId]);
      this.toDataSource = [...this.toDataSource, item];
      this.fromDataSource.data = this.dataSource.data.filter(
        (x) => this.values.findIndex((y) => y == x[this.colId]) < 0
      );
      this.fromItemSelected = undefined;
      this.toItemSelected = item;
    }
  }

  onUnSelectItem(e?: Event) {
    if (this.toItemSelected) {
      this.values = this.values.filter(
        (x) => x != this.toItemSelected[this.colId]
      );
      this.toDataSource = this.toDataSource.filter(
        (x) => x[this.colId] != this.toItemSelected[this.colId]
      );

      this.fromDataSource.data = this.dataSource.data.filter(
        (x) => this.values.findIndex((y) => y == x[this.colId]) < 0
      );

      this.fromItemSelected = this.fromDataSource.data.find(
        (x) => x[this.colId] == this.toItemSelected[this.colId]
      );
      this.toItemSelected = undefined;
    }
  }

  onUnSelectAll(e: Event) {
    this.toDataSource = [];
    this.fromDataSource.data = [...this.dataSource.data];
    this.values = [];
    this.refreshFromData();
  }

  refreshFromData() {
    this.sort = [];
    this.filter = {
      filters: [],
      logic: 'and',
    };
  }

  btnOk_Click(e: Event) {
    let _value = '';
    if (this.toDataSource?.length) {
      _value = this.values.join(',');
    }
    if (this.value != _value) {
      this.value = _value;
      this.onChanged(_value);
      if (this.updateOn != 'blur') {
        this.onTouched();
      }
    }
    this.show = false;
  }
}

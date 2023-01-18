import {
  Component,
  ContentChildren,
  ElementRef,
  EventEmitter,
  HostListener,
  Input,
  OnInit,
  Output,
  QueryList,
  Renderer2,
  TemplateRef,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import {
  CellCloseEvent,
  ColumnComponent,
  DataStateChangeEvent,
  GridComponent,
  GridDataResult,
  PagerSettings,
  RowArgs,
  SelectionEvent,
} from '@progress/kendo-angular-grid';
import { IntlService } from '@progress/kendo-angular-intl';
import { ToolBarButtonComponent } from '@progress/kendo-angular-toolbar';
import {
  aggregateBy,
  CompositeFilterDescriptor,
  GroupDescriptor,
  process,
  SortDescriptor,
  State,
} from '@progress/kendo-data-query';
import { encodeBase64, saveAs } from '@progress/kendo-file-saver';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FTSMain } from 'src/app/base/ftsmain';
import { ResourceManager } from 'src/app/common/resource-manager';
import { ActionType, ColumnType } from 'src/app/common/types';
import { commonFunction } from './../../common/commonFunction';
import { FtsGridColumnComponent } from './fts-grid-column/fts-grid-column.component';

@Component({
  selector: 'fts-grid',
  templateUrl: './fts-grid.component.html',
  encapsulation: ViewEncapsulation.None,
})
/**
 * Base FTS Grid
 * Created by: MTLUC - 25/10/2021
 */
export class FtsGridComponent implements OnInit {
  @ContentChildren(FtsGridColumnComponent)
  ftsColumns?: QueryList<FtsGridColumnComponent>;
  @ContentChildren(ToolBarButtonComponent)
  toolbarbuttons!: QueryList<ToolBarButtonComponent>;

  @Input() resourceCol!: any;

  @Input() summaryData!: any;

  /**
   * Có sort hay không
   * Created by: MTLUC - 25/10/2021
   */
  @Input() sortable: boolean = true;
  @Input() showToolBar: boolean = true;

  /**
   * Có filter không
   * Created by: MTLUC - 25/10/2021
   */
  @Input() filterable: boolean = true;

  private _dataBinding: Array<any> = [];
  /**
   * Có dataBinding
   * Created by: MTLUC - 25/10/2021
   */
  @Input() set dataBinding(v: Array<any>) {
    //this.closeCell();
    this._dataBinding = v;
    this.gridData = process(this._dataBinding, {
      ...this.state,
      group: this.groups,
    });
  }

  get dataBinding(): any[] {
    return this._dataBinding;
  }

  /**
   * Cấu hình pageable
   * Created by: MTLUC - 25/10/2021
   */
  @Input() pageable: PagerSettings | boolean = {
    buttonCount: 3,
    type: 'input',
    info: true,
    pageSizes: [25, 50, 100, 150],
    previousNext: true,
    position: 'bottom',
  };

  /**
   * Cáu hình số bản ghi trên 1 trang
   * Created by: MTLUC - 25/10/2021
   */
  @Input() pageSize: number = this._FTSMain.PageSize;

  /**
   * cho phép thay đổi kích thước cột
   * Created by: MTLUC - 25/10/2021
   */
  @Input() resizable: boolean = true;

  /**
   * Có hiển thị cột select không
   * Created by: MTLUC - 26/10/2021
   */
  @Input() showSelectColumn: boolean = false;

  @Input() isServerPaging: boolean = false;

  /**
   * Có hiển thị cột thứ tự dòng không
   * Created by: MTLUC - 26/10/2021
   */
  @Input() showIndexColumn: boolean = true;

  @Input() loading: boolean = true;

  private _filter: CompositeFilterDescriptor = {
    logic: 'and',
    filters: [],
  };
  @Input() set filter(v: CompositeFilterDescriptor) {
    this._filter = v;
    if (this.grid) {
      this.grid.filter = this._filter;
      this.grid.filterChange.emit(this._filter);
    }
  }
  get filter(): CompositeFilterDescriptor {
    return this._filter;
  }

  @Output() filterChange: EventEmitter<CompositeFilterDescriptor> =
    new EventEmitter();

  @Input() sort: SortDescriptor[] = [];
  @Output() sortChange: EventEmitter<SortDescriptor[]> = new EventEmitter();

  @Output() recordChange: EventEmitter<{
    record: any;
    editMode: 'ADD' | 'EDIT' | 'DELETE' | 'NONE';
    field: string;
  }> = new EventEmitter();

  @ViewChild(GridComponent) grid!: GridComponent;

  @Input() getNewRecord = (): object => {
    return {};
  };

  @Input() formGroup!: FormGroup;

  @Input() isRowEditor: boolean = false;
  @Input() idField: string = '';

  // cho phép hiển thị nút thêm dòng
  @Input() showToolbarAddRow: boolean = false;
  // cho pép hiển thị nút xóa dòng
  @Input() showToolbarRemoveRow: boolean = false;
  // cho phép hiển thị nút import
  @Input() showToolbarImport: boolean = false;
  // cho phép hiển thị nút Excel
  @Input() showToolbarExcel: boolean = false;
  // cho phép hiển thị nút refresh
  @Input() showToolbarRefresh: boolean = true;
  @Input() showPagerPageSizes: boolean = true;

  @Input() showToolbarDuplicateRow: boolean = false;

  // cho phép hiển thị nút tùy chọn cột
  @Input() showButtonConfigColumn: boolean = true;

  // disabledToolBar
  @Input() disabledToolBar: boolean = false;

  @Input() groups: GroupDescriptor[] = [];

  @Output() toolbarActionEvent: EventEmitter<{
    action: ActionType;
    record: any;
  }> = new EventEmitter();

  @Output() dataState_Change: EventEmitter<DataStateChangeEvent> =
    new EventEmitter();

  public itemSelected!: any;

  public isRowSelected = (e: RowArgs) => this.itemSelected == e.dataItem;

  public gridId = commonFunction.newGuid();

  private _aggegates: any[] = [];

  public get aggregates(): any[] {
    if (this._aggegates.length <= 0) {
      this._aggegates = [];
      this.ftsColumns?.forEach((col) => {
        if (col?.aggregate) {
          this._aggegates.push({
            field: col.fieldId,
            aggregate: col.aggregate,
          });
        }
      });
    }
    return this._aggegates;
  }

  public state: State = {
    skip: 0,
    take: this.pageSize,
    filter: this.filter,
  };

  @Input() oldDatas: any[] = [];

  private _gridData: GridDataResult = process(this._dataBinding, {
    ...this.state,
    group: this.groups,
  });
  public get gridData(): GridDataResult {
    return this._gridData;
  }
  @Input() set gridData(v: GridDataResult) {
    this._gridData = v;

    let items: any[] = [];
    if (!this.groups.length) {
      items = v.data;
    } else {
      v.data?.forEach((group) => {
        items.push(...group.items);
      });
    }
    this.oldDatas = JSON.parse(JSON.stringify(items));
    this.aggregate = aggregateBy(this.gridData.data, this.aggregates);
  }
  public aggregate: any = aggregateBy(this.gridData.data, this.aggregates);

  public contextMenuState = {
    isShow: false,
    clientX: 0,
    clientY: 0,
  };

  private onDestroy$ = new Subject<void>();
  private takeUntilChangeCell$ = new Subject<void>();

  constructor(
    public resourceManager: ResourceManager,
    public intl: IntlService,
    private formBuilder: FormBuilder,
    private renderer: Renderer2,
    private _FTSMain: FTSMain,
    public el: ElementRef
  ) {}

  ngOnInit(): void {
    this.eventEditCellKeydown
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((e) => {
        this.editCellKeydown(e);
      });
  }

  ngOnDestroy(): void {
    this.takeUntilChangeCell$.next();
    this.onDestroy$.next();
  }

  ftsGrid_onClick(e: any) {
    const cellClicked = e.target.closest('[kendogridcell]');
    if (cellClicked) {
      const gridEl = cellClicked.closest('kendo-grid');
      const row = cellClicked.closest('tr[role=row]');
      const colIdx = cellClicked
        .closest('td[role=gridcell]')
        ?.getAttribute('data-kendo-grid-column-index');

      if (gridEl && gridEl.id == this.gridId && row && colIdx) {
        const datas = this.getDatas();
        let item = undefined;
        const column = this.grid.columns.filter((x) => x.isVisible)[
          colIdx
        ] as ColumnComponent;
        const rowidx =
          Number.parseInt(row.getAttribute('data-kendo-grid-item-index')) || 0;
        if (datas) {
          item = datas[rowidx - this.grid.skip];
        }

        const ctrName = commonFunction.getControlName(
          this.formGroup?.controls,
          this.formControl
        );

        if (this.disabledToolBar) {
          this.itemSelected = item;
          this.columnEdit = undefined;
          this.grid.closeCell();
        } else if (item != this.itemSelected) {
          this.itemSelected = item;
          this.selectionChange.emit(item);
          this.editCell(rowidx, column);
        } else if (
          ctrName != column.field ||
          !this.columnEdit ||
          this.columnEdit != column
        ) {
          this.editCell(rowidx, column);
        } else if (
          ctrName == column.field &&
          this.columnEdit &&
          this.columnEdit == column &&
          !cellClicked
            .closest('td[role=gridcell]')
            .getElementsByTagName('input').length
        ) {
          this.editCell(rowidx, column);
        }
      }
    }

    this.contextMenuState = {
      ...this.contextMenuState,
      isShow: false,
    };
  }

  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    if (!this.isServerPaging) {
      this.gridData = process(this._dataBinding, this.state);
    } else {
      this.dataState_Change.emit(state);
    }
    this.selectionChange.emit(undefined);
    this.itemSelected = undefined;
  }

  ngAfterViewInit(): void {}

  onSelectionChange($event: SelectionEvent): void {
    const selected = $event.selectedRows;
    const item = selected?.[0]?.dataItem;
    if (item) {
      this.selectionChange.emit(item);
    }
  }

  getItemSelected() {
    let grid: any = this.grid;
    return grid?.selectionService?.currentSelection[
      grid.selectionService.lastSelectionStartIndex
    ]?.dataItem;
  }

  onDoubleClick($event: Event | any): void {
    const rowIndex: number = Number.parseInt(
      $event.target?.parentElement?.getAttribute('data-kendo-grid-item-index')
    );
    if (
      rowIndex != undefined &&
      !isNaN(rowIndex) &&
      typeof rowIndex === 'number'
    ) {
      this.dblClickRow.next();
    }
  }

  private isCloseCell: boolean = false;

  public closeCell() {
    this.isCloseCell = true;
    this.grid.closeCell();
    this.isCloseCell = false;
  }

  public cellCloseHandler($event: CellCloseEvent) {
    if (!this.isCloseCell) {
      $event.preventDefault();
    }
  }

  editRow(rowIndex: number, itemSelected: any) {
    this.editCell(rowIndex, 1);
  }

  formControl?: AbstractControl | null;
  columnEdit?: ColumnComponent;

  editCell(rowIndex: number, column: ColumnComponent | number) {
    if (this.isRowEditor) {
      this.takeUntilChangeCell$.next();
      let formGroup = this.formGroup;

      let _column: ColumnComponent | undefined = undefined;
      if (typeof column == 'number') {
        _column = this.grid.columns.filter((x) => x.isVisible)[
          column
        ] as ColumnComponent;
      } else {
        _column = column;
      }

      if (formGroup.get(_column.field)) {
        this.closeCell();
        setTimeout(() => {
          this.columnEdit = _column;
          formGroup.reset(this.itemSelected);
          this.formControl = formGroup.get(_column?.field || '');
          this.grid?.editCell(rowIndex, column, formGroup);

          this.formControl?.valueChanges
            .pipe(takeUntil(this.takeUntilChangeCell$))
            .subscribe((value) => {
              if (typeof value == 'boolean') {
                value = value ? 1 : 0;
              }
              let oldRecord = this.oldDatas.find(
                (x) => x[this.idField] == this.itemSelected[this.idField]
              );
              if (oldRecord) {
                oldRecord = { ...oldRecord, editMode: 'EDIT' };
                const _record = {
                  ...this.itemSelected,
                  ...{ [_column?.field || '']: value },
                  editMode: 'EDIT',
                };

                this.itemSelected[_column?.field || ''] = value;

                if (JSON.stringify(_record) != JSON.stringify(oldRecord)) {
                  if (!this.itemSelected.editMode) {
                    this.itemSelected.editMode = 'EDIT';
                  }
                  this.recordChange?.emit({
                    record: this.itemSelected,
                    editMode: 'EDIT',
                    field: _column?.field || '',
                  });
                } else if (this.itemSelected.editMode) {
                  delete this.itemSelected.editMode;
                  this.recordChange?.emit({
                    record: this.itemSelected,
                    editMode: 'NONE',
                    field: _column?.field || '',
                  });
                }
              }
              setTimeout(() => {
                this.aggregate = aggregateBy(this.getDatas(), this.aggregates);
              }, 10);
            });

          setTimeout(() => {
            const controlEl = this.el.nativeElement.querySelector(
              `.k-grid-edit-cell [controlname="${_column?.field}"],.k-grid-edit-cell [formcontrolname="${_column?.field}"]`
            );
            if (controlEl) {
              let inputEl: any = null;
              if (controlEl.tagName == 'INPUT') {
                inputEl = controlEl;
              } else {
                inputEl = controlEl.querySelector('input');
              }
              inputEl?.focus();
              inputEl?.select();
            }
          }, 1);
        }, 10);
      }
    }
  }

  toolbarActionHandler($event: Event, action: ActionType) {
    switch (action) {
      case 'ADD_ROW':
        this.addRow();
        break;
      case 'REMOVE_ROW':
        this.removeRow();
        break;
      case 'DUPLICATE':
        this.duplicateRow();
        break;
    }
    this.toolbarActionEvent.emit({ action: action, record: this.itemSelected });
  }

  getDatas() {
    const data = (this.grid?.data as GridDataResult)?.data;
    if (!this.groups.length) {
      return data;
    }

    let items: any[] = [];
    data?.forEach((group) => {
      items.push(...group.items);
    });
    return items;
  }

  removeRow() {
    let datas = this.getDatas();
    if (datas && datas.length > 0) {
      const index = datas.indexOf(this.itemSelected);
      if (index >= 0) {
        datas.splice(index, 1);
        this.recordChange?.emit({
          record: { ...this.itemSelected },
          editMode: 'DELETE',
          field: '',
        });
        if (index > 0) {
          if (index < datas.length) {
            this.itemSelected = datas[index];
          } else {
            this.itemSelected = datas[index - 1];
          }
          this.editRow(datas.indexOf(this.itemSelected), this.itemSelected);
        } else {
          this.itemSelected = undefined;
        }
        this.aggregate = aggregateBy(this.getDatas(), this.aggregates);
      }
    }
  }

  addRow() {
    let datas = this.getDatas();
    let newRecord: any = this.getNewRecord();
    newRecord.editMode = 'ADD';
    datas?.push(newRecord);
    this.itemSelected = newRecord;
    this.editRow(datas.length - 1, newRecord);

    this.recordChange?.emit({
      record: newRecord,
      editMode: 'ADD',
      field: '',
    });
    this.aggregate = aggregateBy(this.getDatas(), this.aggregates);
  }

  duplicateRow() {
    if (this.itemSelected) {
      const datas: any[] = this.getDatas();
      let newRecord = {
        ...this.itemSelected,
        [this.idField]: (this.getNewRecord() as any)[this.idField],
        editMode: 'ADD',
      };
      datas.splice(datas.indexOf(this.itemSelected) + 1, 0, newRecord);
      this.itemSelected = newRecord;
      this.editRow(datas.indexOf(this.itemSelected), newRecord);

      this.recordChange?.emit({
        record: newRecord,
        editMode: 'ADD',
        field: '',
      });
    }
    this.aggregate = aggregateBy(this.getDatas(), this.aggregates);
  }

  rowsError: any[] = [];

  @Output() dblClickRow: EventEmitter<void> = new EventEmitter();

  @Output() selectionChange: EventEmitter<any> = new EventEmitter();

  @ViewChild('contextMenu') contextMenuRef!: ElementRef;
  @ViewChild('btnExcel') btnExcelRef!: ToolBarButtonComponent;
  @HostListener('window:contextmenu', ['$event'])
  handleContextMenu(e: MouseEvent) {
    if (e.target) {
      const gridEl = (e.target as any).closest('kendo-grid');
      const gridListEl = (e.target as any).closest('kendo-grid-list');
      if (gridEl && gridEl.id == this.gridId && gridListEl) {
        const cellClicked = (e.target as any).closest('[kendogridcell]');
        if (cellClicked) {
          const row = cellClicked.closest('tr[role=row]');
          const datas = this.getDatas();
          let item = undefined;
          const rowidx =
            Number.parseInt(row.getAttribute('data-kendo-grid-item-index')) ||
            0;
          if (datas) {
            item = datas[rowidx - this.grid.skip];
          }

          if (item != this.itemSelected) {
            this.closeCell();
            this.itemSelected = item;
            this.selectionChange.emit(item);
          }
        }
        const height = this.contextMenuRef.nativeElement.offsetHeight || 0;
        const width = this.contextMenuRef.nativeElement.offsetWidth || 0;
        const wHeight = window.innerHeight;
        const wWidth = window.innerWidth;

        this.contextMenuState = {
          ...this.contextMenuState,
          isShow: true,
          clientX:
            e.clientX + width + 3 > wWidth
              ? e.clientX - width - 3
              : e.clientX + 3,
          clientY:
            e.clientY + height > wHeight ? e.clientY - height : e.clientY,
        };
        e.preventDefault();
      } else {
        this.contextMenuState = {
          ...this.contextMenuState,
          isShow: false,
        };
      }
    }
  }

  public eventEditCellKeydown: EventEmitter<{
    event: KeyboardEvent;
    fieldId: string;
  }> = new EventEmitter();

  editCellKeydown(e: { event: KeyboardEvent; fieldId: string }) {
    if (e?.event?.key == 'Tab' && this.itemSelected) {
      if (
        !e.event.shiftKey &&
        (e.event.target as any)?.className
          ?.split(' ')
          ?.indexOf('input-text-lookup') >= 0
      ) {
        return;
      }

      let nav = e.event.shiftKey
        ? this.grid.focusPrevCell(true)
        : this.grid.focusNextCell(true);

      let fn: Function | undefined = () => {
        if (!e.event.shiftKey) {
          this.addRow();
        }
      };

      if (nav) {
        while (nav?.colIndex == 0) {
          nav = e.event.shiftKey
            ? this.grid.focusPrevCell(true)
            : this.grid.focusNextCell(true);
        }
        if (nav) {
          let rowIdx =
            nav.dataRowIndex >= 0 || nav.rowIndex <= 0
              ? nav.dataRowIndex
              : nav.rowIndex - 3;
          if (rowIdx >= 0 && nav.dataItem) {
            fn = () => {
              this.itemSelected = nav.dataItem;
              this.editCell(rowIdx, nav.colIndex);
            };
          }
        }
      }

      if (fn) {
        e.event.preventDefault();
        setTimeout(fn, 100);
      }
    }
  }

  keyup(e: KeyboardEvent) {
    if (e.ctrlKey && e.key == 'F9') {
      const columnList: Array<ColumnComponent> =
        this.grid.columnList.toArray() as ColumnComponent[];

      let colResult: any[] = [];

      columnList
        .filter((x) => x.field)
        .sort((x, y) => {
          return x.leafIndex - y.leafIndex;
        })
        .forEach((x) => {
          colResult.push({
            FieldId: x.field,
            Width: x.width,
            Order: x.leafIndex,
          });
        });
      saveAs(
        `data:text/plain;base64,${encodeBase64(JSON.stringify(colResult))}`,
        `table-config-${commonFunction.newGuid()}.json`
      );

      e.preventDefault();
      e.stopPropagation();
    }
  }
}

/**
 * Định nghĩa thông tin 1 colum
 */
export interface FtsColumn {
  /**
   * Id cột ~ property của record
   */
  FieldId: string;

  /**
   * Kiểu column
   */
  ColumnType: ColumnType;

  /**
   * Tên cột
   * Không nên truyền giá trị này, mặc định lấy trong resouce theo FieldId
   */
  Text: string;

  /**
   * độ rộng cột
   */
  Width: number;

  /**
   * Số ký tự cột
   */
  Length: number;

  /**
   * Độ rộng tối thiểu khi resize
   */
  MinResizableWidth: number;

  /**
   * Cột khóa
   */
  Locked: boolean;

  /**
   * Class
   */
  ClassNames: string;

  /**
   * Format value view
   */
  Format: string;

  /**
   * data nếu column combobox
   */
  Data: any[];

  /**
   * ValueField Combo
   */
  ValueField: string;

  /**
   * TextField Combo
   */
  TextField: string;

  /**
   * tên hàm tính dòng footer vd sum
   */
  Aggregate: string;

  /**
   * Show text nếu là combo
   * false - hiển thị value là ID bình thường
   */
  ShowText: boolean;

  EditTemplateRef: TemplateRef<any>;

  Hidden: boolean;

  ReadOnly: boolean;

  ControlTextName: string;
}

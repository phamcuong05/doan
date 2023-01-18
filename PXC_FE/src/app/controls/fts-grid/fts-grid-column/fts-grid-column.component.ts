import {
  Component,
  ContentChild,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  TemplateRef,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import { VirtualizationSettings } from '@progress/kendo-angular-dropdowns';
import {
  CellTemplateDirective,
  ColumnSortSettings,
  EditTemplateDirective,
  FilterCellTemplateDirective,
  FilterService,
  FooterTemplateDirective,
  GroupHeaderTemplateDirective,
} from '@progress/kendo-angular-grid';
import { IntlService } from '@progress/kendo-angular-intl';
import { LocalizationService } from '@progress/kendo-angular-l10n';
import { CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ResourceManager } from 'src/app/common/resource-manager';
import { ColumnType } from 'src/app/common/types';
import { FtsColumn } from '../fts-grid.component';

@Component({
  selector: 'fts-grid-column',
  templateUrl: './fts-grid-column.component.html',
  encapsulation: ViewEncapsulation.None,
  providers: [LocalizationService, FilterService],
})
export class FtsGridColumnComponent implements OnInit, OnDestroy {
  // cellTemplate
  groupHeaderTemplateDirective?: GroupHeaderTemplateDirective;
  @ViewChild(GroupHeaderTemplateDirective)
  _groupHeaderTemplateDirectiveViewChild?: GroupHeaderTemplateDirective;
  @ContentChild(GroupHeaderTemplateDirective)
  _groupHeaderTemplateDirectiveContentChild?: GroupHeaderTemplateDirective;

  // cellTemplate
  cellTemplateDirective?: CellTemplateDirective;
  @ViewChild(CellTemplateDirective)
  _cellTemplateDirectiveViewChild?: CellTemplateDirective;
  @ContentChild(CellTemplateDirective)
  _cellTemplateDirectiveContentChild?: CellTemplateDirective;

  // filter Template
  filterCellTemplateDirective?: FilterCellTemplateDirective;
  @ViewChild(FilterCellTemplateDirective)
  _filterCellTemplateDirectiveViewChild?: FilterCellTemplateDirective;
  @ContentChild(CellTemplateDirective)
  _filterCellTemplateDirectiveContentChild?: FilterCellTemplateDirective;

  //editor
  editTemplateDirective?: EditTemplateDirective;
  @ViewChild(EditTemplateDirective)
  _editTemplateDirectiveViewChild?: EditTemplateDirective;
  @ContentChild(EditTemplateDirective)
  _editTemplateDirectiveContentChild?: EditTemplateDirective;

  //footer
  footerTemplateDirective?: FooterTemplateDirective;
  @ViewChild(FooterTemplateDirective)
  _footerTemplateDirectiveViewChild?: FooterTemplateDirective;
  @ContentChild(FooterTemplateDirective)
  _footerTemplateDirectiveContentChild?: FooterTemplateDirective;

  private _colConfig: FtsColumn = {
    ShowText: true,
    MinResizableWidth: 100,
  } as FtsColumn;
  @Input() set colConfig(v: FtsColumn) {
    this._colConfig = v;
    
    if (this._colConfig.MinResizableWidth == undefined) {
      this._colConfig.MinResizableWidth = 0;
    }
  }

  get colConfig(): FtsColumn {
    return this._colConfig;
  }

  /**
   * tên biểu thức dòng tổng
   */
  @Input() set aggregate(v: string) {
    this._colConfig.Aggregate = v;
  }

  get aggregate(): string {
    return this._colConfig.Aggregate;
  }

  /**
   * fieldId
   */
  @Input() set fieldId(v: string) {
    this._colConfig.FieldId = v;
  }

  get fieldId(): string {
    return this._colConfig.FieldId;
  }

  /**
   * format
   */
  @Input() set format(v: string) {
    this._colConfig.Format = v;
  }

  get format(): string {
    return this._colConfig.Format;
  }

  /**
   * field value của combobox với cột filter combobox
   */
  @Input() set valueField(v: string) {
    this._colConfig.ValueField = v;
  }

  get valueField(): string {
    return this._colConfig.ValueField;
  }

  /**
   * field hiển thị của combobox với cột filter combobox
   */
  @Input() set textField(v: string) {
    this._colConfig.TextField = v;
  }

  get textField(): string {
    return this._colConfig.TextField;
  }

  /**
   * dataSource của combobox filter
   */
  @Input() set dataSource(v: any[]) {
    this._colConfig.Data = v;
  }

  get dataSource(): any[] {
    return this._colConfig.Data;
  }

  /**
   * Show text nếu là combo
   * false - hiển thị value là ID bình thường
   */
  @Input() set showText(v: boolean) {
    this._colConfig.ShowText = v;
  }

  get showText(): boolean {
    return this._colConfig.ShowText;
  }

  /**
   * Tiêu đề cột
   */
  @Input() set title(v: string) {
    this._colConfig.Text = v;
  }

  get title(): string {
    return this._colConfig.Text;
  }

  /**
   * Chỉ đọc
   */
  @Input() set readOnly(v: boolean) {
    this._colConfig.ReadOnly = v;
  }

  get readOnly(): boolean {
    return this._colConfig.ReadOnly;
  }

  //#region <width> độ rộng cột

  /**
   * Chỉ đọc
   */
  @Input() set width(v: number) {
    if (v != undefined) this._colConfig.Width = v;
  }

  get width(): number {
    if (this._colConfig.Length) {
      return 8.25 * this._colConfig.Length + 40;
    } else if (!this._colConfig.Width && this._colConfig.ColumnType == 'date') {
      return 105;
    }
    return this._colConfig.Width;
  }
  //#endregion

  //#region <minResizableWidth> Độ rộng tối thiểu được phép resize
  @Input() set minResizableWidth(v: number) {
    if (v != undefined) this._colConfig.MinResizableWidth = v;
  }

  get minResizableWidth(): number {
    return this._colConfig.MinResizableWidth;
  }
  //#endregion

  /**
   * editTemplateRef
   */
  @Input() set editTemplateRef(v: TemplateRef<any>) {
    this._colConfig.EditTemplateRef = v;
  }

  get editTemplateRef(): TemplateRef<any> {
    return this._colConfig.EditTemplateRef;
  }

  @Input() set hidden(v: boolean) {
    this._colConfig.Hidden = v;
  }

  get hidden(): boolean {
    return this._colConfig.Hidden;
  }

  //#region <type> Kiểu column

  @Input() set type(v: ColumnType) {
    if (v != undefined) this._colConfig.ColumnType = v;
    else this._colConfig.ColumnType = 'default';
  }

  get type(): ColumnType {
    return this._colConfig.ColumnType || 'default';
  }

  //#endregion

  @Input() set class(v: string) {
    this._colConfig.ClassNames = v;
  }

  get class(): string {
    let _class = '';
    switch (this.type) {
      case 'boolean':
        _class = 'text-center ';
        break;
      case 'numeric':
        _class = 'text-right ';
        break;
        /* case 'date':
        _class = 'text-center '; */
        break;
    }
    return _class + this._colConfig.ClassNames;
  }

  @Input() sortable: boolean | ColumnSortSettings = true;

  @Input() filterable: boolean = true;

  @Input() filter: CompositeFilterDescriptor = {
    logic: 'and',
    filters: [],
  };
  @Output() filterChange: EventEmitter<CompositeFilterDescriptor> =
    new EventEmitter();

  private onDestroy$ = new Subject<void>();
  constructor(
    public resourceManager: ResourceManager,
    public intl: IntlService,
    private filterService: FilterService
  ) {
    let that = this;
    that.filterService.changes
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((state) => {
        that.filter = state;
        that.filterChange.emit(that.filter);
      });
  }
  ngOnDestroy(): void {
    this.onDestroy$.next();
  }
  ngOnInit(): void {}

  initValue() {}

  ngAfterViewInit(): void {
    setTimeout(() => {
      //gán cell template
      this.cellTemplateDirective =
        this._cellTemplateDirectiveContentChild ||
        this._cellTemplateDirectiveViewChild;

      //gán filter template
      this.filterCellTemplateDirective =
        this._filterCellTemplateDirectiveContentChild ||
        this._filterCellTemplateDirectiveViewChild;

      //gán editor template
      this.editTemplateDirective =
        this._editTemplateDirectiveContentChild ||
        this._editTemplateDirectiveViewChild;

      //gán footer template
      this.footerTemplateDirective =
        this._footerTemplateDirectiveContentChild ||
        this._footerTemplateDirectiveViewChild;

      //gán group header template
      this.groupHeaderTemplateDirective =
        this._groupHeaderTemplateDirectiveContentChild ||
        this._groupHeaderTemplateDirectiveViewChild;
    }, 1);
  }

  public virtual: VirtualizationSettings = {
    itemHeight: 26,
    pageSize: 10,
  };

  findList(list: any[], value: any, field: string) {
    return list?.find((x) => x[field] == value);
  }
}

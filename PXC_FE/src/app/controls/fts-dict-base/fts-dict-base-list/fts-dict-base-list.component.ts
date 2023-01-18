import {
  ChangeDetectorRef,
  Component,
  ContentChild,
  Input,
  OnInit,
  TemplateRef,
  ViewChild,
  ViewEncapsulation
} from '@angular/core';
import {
  DataStateChangeEvent,
  GridDataResult
} from '@progress/kendo-angular-grid';
import {
  CompositeFilterDescriptor,
  SortDescriptor
} from '@progress/kendo-data-query';
import { combineLatest, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { EnumLoadingState } from 'src/app/common/enum';
import { ActionType } from 'src/app/common/types';
import { DictBaseDetailDirective } from '../../../directive/fts-dict-base-detail-directive';
import { FtsGridComponent } from '../../fts-grid/fts-grid.component';
import { FtsImportExcelComponent } from '../../fts-import-excel/fts-import-excel.component';
import { FtsDictBaseListInject } from './fts-dict-base-list-inject';

@Component({
  selector: 'fts-dict-base-list',
  templateUrl: './fts-dict-base-list.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class FtsDictBaseListComponent implements OnInit {
  // cho phép hiển thị nút thêm
  @Input() showToolbarAdd: boolean = true;
  // cho phép hiển thị nút sửa
  @Input() showToolbarEdit: boolean = true;
  // cho phép hiển thị nút xóa
  @Input() showToolbarDelete: boolean = true;

  @Input() showToolbarExcel: boolean = true;

  @Input() showToolbarImport: boolean = true;

  @Input() showToolbarRefresh: boolean = true;
  
  @Input() showButtonConfigColumn: boolean = true;

  

  vm$!: Observable<{
    sortable: boolean;
    filterable: boolean;
    gridData: GridDataResult;
    columns: Array<any>;
    filter: CompositeFilterDescriptor;
    sort: Array<SortDescriptor>;
  }>;

  @ViewChild('dictBaseDetailTemplate')
  dictBaseDetailTemplateRef!: TemplateRef<any>;

  @ViewChild('grdListing')
  grdListing!: FtsGridComponent;

  @ContentChild(DictBaseDetailDirective, { static: false })
  dictBaseDetailDirective!: DictBaseDetailDirective;

  @ViewChild(FtsImportExcelComponent)
  importExcel!: FtsImportExcelComponent;

  constructor(
    public myInject: FtsDictBaseListInject,
    public changeDetectorRef: ChangeDetectorRef
  ) {
    this.vm$ = combineLatest([
      this.myInject.dictBaseListStore.sortable$,
      this.myInject.dictBaseListStore.filterable$,
      this.myInject.dictBaseListStore.gridData$,
      this.myInject.dictBaseListStore.columns$,
      this.myInject.dictBaseListStore.filter$,
      this.myInject.dictBaseListStore.sort$,
    ]).pipe(
      map(([sortable, filterable, gridData, columns, filter, sort]) => {
        return {
          sortable: sortable,
          filterable: filterable,
          gridData: gridData,
          columns: columns,
          filter: filter,
          sort: sort,
        };
      })
    );
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {}

  ngAfterViewInit(): void {}

  @Input() pageResource: any;

  onDoubleClickRow(): void {
    this.myInject.dictBaseListStore.actionClick.emit([
      'VIEW',
      EnumLoadingState.Loading,
    ]);
  }

  onSelectionChange(itemSelected: any) {
    this.myInject.dictBaseListStore.setCurrentRow(itemSelected);
  }

  toolbarActionHandler($event: { action: ActionType; record: any }) {
    this.myInject.dictBaseListStore.actionClick.emit([
      $event.action,
      EnumLoadingState.Loading,
    ]);
  }

  dataStateChange(state: DataStateChangeEvent) {
    if (state.filter) {
      this.myInject.dictBaseListStore.setFilter(state.filter);
    }
    if (state.sort) {
      this.myInject.dictBaseListStore.setSort(state.sort);
    }
    this.myInject.dictBaseListStore.dataStateChange.emit(state);
  }
}

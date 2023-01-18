import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import {
  DataStateChangeEvent,
  GridDataResult
} from '@progress/kendo-angular-grid';
import { TooltipDirective } from '@progress/kendo-angular-tooltip';
import {
  CompositeFilterDescriptor,
  SortDescriptor
} from '@progress/kendo-data-query';
import { combineLatest, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { EnumLoadingState } from 'src/app/common/enum';
import { ActionType } from 'src/app/common/types';
import { FtsGridComponent } from '../fts-grid/fts-grid.component';
import { FtsImportExcelComponent } from '../fts-import-excel/fts-import-excel.component';
import { FtsEditListBaseInject } from './fts-edit-list-base-inject';

@Component({
  selector: 'fts-edit-list-base',
  templateUrl: './fts-edit-list-base.component.html',
  styleUrls: ['./fts-edit-list-base.component.scss'],
})
export class FtsEditListBaseComponent implements OnInit {
  @ViewChild(TooltipDirective) tooltipDir!: TooltipDirective;

  // cho phép hiển thị nút lưu
  @Input() showToolbarSave: boolean = false;
  // cho phép hiển thị nút thêm
  @Input() showToolbarAdd: boolean = true;
  // cho phép hiển thị nút sửa
  @Input() showToolbarEdit: boolean = true;
  // cho phép hiển thị nút xóa
  @Input() showToolbarDelete: boolean = true;

  @Input() showToolbarExcel: boolean = true;

  @Input() showToolbarImport: boolean = true;
  @Input() pageResource: any;

  @ViewChild(FtsImportExcelComponent)
  importExcel!: FtsImportExcelComponent;

  @Input() formGroupEditRow!: FormGroup;

  @Input() getNewRecord = (): object => {
    return {};
  };

  @ViewChild('grdListing') grdListing!: FtsGridComponent;

  vm$!: Observable<{
    sortable: boolean;
    filterable: boolean;
    gridData: GridDataResult;
    columns: Array<any>;
    filter: CompositeFilterDescriptor;
    sort: Array<SortDescriptor>;
    idField: string;
  }>;

  public oldDatas: any[] = [];

  constructor(
    public myInject: FtsEditListBaseInject,
  ) {}

  ngOnInit(): void {
    this.vm$ = combineLatest([
      this.myInject.editListBaseStore.sortable$,
      this.myInject.editListBaseStore.filterable$,
      this.myInject.editListBaseStore.gridData$,
      this.myInject.editListBaseStore.columns$,
      this.myInject.editListBaseStore.filter$,
      this.myInject.editListBaseStore.sort$,
      this.myInject.editListBaseStore.idField$,
    ]).pipe(
      map(
        ([sortable, filterable, gridData, columns, filter, sort, idField]) => {
          return {
            sortable: sortable,
            filterable: filterable,
            gridData: gridData,
            columns: columns,
            filter: filter,
            sort: sort,
            idField: idField,
          };
        }
      )
    );
  }

  onSelectionChange(itemSelected: any) {
    this.myInject.editListBaseStore.setCurrentRow(itemSelected);
  }
  toolbarActionHandler($event: { action: ActionType; record: any }) {
    const { action, record } = $event;
    switch (action) {
      case 'ADD':
        this.grdListing?.addRow();
        break;
      case 'DELETE':
        this.grdListing?.removeRow();
        break;
      default:
        this.myInject.editListBaseStore.actionClick.emit([
          $event.action,
          EnumLoadingState.Loading,
        ]);
        break;
    }
  }

  dataStateChange(state: DataStateChangeEvent) {
    if (state.filter) {
      this.myInject.editListBaseStore.setFilter(state.filter);
    }
    if (state.sort) {
      this.myInject.editListBaseStore.setSort(state.sort);
    }
    this.myInject.editListBaseStore.dataStateChange.emit(state);
  }
}

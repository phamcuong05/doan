import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { SysTableService } from 'src/app/model/system/sys-table/sys-table-service';
import { SysTableDetailComponent } from '../sys-table-detail/sys-table-detail.component';

@Component({
  selector: 'sys-table-list',
  templateUrl: './sys-table-list.component.html',
  styleUrls: ['./sys-table-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class SysTableListComponent extends FtsDictBaseList {
  override tableName: string = 'SYS_TABLE';

  constructor(myService: SysTableService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = SysTableDetailComponent;
  columns = [
    { FieldId: 'TABLE_NAME', Width: 170 } as FtsColumn,
    { FieldId: 'ID_FIELD', Width: 156 },
    { FieldId: 'NAME_FIELD', Width: 176 },
    { FieldId: 'TABLE_TYPE', Width: 82 },
    { FieldId: 'CAN_GROUP', ColumnType: 'boolean', Width: 113 },
    { FieldId: 'ID_AUTO', ColumnType: 'boolean', Width: 98 },
    { FieldId: 'ID_MASK', Width: 118 },
    { FieldId: 'ID_LENGTH', ColumnType: 'numeric', Format: 'n0', Width: 122 },
    { FieldId: 'ID_PARTS', ColumnType: 'numeric', Format: 'n0', Width: 122 },
    { FieldId: 'ID_SPLIT', Width: 119 },
    { FieldId: 'LOAD_BY_SEARCH', ColumnType: 'boolean', Width: 134 },
  ] as Array<FtsColumn>;

  idField = 'TABLE_NAME';
  nameField = 'ID_FIELD';

  ngOnInit(): void {
    this.dictBaseListcomponent.showToolbarAdd = false;
    this.dictBaseListcomponent.showToolbarDelete = false;
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

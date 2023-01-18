import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmWarehouseService } from 'src/app/model/dictionary/dm-warehouse/dm-warehouse-service';
import { DmWarehouseDetailComponent } from '../dm-warehouse-detail/dm-warehouse-detail.component';

@Component({
  selector: 'dm-warehouse-list',
  templateUrl: './dm-warehouse-list.component.html',
  styleUrls: ['./dm-warehouse-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmWarehouseListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_WAREHOUSE';

  constructor(myService: DmWarehouseService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmWarehouseDetailComponent;
  columns = [
    {
      FieldId: 'WAREHOUSE_ID',
      Length: 20,
    },
    {
      FieldId: 'WAREHOUSE_NAME',
      Width: 250,
    },
    {
      FieldId: 'DEPARTMENT_NAME',
      Width: 250,
    },
    {
      FieldId: 'IS_USE_WAREHOUSE',
      ColumnType: 'boolean',
      ClassNames: 'text-center',
      Width: 120,
    } as FtsColumn,
    {
      FieldId: 'USER_ID',
      Length: 15,
    },
    {
      FieldId: 'ACTIVE',
      ColumnType: 'boolean',
      ClassNames: 'text-center',
      Width: 80,
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'WAREHOUSE_ID';
  nameField = 'WAREHOUSE_NAME';
  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

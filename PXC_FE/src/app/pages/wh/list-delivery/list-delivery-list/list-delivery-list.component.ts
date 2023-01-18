import { Component, OnInit } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { ListDeliveryService } from 'src/app/model/wh/list-delivery/list-delivery.service';

@Component({
  selector: 'list-delivery-list',
  templateUrl: './list-delivery-list.component.html',
  styleUrls: ['./list-delivery-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class ListDeliveryListComponent extends FtsDictBaseList {

  constructor(
    myService: ListDeliveryService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }
  
  columns = [
    { FieldId: 'WARE_HOUSE_NAME', Width: 200 }, 
    {
      FieldId: 'FROM_DATE',
      ColumnType: 'date',
      Format: this.myInject.FTSMain.dateFormat,
      Width: 120
    },
    { FieldId: 'TOTAL_ORDER', 
      Width: 100,
    },
    { FieldId: 'WEIGHT', 
      Width: 100,
      ColumnType: 'numeric'
    },
    { FieldId: 'ORGANIZATION_ID', Width: 120 }, 
    { FieldId: 'ORGANIZATION_NAME', Width: 200 },
    { FieldId: 'NOTE', Width: 200 } as FtsColumn,
    { FieldId: 'USER_ID', Width: 100 },
  ] as Array<FtsColumn>;
  idField: string = 'WARE_HOUSE_ID';
  nameField: string = 'WARE_HOUSE_NAME';
  tableName: string = 'LIST_DELIVERY';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }

}

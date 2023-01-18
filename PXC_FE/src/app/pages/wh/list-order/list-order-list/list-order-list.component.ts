import { Component, OnInit } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { ListOrderService } from 'src/app/model/wh/list-order/list-order.service';

@Component({
  selector: 'list-order-list',
  templateUrl: './list-order-list.component.html',
  styleUrls: ['./list-order-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class ListOrderListComponent extends FtsDictBaseList {

  constructor(
    myService: ListOrderService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }
  
  columns = [
    { FieldId: 'ORDER_ID', Width: 100 },
    { FieldId: 'PACKAGE_NAME', Width: 200 }, 
    {
      FieldId: 'ORDER_DATE',
      ColumnType: 'date',
      Format: this.myInject.FTSMain.dateFormat,
      Width: 120
    },
    { FieldId: 'SERVICE_CHARGE_NAME', Width: 200 }, 
    {
      FieldId: 'TOTAL',
      ColumnType: 'numeric',
      Format: this.myInject.FTSMain.amountFormat,
      Width: 120
    },
    { FieldId: 'CUSTOMER_NAME', Width: 200 }, 
    { FieldId: 'PHONE', Width: 120 }, 
    { FieldId: 'ADDRESS', Width: 350 } as FtsColumn,
    { FieldId: 'USER_ID', Width: 100 },
  ] as Array<FtsColumn>;
  idField: string = 'ORDER_ID';
  nameField: string = 'PACKAGE_NAME';
  tableName: string = 'LIST_ORDER';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }


}

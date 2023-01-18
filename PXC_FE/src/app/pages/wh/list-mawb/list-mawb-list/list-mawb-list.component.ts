import { Component, OnInit } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { ListMawbService } from 'src/app/model/wh/list-mawb/list-mawb.service';
import { ListOrderService } from 'src/app/model/wh/list-order/list-order.service';

@Component({
  selector: 'list-mawb-list',
  templateUrl: './list-mawb-list.component.html',
  styleUrls: ['./list-mawb-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class ListMawbListComponent extends FtsDictBaseList {

  constructor(
    myService: ListMawbService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }
  
  columns = [
    { FieldId: 'MAWB_ID', Width: 100 },
    { FieldId: 'MAWB_NAME', Width: 120 }, 
    { FieldId: 'DEPARTURE', Width: 100 },
    { FieldId: 'DESTINATION', Width: 120 }, 
    {
      FieldId: 'TOTAL_ORDER',
      Width: 120
    },
    {
      FieldId: 'WEIGHT',
      ColumnType: 'numeric',
      Width: 120
    },
    {
      FieldId: 'CREATE_DATE',
      ColumnType: 'date',
      Format: this.myInject.FTSMain.dateFormat,
      Width: 120
    } as FtsColumn,
    { FieldId: 'USER_ID', Width: 100 },
  ] as Array<FtsColumn>;
  idField: string = 'MAWB_ID';
  nameField: string = 'MAWB_NAME';
  tableName: string = 'LIST_MAWB';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }

}

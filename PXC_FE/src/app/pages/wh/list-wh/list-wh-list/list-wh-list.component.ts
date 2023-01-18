import { Component, OnInit } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { ListWhService } from 'src/app/model/wh/list-wh/list-wh.service';

@Component({
  selector: 'list-wh-list',
  templateUrl: './list-wh-list.component.html',
  styleUrls: ['./list-wh-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class ListWhListComponent extends FtsDictBaseList {

  constructor(
    myService: ListWhService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }
  
  columns = [
    { FieldId: 'WARE_HOUSE_NAME', Width: 200 }, 
    {
      FieldId: 'TO_DATE',
      ColumnType: 'date',
      Format: this.myInject.FTSMain.dateFormat,
      Width: 120
    },
    { FieldId: 'CONTAINER_ID', Width: 120 }, 
    { FieldId: 'CONTAINER_NAME', Width: 200 }, 
    { FieldId: 'MAWB_ID', Width: 120 }, 
    { FieldId: 'MAWB_NAME', Width: 200 }, 
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
  tableName: string = 'LIST_WH';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

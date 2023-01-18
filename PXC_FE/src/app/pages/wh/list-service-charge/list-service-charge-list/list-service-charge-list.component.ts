import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { ListServiceChargeService } from 'src/app/model/wh/list-service-charge/list-service-charge.service';

@Component({
  selector: 'list-service-charge-list',
  templateUrl: './list-service-charge-list.component.html',
  styleUrls: ['./list-service-charge-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class ListServiceChargeListComponent extends FtsDictBaseList {

  
  constructor(
    myService: ListServiceChargeService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }

  columns = [
    { FieldId: 'SERVICE_CHARGE_ID', Width: 120 },
    { FieldId: 'SERVICE_CHARGE_NAME', Width: 120 },
    { FieldId: 'SHIP_FEE',
      Width: 120,
      ColumnType: 'numeric',
      Format: this.myInject.FTSMain.amountFormat,   
    },
    { FieldId: 'REGION', Width: 120 },
    { FieldId: 'DESCRIPTION', Width: 220 },
    {
      FieldId: 'CREATE_DATE',
      ColumnType: 'date',
      Format: this.myInject.FTSMain.dateFormat,
      Width: 120
    } as FtsColumn,
    { FieldId: 'USER_ID', Width: 120 },
  ] as Array<FtsColumn>;
  idField: string = 'SERVICE_CHARGE_ID';
  nameField: string = 'SERVICE_CHARGE_NAME';
  tableName: string = 'LIST_SERVICE_CHARGE';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }

}

import { Component, OnInit } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmCashbankLimitService } from 'src/app/model/dictionary/dm-cashbank-limit/dm-cashbank-limit.service';

@Component({
  selector: 'dm-cashbank-limit-list',
  templateUrl: './dm-cashbank-limit-list.component.html',
  styleUrls: ['./dm-cashbank-limit-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmCashbankLimitListComponent extends FtsDictBaseList {

  constructor(myService: DmCashbankLimitService, myInject: FtsDictBaseListInject) { 
    super(myService, myInject);
  }

  columns = [
    { FieldId: 'ORGANIZATION_ID', Width: 120 },
    { FieldId: 'ACCOUNT_ID', Width: 120 },
    { 
      FieldId: 'ACCOUNT_NAME', 
      Width: 250 
    },   
    { 
      FieldId: 'BANK_ID', 
      Width: 120 
    },
    { 
      FieldId: 'BANK_NAME', 
      Width: 250 
    },   
    { 
      FieldId: 'VALID_DATE',
      ColumnType: 'date',
      Format: 'dd/MM/yyyy',
      Width: 120
    },   
    { FieldId: 'LIMIT', 
      Width: 120 
    },   
    { FieldId: 'NOTES', 
      Width: 120
    } as FtsColumn,   
    { FieldId: 'USER_ID', Width: 135 },
  ] as Array<FtsColumn>;
  idField : string = 'PR_KEY';
  nameField : string = 'ORGANIZATION_ID';
  tableName = 'DM_CASHBANK_LIMIT';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }

}

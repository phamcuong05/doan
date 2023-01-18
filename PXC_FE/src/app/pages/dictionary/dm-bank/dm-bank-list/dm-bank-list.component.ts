import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmBankService } from 'src/app/model/dictionary/dm-bank/dm-bank-service';
import { DmBankDetailComponent } from '../dm-bank-detail/dm-bank-detail.component';

@Component({
  selector: 'dm-bank-list',
  templateUrl: './dm-bank-list.component.html',
  styleUrls: ['./dm-bank-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmBankListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_BANK';
  constructor(myService: DmBankService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmBankDetailComponent;
  columns = [
    { FieldId: 'BANK_ID', Length: 20 },
    { FieldId: 'BANK_NAME' },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'BANK_ID';
  nameField = 'BANK_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

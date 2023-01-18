import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmPrDetailService } from 'src/app/model/dictionary/dm-pr-detail/dm-pr-detail-service';

@Component({
  selector: 'dm-pr-detail-list',
  templateUrl: './dm-pr-detail-list.component.html',
  styleUrls: ['./dm-pr-detail-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmPrDetailListComponent extends FtsDictBaseList {
  constructor(myService: DmPrDetailService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }
  columns = [
    { FieldId: 'PR_DETAIL_ID', Length: 11 } as FtsColumn,
    { FieldId: 'PR_DETAIL_NAME', Length: 40 },
    { FieldId: 'IDENTITY_NO', Length: 13 },
    { FieldId: 'PR_DETAIL_CLASS_NAME', Length: 25 },
    { FieldId: 'PR_DETAIL_CLASS1_NAME', Length: 25 },
    { FieldId: 'EMAIL', Length: 25 },
    { FieldId: 'ADDRESS', Length: 40 },
    { FieldId: 'BANK_NAME', Length: 25 },
    { FieldId: 'BANK_ACCOUNT_NAME', Length: 20 },
    { FieldId: 'BANK_ACCOUNT_NO', Length: 20 },
    { FieldId: 'BANK_BRANCH', Length: 20 },
    { FieldId: 'TAX_FILE_NUMBER', Length: 14 },
    {
      FieldId: 'CREATE_DATE',
      ColumnType: 'date',
      Format: 'dd/MM/yyyy',
    },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    },
  ] as Array<FtsColumn>;
  idField = 'PR_DETAIL_ID';
  nameField = 'PR_DETAIL_NAME';
  tableName = 'DM_PR_DETAIL';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

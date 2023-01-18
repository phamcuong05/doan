import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmSecurityService } from 'src/app/model/dictionary/dm-security/dm-security-service';

@Component({
  selector: 'dm-security-list',
  templateUrl: './dm-security-list.component.html',
  styleUrls: ['./dm-security-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmSecurityListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_SECURITY';

  constructor(myService: DmSecurityService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }
  columns = [
    { FieldId: 'SECURITY_ID', Width: 115 },
    { FieldId: 'SECURITY_NAME', Width: 250 },
    {
      FieldId: 'SECURITY_CLASS_ID',
      Width: 125,
    },
    {
      FieldId: 'BOOK_UNIT_PRICE_ORIG',
      ColumnType: 'numeric',
      Format: this.myInject.FTSMain.amountOrigFormat,
      Width: 100,
    },
    {
      FieldId: 'CURRENCY_ID',
      Width: 70,
    },
    {
      FieldId: 'PR_DETAIL_ID',
      Width: 130,
    },
    {
      FieldId: 'PERIOD_ID',
      Width: 60,
    },
    {
      FieldId: 'ISSUE_DATE',
      ColumnType: 'date',
      Format: this.myInject.FTSMain.dateFormat,
      Width: 110,
    },
    {
      FieldId: 'MATURITY_DATE',
      ColumnType: 'date',
      Format: this.myInject.FTSMain.dateFormat,
      Width: 105,
    },
    {
      FieldId: 'SHORT_TERM_COST_ACCOUNT_ID',
      Width: 135,
    },
    {
      FieldId: 'SHORT_TERM_PROFIT_ACCOUNT_ID',
      Width: 108,
    },
    {
      FieldId: 'SHORT_TERM_LOSS_ACCOUNT_ID',
      Width: 100,
    },
    {
      FieldId: 'SHORT_TERM_RESERVE_ACCOUNT_ID',
      Width: 150,
    },
    {
      FieldId: 'LONG_TERM_COST_ACCOUNT_ID',
      Width: 120,
    },
    {
      FieldId: 'LONG_TERM_PROFIT_ACCOUNT_ID',
      Width: 100,
    },
    {
      FieldId: 'LONG_TERM_LOSS_ACCOUNT_ID',
      Width: 90,
    },
    {
      FieldId: 'LONG_TERM_RESERVE_ACCOUNT_ID',
      Width: 140,
    },
    { FieldId: 'USER_ID', Width: 90 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'SECURITY_ID';
  nameField = 'SECURITY_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

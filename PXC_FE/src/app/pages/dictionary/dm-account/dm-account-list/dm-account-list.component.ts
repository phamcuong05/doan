import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmAccountService } from 'src/app/model/dictionary/dm-account/dm-account-service';
import { DmAccountDetailComponent } from '../dm-account-detail/dm-account-detail.component';

@Component({
  selector: 'dm-account-list',
  templateUrl: './dm-account-list.component.html',
  styleUrls: ['./dm-account-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmAccountListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_ACCOUNT';
  constructor(myService: DmAccountService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmAccountDetailComponent;
  columns = [
    { FieldId: 'ACCOUNT_ID', Length: 10 },
    { FieldId: 'ACCOUNT_NAME', Width: 400 },
    { FieldId: 'PARENT_ACCOUNT_ID', Length: 10 },
    {
      FieldId: 'IS_PARENT',
      ColumnType: 'boolean',
      ClassNames: 'text-center',
      Width: 100,
    } as FtsColumn,
    {
      FieldId: 'CURRENCY_ID',
      Width: 120,
    },
    {
      FieldId: 'IS_OOB',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_PR_DETAIL',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_EXPENSE',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_JOB',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_BANK',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_EMPLOYEE',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_DEPARTMENT',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_AGENT',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_INSURANCE_SOURCE',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_ITEM',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_CONTRACT',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_CAPITAL_SOURCE',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_REINSURANCE_SOURCE',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    {
      FieldId: 'IS_VAT',
      Width: 100,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
    { FieldId: 'USER_ID', Width: 150 },

    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'ACCOUNT_ID';
  nameField = 'ACCOUNT_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmCurrencyService } from 'src/app/model/dictionary/dm-currency/dm-currency-service';
import { DmCurrencyDetailComponent } from '../dm-currency-detail/dm-currency-detail.component';

@Component({
  selector: 'dm-currency-list',
  templateUrl: './dm-currency-list.component.html',
  styleUrls: ['./dm-currency-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmCurrencyListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_CURRENCY';
  constructor(myService: DmCurrencyService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmCurrencyDetailComponent;
  columns = [
    { FieldId: 'CURRENCY_ID', Length: 20 },
    { FieldId: 'CURRENCY_NAME' },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'CURRENCY_ID';
  nameField = 'CURRENCY_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

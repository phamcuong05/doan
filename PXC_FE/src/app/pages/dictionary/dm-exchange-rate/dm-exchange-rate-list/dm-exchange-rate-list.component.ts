import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { DmExchangeRateService } from 'src/app/model/dictionary/dm-exchange-rate/dm-exchange-rate-service';

@Component({
  selector: 'dm-exchange-rate-list',
  templateUrl: './dm-exchange-rate-list.component.html',
  styleUrls: ['./dm-exchange-rate-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmExchangeRateListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_EXCHANGE_RATE';
  constructor(
    myService: DmExchangeRateService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }

  columns: FtsColumn[] = [
    {
      FieldId: 'VALID_DATE',
      Width: 200,
      ColumnType: 'date',
      Format: 'dd/MM/yyyy',
    },
    { FieldId: 'CURRENCY_ID', Width: 200 },
    { FieldId: 'TO_CURRENCY_ID', Width: 200 },
    {
      FieldId: 'EXCHANGE_RATE',
      Width: 200,
      ColumnType: 'numeric',
      Format: this.myInject.FTSMain.exRateFormat,
    },
  ] as FtsColumn[];
  idField: string = 'PR_KEY';
  nameField: string = 'CURRENCY_ID';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override setParamBeforLoad(param: PagingParam): PagingParam {
    if (!param.Sorts) {
      param.Sorts = [
        {
          Field: 'VALID_DATE',
          Dir: 'ASC',
        },
        {
          Field: 'CURRENCY_ID',
          Dir: 'ASC',
        },
        {
          Field: 'TO_CURRENCY_ID',
          Dir: 'ASC',
        },
      ];
    }
    return param;
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

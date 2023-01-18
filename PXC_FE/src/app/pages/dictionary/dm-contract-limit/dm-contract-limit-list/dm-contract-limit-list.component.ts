import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { DmContractLimitService } from 'src/app/model/dictionary/dm-contract-limit/dm-contract-limit-service';
import { DmContractLimitDetailComponent } from '../dm-contract-limit-detail/dm-contract-limit-detail.component';

@Component({
  selector: 'dm-contract-limit-list',
  templateUrl: './dm-contract-limit-list.component.html',
  styleUrls: ['./dm-contract-limit-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmContractLimitListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_CONTRACT_CLASS';

  constructor(
    myService: DmContractLimitService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }

  detailComponent = DmContractLimitDetailComponent;
  columns = [
    {
      FieldId: 'ORGANIZATION_ID',
      Width: 134,
    },
    {
      FieldId: 'PR_DETAIL_ID', Width: 140,
    },
    {
      FieldId: 'PR_DETAIL_NAME', Width: 300,
    },
    {
      FieldId: 'VALID_DATE', Width: 110,
      ColumnType: 'date',
      Aggregate: 'sum'
    } as FtsColumn,
    {
      FieldId: 'AMOUNT', Width: 140,
      ColumnType: 'numeric'
    } as FtsColumn,
    {
      FieldId: 'NOTES', Width: 300,
    },
    { FieldId: 'USER_ID', Length: 15 },
  ] as Array<FtsColumn>;
  idField = 'PR_KEY';
  nameField = 'PR_DETAIL_ID';

  override setParamBeforLoad(param: PagingParam): PagingParam {
    if (param) {
      if (!param.FilterGroups) {
        param.FilterGroups = [];
      }

      if (!param.Sorts) {
        param.Sorts = [
          {
            Dir: 'DESC',
            Field: 'VALID_DATE',
          },
        ];
      }

      param.SumaryFields = ['AMOUNT'];
    }

    return param;
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

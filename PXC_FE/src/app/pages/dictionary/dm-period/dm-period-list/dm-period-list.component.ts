import { Component } from '@angular/core';
import { EnumLoadingState } from 'src/app/common/enum';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmPeriodService } from 'src/app/model/dictionary/dm-period/dm-period-service';
import { PeriodType } from 'src/app/model/dictionary/dm-period/period-type';
import { DmPeriodDetailComponent } from '../dm-period-detail/dm-period-detail.component';

@Component({
  selector: 'dm-period-list',
  templateUrl: './dm-period-list.component.html',
  styleUrls: ['./dm-period-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmPeriodListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_ITEM_OP';
  state$: { PeriodTypeDatas: PeriodType[] } = { PeriodTypeDatas: [] };

  constructor(myService: DmPeriodService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmPeriodDetailComponent;
  columns = [
    { FieldId: 'PERIOD_ID', Width: 120 },
    { FieldId: 'PERIOD_NAME' },
    { FieldId: 'PERIOD_NUMBER', Width: 100, ColumnType: 'number' },
    {
      FieldId: 'PERIOD_TYPE',
      Width: 200,
      ColumnType: 'combo',
      Data: this.state$.PeriodTypeDatas,
      ValueField: 'PERIOD_TYPE_ID',
      TextField: 'PERIOD_TYPE_NAME',
      ShowText: true,
    },

    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      ColumnType: 'boolean',
      ClassNames: 'text-center',
      Width: 80,
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'PERIOD_ID';
  nameField = 'PERIOD_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  public loadDm() {
    this.myInject.dictBaseListStore.setLoadingState(EnumLoadingState.Loading);
    Promise.all([(<DmPeriodService>this.myService).GetPeriodTypeList()])
      .then(([PeriodTypeDatas]) => {
        Object.assign(this.state$.PeriodTypeDatas, PeriodTypeDatas);
        this.myInject.dictBaseListStore.setLoadingState(
          EnumLoadingState.Complete
        );
      })
      .catch((err) => {
        this.myInject.dictBaseListStore.loadDataComplete(err);
        this.myInject.ftsDialogService.alert.show({
          content: this.myInject.FTSMain.ExceptionManager.processException(err),
          icon: 'warning',
        });
      });
  }
}

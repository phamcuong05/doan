import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmContractStatusService } from 'src/app/model/dictionary/dm-contract-status/dm-contract-status-service';
import { DmContractStatusDetailComponent } from '../dm-contract-status-detail/dm-contract-status-detail.component';

@Component({
  selector: 'dm-contract-status-list',
  templateUrl: './dm-contract-status-list.component.html',
  styleUrls: ['./dm-contract-status-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmContractStatusListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_CONTRACT_STATUS';

  constructor(
    myService: DmContractStatusService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }

  detailComponent = DmContractStatusDetailComponent;
  columns = [
    {
      FieldId: 'CONTRACT_STATUS_ID',
      Length: 20,
    },
    {
      FieldId: 'CONTRACT_STATUS_NAME',
    },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'CONTRACT_STATUS_ID';
  nameField = 'CONTRACT_STATUS_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

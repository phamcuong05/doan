import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmContractClassService } from 'src/app/model/dictionary/dm-contract-class/dm-contract-class-service';
import { DmContractClassDetailComponent } from '../dm-contract-class-detail/dm-contract-class-detail.component';

@Component({
  selector: 'dm-contract-class-list',
  templateUrl: './dm-contract-class-list.component.html',
  styleUrls: ['./dm-contract-class-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmContractClassListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_CONTRACT_CLASS';

  constructor(
    myService: DmContractClassService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }

  detailComponent = DmContractClassDetailComponent;
  columns = [
    {
      FieldId: 'CONTRACT_CLASS_ID',
      Text: 'Mã loại hợp đồng',
      Length: 20,
    },
    {
      FieldId: 'CONTRACT_CLASS_NAME',
      Text: 'Tên loại hợp đồng',
    },
    { FieldId: 'USER_ID', Text: 'Người lập', Length: 15 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      Text: 'Sử dụng',
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'CONTRACT_CLASS_ID';
  nameField = 'CONTRACT_CLASS_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

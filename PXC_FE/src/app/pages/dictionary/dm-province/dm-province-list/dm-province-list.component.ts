import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmProvinceService } from 'src/app/model/dictionary/dm-province/dm-province-service';
import { DmProvinceDetailComponent } from '../dm-province-detail/dm-province-detail.component';

@Component({
  selector: 'dm-province-list',
  templateUrl: './dm-province-list.component.html',
  styleUrls: ['./dm-province-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmProvinceListComponent extends FtsDictBaseList {
  constructor(myService: DmProvinceService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmProvinceDetailComponent;
  columns = [
    {
      FieldId: 'PROVINCE_ID',
      Length: 20,
    },
    {
      FieldId: 'PROVINCE_NAME',
    },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'PROVINCE_ID';
  nameField = 'PROVINCE_NAME';
  tableName = 'DM_PROVINCE';
  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmDistrictService } from 'src/app/model/dictionary/dm-district/dm-district-service';
import { DmDistrictDetailComponent } from '../dm-district-detail/dm-district-detail.component';

@Component({
  selector: 'dm-district-list',
  templateUrl: './dm-district-list.component.html',
  styleUrls: ['./dm-district-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmDistrictListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_DISTRICT';

  constructor(myService: DmDistrictService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmDistrictDetailComponent;
  columns = [
    {
      FieldId: 'DISTRICT_ID',
      Length: 15,
    },
    {
      FieldId: 'DISTRICT_NAME',
      Width: 200,
    },
    {
      FieldId: 'PROVINCE_ID',
      Length: 15,
    },
    {
      FieldId: 'PROVINCE_NAME',
      Width: 200,
    },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      ColumnType: 'boolean',
      ClassNames: 'text-center',
      Width: 80,
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'DISTRICT_ID';
  nameField = 'DISTRICT_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

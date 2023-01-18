import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmUnitService } from 'src/app/model/dictionary/dm-unit/dm-unit-service';
import { DmUnitDetailComponent } from '../dm-unit-detail/dm-unit-detail.component';

@Component({
  selector: 'dm-unit-list',
  templateUrl: './dm-unit-list.component.html',
  styleUrls: ['./dm-unit-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmUnitListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_UNIT';

  constructor(myService: DmUnitService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmUnitDetailComponent;
  columns = [
    {
      FieldId: 'UNIT_ID',
      Length: 20,
    },
    {
      FieldId: 'UNIT_NAME',
    },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'UNIT_ID';
  nameField = 'UNIT_NAME';
  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmEstimateTypeService } from 'src/app/model/dictionary/dm-estimate-type/dm-estimate-type-service';
import { DmEstimateTypeDetailComponent } from '../dm-estimate-type-detail/dm-estimate-type-detail.component';

@Component({
  selector: 'dm-estimate-type-list',
  templateUrl: './dm-estimate-type-list.component.html',
  styleUrls: ['./dm-estimate-type-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmEstimateTypeListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_ESTIMATE_TYPE';

  constructor(
    myService: DmEstimateTypeService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }

  detailComponent = DmEstimateTypeDetailComponent;
  columns = [
    { FieldId: 'ESTIMATE_TYPE_ID', Length: 20 },
    { FieldId: 'ESTIMATE_TYPE_NAME' },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'ESTIMATE_TYPE_ID';
  nameField = 'ESTIMATE_TYPE_NAME';
  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

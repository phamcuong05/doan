import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmRiskClassService } from 'src/app/model/dictionary/dm-risk-class/dm-risk-class-service';
import { DmRiskClassDetailComponent } from '../dm-risk-class-detail/dm-risk-class-detail.component';

@Component({
  selector: 'dm-risk-class-list',
  templateUrl: './dm-risk-class-list.component.html',
  styleUrls: ['./dm-risk-class-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmRiskClassListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_RISK_CLASS';

  constructor(myService: DmRiskClassService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmRiskClassDetailComponent;
  columns = [
    {
      FieldId: 'RISK_CLASS_ID',
      Text: 'Mã nhóm rủi ro',
      Length: 20,
    },
    {
      FieldId: 'RISK_CLASS_NAME',
      Text: 'Tên nhóm rủi ro',
    },
    {
      FieldId: 'RISK_CLASS_CATEGORY',
      Text: 'Mức rủi ro',
    },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'RISK_CLASS_ID';
  nameField = 'RISK_CLASS_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }
  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

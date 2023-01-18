import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmPrDetailClassService } from 'src/app/model/dictionary/dm-pr-detail-class/dm-pr-detail-class-service';
import { DmPrDetailClassDetailComponent } from '../dm-pr-detail-class-detail/dm-pr-detail-class-detail.component';

@Component({
  selector: 'dm-pr-detail-class-list',
  templateUrl: './dm-pr-detail-class-list.component.html',
  styleUrls: ['./dm-pr-detail-class-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmPrDetailClassListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_PR_DETAIL_CLASS';

  constructor(
    myService: DmPrDetailClassService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }

  detailComponent = DmPrDetailClassDetailComponent;
  columns = [
    { FieldId: 'PR_DETAIL_CLASS_ID', Length: 20 },
    { FieldId: 'PR_DETAIL_CLASS_NAME' },
    { FieldId: 'PR_DETAIL_TYPE_NAME' },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      ColumnType: 'boolean',
      ClassNames: 'text-center',
      Width: 80
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'PR_DETAIL_CLASS_ID';
  nameField = 'PR_DETAIL_CLASS_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

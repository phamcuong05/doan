import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmItemClassService } from 'src/app/model/dictionary/dm-item-class/dm-item-class-service';
import { DmItemClassDetailComponent } from '../dm-item-class-detail/dm-item-class-detail.component';

@Component({
  selector: 'dm-item-class-list',
  templateUrl: './dm-item-class-list.component.html',
  styleUrls: ['./dm-item-class-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmItemClassListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_ITEM_CLASS';

  constructor(myService: DmItemClassService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmItemClassDetailComponent;
  columns = [
    { FieldId: 'ITEM_CLASS_ID', Length: 20 },
    { FieldId: 'ITEM_CLASS_NAME' },
    { FieldId: 'INV_ACCOUNT_ID', Length: 15 },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'ITEM_CLASS_ID';
  nameField = 'ITEM_CLASS_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

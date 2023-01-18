import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmItemService } from 'src/app/model/dictionary/dm-item/dm-item-service';
import { DmItemDetailComponent } from '../dm-item-detail/dm-item-detail.component';

@Component({
  selector: 'dm-item-list',
  templateUrl: './dm-item-list.component.html',
  styleUrls: ['./dm-item-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmItemListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_ITEM';

  constructor(myService: DmItemService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmItemDetailComponent;
  columns = [
    { FieldId: 'ITEM_ID', Length: 17 },
    { FieldId: 'ITEM_NAME', Width: 400 },
    { FieldId: 'UNIT_NAME', Length: 10 },
    { FieldId: 'ITEM_CLASS_NAME', Width: 200 },
    { FieldId: 'ITEM_CLASS1_NAME', Width: 200 },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      ColumnType: 'boolean',
      ClassNames: 'text-center',
      Width: 80,
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'ITEM_ID';
  nameField = 'ITEM_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

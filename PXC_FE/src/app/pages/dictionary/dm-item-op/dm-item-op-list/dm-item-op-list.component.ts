import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmItemOpService } from 'src/app/model/dictionary/dm-item-op/dm-item-op-service';
import { DmItemOpDetailComponent } from '../dm-item-op-detail/dm-item-op-detail.component';

@Component({
  selector: 'dm-item-op-list',
  templateUrl: './dm-item-op-list.component.html',
  styleUrls: ['./dm-item-op-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmItemOpListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_ITEM_OP';

  constructor(myService: DmItemOpService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmItemOpDetailComponent;
  columns = [
    { FieldId: 'ITEM_OP_ID', Length: 10 },
    { FieldId: 'ITEM_OP_NAME', Width: 200 },
    { FieldId: 'ISSUE_RECEIPT', Length: 10 },
    { FieldId: 'TRANSFER_ITEM_OP_ID', Length: 10 },
    { FieldId: 'TRANSFER_ITEM_OP_NAME', Width: 150 },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      ColumnType: 'boolean',
      ClassNames: 'text-center',
      Width: 80,
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'ITEM_OP_ID';
  nameField = 'ITEM_OP_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { SysMenuService } from 'src/app/model/system/sys-menu/sys-menu-service';
import { SysMenuDetailComponent } from '../sys-menu-detail/sys-menu-detail.component';

@Component({
  selector: 'sys-menu-list',
  templateUrl: './sys-menu-list.component.html',
  styleUrls: ['./sys-menu-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class SysMenuListComponent extends FtsDictBaseList {
  override tableName: string = 'SYS_MENU';

  constructor(myService: SysMenuService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = SysMenuDetailComponent;
  columns = [
    { FieldId: 'MENU_ID', Width: 225 },
    { FieldId: 'MENU_NAME', Width: 190 },
    { FieldId: 'MODULE_ID', Width: 135 },
    { FieldId: 'MENU_GROUP_ID', Width: 135 },
    { FieldId: 'ICON_CLS', Width: 200 },
    { FieldId: 'HREF', Width: 145 },
    { FieldId: 'MAP_PATH', Width: 155 },
    { FieldId: 'MENU_ORDER', Width: 115, ColumnType: 'numeric' },
    { FieldId: 'EXPAND_TYPE', Width: 110 },
    { FieldId: 'ACTIVE', Width: 80, ColumnType: 'boolean' } as FtsColumn,
  ] as Array<FtsColumn>;

  idField = 'MENU_ID';
  nameField = 'MENU_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

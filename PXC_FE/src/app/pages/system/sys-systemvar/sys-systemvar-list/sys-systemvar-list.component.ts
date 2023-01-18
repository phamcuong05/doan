import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { SysSystemVarService } from 'src/app/model/system/system-var/sys-systemvar-service';
import { SysSystemVarDetailComponent } from '../sys-systemvar-detail/sys-systemvar-detail.component';

@Component({
  selector: 'sys-systemvar-list',
  templateUrl: './sys-systemvar-list.component.html',
  styleUrls: ['./sys-systemvar-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class SysSystemVarListComponent extends FtsDictBaseList {
  override tableName: string = 'SYS_MENU';

  constructor(myService: SysSystemVarService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = SysSystemVarDetailComponent;
  columns = [
    { FieldId: 'VAR_NAME', Width: 300 },
    { FieldId: 'VAR_VALUE', Width: 150 },
    { FieldId: 'DESCRIPTION', Width: 300 },
    { FieldId: 'VAR_TYPE', Width: 126 },
    { FieldId: 'VAR_GROUP', Width: 126 },
  ] as Array<FtsColumn>;

  idField = 'VAR_NAME';
  nameField = 'VAR_VALUE';

  ngOnInit(): void {
    this.dictBaseListcomponent.showToolbarAdd = false;
    this.dictBaseListcomponent.showToolbarDelete = false;
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

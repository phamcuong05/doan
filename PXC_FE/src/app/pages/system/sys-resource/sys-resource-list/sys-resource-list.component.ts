import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { SysResoureService } from 'src/app/model/system/sys-resource/sys-resource-service';
import { SysResourceDetailComponent } from '../sys-resource-detail/sys-resource-detail.component';

@Component({
  selector: 'sys-resource-list',
  templateUrl: './sys-resource-list.component.html',
  styleUrls: ['./sys-resource-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class SysResourceListComponent extends FtsDictBaseList {
  override tableName: string = 'SYS_RESOURCE';

  constructor(myService: SysResoureService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = SysResourceDetailComponent;
  columns = [
    { FieldId: 'RES_ID', Width: 200 },
    { FieldId: 'RES_VALUE', Width: 400 },
  ] as Array<FtsColumn>;
  idField = 'RES_ID';
  nameField = 'RES_VALUE';

  ngOnInit(): void {
    this.dictBaseListcomponent.showToolbarAdd = false;
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmDepartmentService } from 'src/app/model/dictionary/dm-department/dm-department-service';

@Component({
  selector: 'dm-department-list',
  templateUrl: './dm-department-list.component.html',
  styleUrls: ['./dm-department-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmDepartmentListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_DEPARTMENT';

  columns: FtsColumn[] = [
    { FieldId: 'DEPARTMENT_ID', Length: 20 } as FtsColumn,
    { FieldId: 'DEPARTMENT_NAME' },
    { FieldId: 'USER_ID', Length: 10 },
    { FieldId: 'ACTIVE', ColumnType: 'boolean', Width: 80 },
  ] as FtsColumn[];
  idField: string = 'DEPARTMENT_ID';
  nameField: string = 'DEPARTMENT_NAME';
  constructor(myService: DmDepartmentService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

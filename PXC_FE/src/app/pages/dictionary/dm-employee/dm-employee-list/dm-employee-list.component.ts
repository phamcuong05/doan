import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmEmployeeService } from 'src/app/model/dictionary/dm-employee/dm-employee-service';

@Component({
  selector: 'dm-employee-list',
  templateUrl: './dm-employee-list.component.html',
  styleUrls: ['./dm-employee-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmEmployeeListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_EMPLOYEE';

  columns: FtsColumn[] = [
    { FieldId: 'EMPLOYEE_ID', Length: 11 } as FtsColumn,
    { FieldId: 'EMPLOYEE_NAME', Length: 30 },
    { FieldId: 'DEPARTMENT_NAME', Length: 20 },
    { FieldId: 'ADDRESS', Length: 40 },
    { FieldId: 'PHONE', Length: 10 },
    { FieldId: 'EMAIL', Length: 25 },
    { FieldId: 'IDENTITY_NO', Length: 13 },
    { FieldId: 'USER_ID', Length: 10 },
    { FieldId: 'ACTIVE', ColumnType: 'boolean', Width: 80 },
  ] as FtsColumn[];
  idField: string = 'EMPLOYEE_ID';
  nameField: string = 'EMPLOYEE_NAME';
  constructor(myService: DmEmployeeService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }
  ngOnInit(): void {}

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

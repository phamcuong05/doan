import { Component, Input } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { SecUserService } from 'src/app/model/system/sec-user/sec-user-service';
import { SecUserDetailComponent } from '../sec-user-detail/sec-user-detail.component';

@Component({
  selector: 'sec-user-list',
  templateUrl: './sec-user-list.component.html',
  styleUrls: ['./sec-user-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class SecUserListComponent extends FtsDictBaseList {
  override tableName: string = 'SEC_USER';
  constructor(myService: SecUserService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = SecUserDetailComponent;

  @Input() columns = [
    { FieldId: 'USER_ID', Width: 120 },
    { FieldId: 'USER_NAME', Width: 120 },
    // { FieldId: 'USER_GROUP_ID' },
    { FieldId: 'USER_PASSWORD', Width: 140 }, 
    // { FieldId: 'ORGANIZATION_ID' },
    // { FieldId: 'EMPLOYEE_ID' },
    /*{ FieldId: 'USER_KEY' },
      { FieldId: 'QUANTITY_INVALID', ColumnType: 'numeric' },
      {
         FieldId: 'LOGIN_DATE',
         ColumnType: 'date',
         Format: 'dd/MM/yyyy',
      }, 
    */
    {
      FieldId: 'ACTIVE', Width: 120,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'USER_ID';
  nameField = 'USER_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

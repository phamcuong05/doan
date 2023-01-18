import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmSecurityTypeService } from 'src/app/model/dictionary/dm-security-type/dm-security-type-service';

@Component({
  selector: 'dm-security-type-list',
  templateUrl: './dm-security-type-list.component.html',
  styleUrls: ['./dm-security-type-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmSecurityTypeListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_SECURITY_TYPE';

  constructor(
    myService: DmSecurityTypeService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }
  columns = [
    { FieldId: 'SECURITY_TYPE_ID', Width: 205 },
    { FieldId: 'SECURITY_TYPE_NAME' },
    { FieldId: 'USER_ID', Width: 163 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'SECURITY_TYPE_ID';
  nameField = 'SECURITY_TYPE_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

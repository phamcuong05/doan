import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmSecurityClassService } from 'src/app/model/dictionary/dm-security-class/dm-security-class-service';

@Component({
  selector: 'dm-security-class-list',
  templateUrl: './dm-security-class-list.component.html',
  styleUrls: ['./dm-security-class-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmSecurityClassListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_SECURITY_CLASS';

  constructor(
    myService: DmSecurityClassService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }
  columns = [
    { FieldId: 'SECURITY_CLASS_ID', Width: 205 },
    { FieldId: 'SECURITY_CLASS_NAME' },
    { FieldId: 'USER_ID', Width: 163 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'SECURITY_CLASS_ID';
  nameField = 'SECURITY_CLASS_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

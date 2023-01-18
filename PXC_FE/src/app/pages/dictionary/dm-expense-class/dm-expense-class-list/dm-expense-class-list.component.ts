import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmExpenseClassService } from 'src/app/model/dictionary/dm-expense-class/dm-expense-class-service';

@Component({
  selector: 'dm-expense-class-list',
  templateUrl: './dm-expense-class-list.component.html',
  styleUrls: ['./dm-expense-class-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmExpenseClassListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_EXPENSE_CLASS';
  columns: FtsColumn[] = [
    { FieldId: 'EXPENSE_CLASS_ID', Length: 20 } as FtsColumn,
    { FieldId: 'EXPENSE_CLASS_NAME' },
    { FieldId: 'USER_ID', Length: 15 },
    { FieldId: 'ACTIVE', ColumnType: 'boolean', Width: 80 },
  ] as FtsColumn[];
  idField: string = 'EXPENSE_CLASS_ID';
  nameField: string = 'EXPENSE_CLASS_NAME';
  constructor(
    myService: DmExpenseClassService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmExpenseService } from 'src/app/model/dictionary/dm-expense/dm-expense-service';

@Component({
  selector: 'dm-expense-list',
  templateUrl: './dm-expense-list.component.html',
  styleUrls: ['./dm-expense-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmExpenseListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_EXPENSE';
  columns: FtsColumn[] = [
    { FieldId: 'EXPENSE_ID', Length: 15 } as FtsColumn,
    { FieldId: 'EXPENSE_NAME', Length: 35 },
    { FieldId: 'EXPENSE_CLASS_ID', Length: 15 },
    { FieldId: 'EXPENSE_CLASS_NAME', Length: 35 },
    { FieldId: 'USER_ID', Length: 15 },
    { FieldId: 'ACTIVE', ColumnType: 'boolean', Width: 100 },
  ] as FtsColumn[];
  idField: string = 'EXPENSE_ID';
  nameField: string = 'EXPENSE_NAME';
  constructor(myService: DmExpenseService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  ngOnInit(): void {}

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

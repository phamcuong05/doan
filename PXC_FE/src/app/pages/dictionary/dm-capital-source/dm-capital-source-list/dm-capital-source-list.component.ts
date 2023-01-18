import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmCapitalSourceService } from 'src/app/model/dictionary/dm-capital-source/dm-capital-source-service';

@Component({
  selector: 'dm-capital-source-list',
  templateUrl: './dm-capital-source-list.component.html',
  styleUrls: ['./dm-capital-source-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmCapitalSourceListComponent extends FtsDictBaseList {
  columns: FtsColumn[] = [
    {
      FieldId: 'CAPITAL_SOURCE_ID',
      Length: 20,
    } as FtsColumn,
    {
      FieldId: 'CAPITAL_SOURCE_NAME',
    },
    {
      FieldId: 'USER_ID',
      Length: 15,
    },
    {
      FieldId: 'ACTIVE',
      ColumnType: 'boolean',
      Width: 80,
    },
  ] as FtsColumn[];
  idField: string = 'CAPITAL_SOURCE_ID';
  nameField: string = 'CAPITAL_SOURCE_NAME';
  tableName: string = 'CAPITAL_SOURCE';

  constructor(
    myService: DmCapitalSourceService,
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

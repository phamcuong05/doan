import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmAgentService } from 'src/app/model/dictionary/dm-agent/dm-agent-service';

@Component({
  selector: 'dm-agent-list',
  templateUrl: './dm-agent-list.component.html',
  styleUrls: ['./dm-agent-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmAgentListComponent extends FtsDictBaseList {
  columns: FtsColumn[] = [
    {
      FieldId: 'AGENT_ID',
      Length: 11,
    } as FtsColumn,
    {
      FieldId: 'AGENT_NAME',
      Length: 40,
    },
    {
      FieldId: 'IDENTITY_NO',
      Length: 13,
    },
    {
      FieldId: 'TAX_FILE_NUMBER',
      Length: 14,
    },
    {
      FieldId: 'PHONE',
      Length: 10,
    },
    {
      FieldId: 'EMAIL',
      Length: 20,
    },
    {
      FieldId: 'ADDRESS',
      Length: 40,
    },
    {
      FieldId: 'BANK_CODE',
      Length: 10,
    },
    {
      FieldId: 'BANK_NAME',
      Length: 20,
    },
    {
      FieldId: 'BANK_ACCOUNT',
      Length: 14,
    },
    {
      FieldId: 'BANK_BRANCH',
      Length: 20,
    },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
    },
  ] as FtsColumn[];
  idField: string = 'AGENT_ID';
  nameField: string = 'AGENT_NAME';
  tableName: string = 'DM_AGENT';

  constructor(myService: DmAgentService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

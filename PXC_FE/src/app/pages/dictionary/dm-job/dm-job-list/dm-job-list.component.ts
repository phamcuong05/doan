import { Component, OnInit } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmJobService } from 'src/app/model/dictionary/dm-job/dm-job.service';

@Component({
  selector: 'dm-job-list',
  templateUrl: './dm-job-list.component.html',
  styleUrls: ['./dm-job-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmJobListComponent extends FtsDictBaseList {

  constructor(myService: DmJobService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
   }

   columns = [
    { FieldId: 'JOB_ID', Width: 60 },
    { FieldId: 'JOB_NAME', Width: 220 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,   
      
      { FieldId: 'USER_ID', Width: 100, ClassNames: 'text-center' },
  ] as Array<FtsColumn>;
  idField : string = 'JOB_ID';
  nameField : string = 'JOB_NAME';
  tableName = 'DM_JOB';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }

}

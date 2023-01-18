import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmOrganizarionService } from 'src/app/model/system/dm-organizarion/dm-organizarion-service';
import { DmOrganizarionDetailComponent } from '../dm-organizarion-detail/dm-organizarion-detail.component';

@Component({
  selector: 'dm-organizarion-list',
  templateUrl: './dm-organizarion-list.component.html',
  styleUrls: ['./dm-organizarion-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmOrganizarionListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_ORGANIZATION';

  constructor(
    myService: DmOrganizarionService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }

  detailComponent = DmOrganizarionDetailComponent;

  columns = [
    { FieldId: 'ORGANIZATION_ID', Length: 11 },
    { FieldId: 'ORGANIZATION_NAME', Width: 250 },
    { FieldId: 'ORGANIZATION_NAME_SHORT', Width: 200 } as FtsColumn,
  ] as Array<FtsColumn>;

  idField = 'ORGANIZATION_ID';

  nameField = 'ORGANIZATION_NAME';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

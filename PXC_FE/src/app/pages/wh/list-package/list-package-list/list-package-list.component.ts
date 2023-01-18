import { Component, OnInit } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { ListPackageService } from 'src/app/model/wh/list-package/list-package.service';

@Component({
  selector: 'list-package-list',
  templateUrl: './list-package-list.component.html',
  styleUrls: ['./list-package-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class ListPackageListComponent extends FtsDictBaseList {

  constructor(
    myService: ListPackageService,
    myInject: FtsDictBaseListInject
  ) {
    super(myService, myInject);
  }
  
  columns = [
    { FieldId: 'PACKAGE_CODE', Width: 100 },
    { FieldId: 'PACKAGE_NAME', Width: 140 },
    { FieldId: 'TRACKING_CODE', Width: 120, ClassNames: 'idField' },
    { FieldId: 'ITEM', Width: 100 }, 
    { FieldId: 'SERVICE_CHARGE_CODE', Width: 100, ClassNames: 'idField' }, 
    {
      FieldId: 'WEIGHT',
      ColumnType: 'numeric',
      Format: '',
      Width: 100
    },
    { FieldId: 'CONTAINER_ID', Width: 110, ClassNames: 'idField' }, 
    { FieldId: 'WARE_HOUSE_ID', Width: 100, ClassNames: 'idField' }, 
    { FieldId: 'MAWB_ID', Width: 110, ClassNames: 'idField' },
    {
      FieldId: 'CREATE_DATE',
      ColumnType: 'date',
      Format: this.myInject.FTSMain.dateFormat,
      Width: 120
    },
    {
      FieldId: 'MODIFIED_DATE',
      ColumnType: 'date',
      Format: this.myInject.FTSMain.dateFormat,
      Width: 120
    } as FtsColumn,
    { FieldId: 'USER_ID', Width: 100 },
  ] as Array<FtsColumn>;
  idField: string = 'PACKAGE_CODE';
  nameField: string = 'PACKAGE_NAME';
  tableName: string = 'LIST_PACKAGE';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }

}

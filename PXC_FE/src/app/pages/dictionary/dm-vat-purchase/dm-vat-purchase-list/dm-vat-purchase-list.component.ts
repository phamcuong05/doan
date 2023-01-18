import { Component, OnInit } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmVatPurchaseService } from 'src/app/model/dictionary/dm-vat-purchase/dm-vat-purchase.service';

@Component({
  selector: 'dm-vat-purchase-list',
  templateUrl: './dm-vat-purchase-list.component.html',
  styleUrls: ['./dm-vat-purchase-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmVatPurchaseListComponent extends FtsDictBaseList {

  constructor(myService: DmVatPurchaseService, myInject: FtsDictBaseListInject) { 
    super(myService, myInject);
  }

  columns = [
    { FieldId: 'VAT_PURCHASE_ID', Width: 120 } as FtsColumn,
    { FieldId: 'VAT_PURCHASE_NAME', Width: 560 },   
    {
      FieldId: 'ACTIVE',
      Width: 143,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    },
    { FieldId: 'USER_ID', Width: 135 },
  ] as Array<FtsColumn>;
  idField = 'VAT_PURCHASE_ID';
  nameField = 'VAT_PURCHASE_NAME';
  tableName = 'DM_VAT_PURCHASE';

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }

}

import { Component } from '@angular/core';
import { FtsDictBaseList } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list';
import { FtsDictBaseListInject } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { DmVatTaxService } from 'src/app/model/dictionary/dm-vat-tax/dm-vat-tax-service';
import { DmVatTaxDetailComponent } from '../dm-vat-tax-detail/dm-vat-tax-detail.component';

@Component({
  selector: 'dm-vat-tax-list',
  templateUrl: './dm-vat-tax-list.component.html',
  styleUrls: ['./dm-vat-tax-list.component.scss'],
  providers: [DictBaseListStore, FtsDictBaseListInject],
})
export class DmVatTaxListComponent extends FtsDictBaseList {
  override tableName: string = 'DM_VAT_TAX';
  constructor(myService: DmVatTaxService, myInject: FtsDictBaseListInject) {
    super(myService, myInject);
  }

  detailComponent = DmVatTaxDetailComponent;
  columns = [
    { FieldId: 'VAT_TAX_ID', Length: 20 },
    { FieldId: 'VAT_TAX_NAME' },
    {
      FieldId: 'VAT_TAX_RATE',
      ColumnType: 'numeric',
      Format: 'n2',
      Length: 10,
    },
    { FieldId: 'USER_ID', Length: 15 },
    {
      FieldId: 'ACTIVE',
      Width: 80,
      ColumnType: 'boolean',
      ClassNames: 'text-center',
    } as FtsColumn,
  ] as Array<FtsColumn>;
  idField = 'VAT_TAX_ID';
  nameField = 'VAT_TAX_NAME';
  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}

import { Component, ElementRef, forwardRef, ViewContainerRef } from '@angular/core';
import { FTSMain } from 'src/app/base/ftsmain';
import { EventManager } from 'src/app/common/eventManager';
import { MyReference } from 'src/app/common/MyReference';
import { ResourceManager } from 'src/app/common/resource-manager';
import { FtsDialogService } from 'src/app/controls/fts-dialog/fts-dialog.service';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { FtsTextLookupSelectorBase } from 'src/app/controls/fts-text-lookup-selector/fts-text-lookup-selector-base';
import { MaskLoadService } from 'src/app/controls/mask-load/mask-load.service';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { DmVatPurchaseService } from 'src/app/model/dictionary/dm-vat-purchase/dm-vat-purchase.service';

@Component({
  selector: 'dm-vat-purchase-selector',
  templateUrl: './dm-vat-purchase-selector.component.html',
  styleUrls: ['./dm-vat-purchase-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmVatPurchaseSelectorComponent),
    },
  ],
})
export class DmVatPurchaseSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_VAT_PURCHASE';
  searchFields: string[] = ['VAT_PURCHASE_ID', 'VAT_PURCHASE_NAME'];

  get formTitle(): string {
    return this.resourceManager.DmVatPurchaseResource.MyResource.DM_VAT_PURCHASE;
  }
  columns: FtsColumn[] = [
    { FieldId: 'VAT_PURCHASE_ID', Length: 20 },
    { FieldId: 'VAT_PURCHASE_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    myService: DmVatPurchaseService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      myService,
      maskLoad,
      viewContainerRef,
      el,
      ftsDialogService,
      eventManager,
      ftsMain
    );
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
  ngAfterViewInit(): void {
    super.ngAfterViewInit();
  }
  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  override setParamBeforLoad(param: PagingParam): PagingParam {
    if (param) {
      if (!param.FilterGroups) {
        param.FilterGroups = [];
      }
      param.FilterGroups.push({
        Filters: [
          {
            Field: 'ACTIVE',
            Operator: 'eq',
            Value: 1,
          },
        ],
        Logic: 'and',
      });

      param.FilterFields = ['VAT_PURCHASE_ID', 'VAT_PURCHASE_NAME'];
    }
    return param;
  }
}


import {
  Component,
  ElementRef,
  forwardRef,
  ViewContainerRef,
} from '@angular/core';
import { FTSMain } from 'src/app/base/ftsmain';
import { EventManager } from 'src/app/common/eventManager';
import { MyReference } from 'src/app/common/MyReference';
import { ResourceManager } from 'src/app/common/resource-manager';
import { FtsDialogService } from 'src/app/controls/fts-dialog/fts-dialog.service';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { FtsTextLookupSelectorBase } from 'src/app/controls/fts-text-lookup-selector/fts-text-lookup-selector-base';
import { MaskLoadService } from 'src/app/controls/mask-load/mask-load.service';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { DmVatTaxService } from 'src/app/model/dictionary/dm-vat-tax/dm-vat-tax-service';

@Component({
  selector: 'dm-vat-tax-selector',
  templateUrl: './dm-vat-tax-selector.component.html',
  styleUrls: ['./dm-vat-tax-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmVatTaxSelectorComponent),
    },
  ],
})
export class DmVatTaxSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_VAT_TAX';
  searchFields: string[] = ['VAT_TAX_ID', 'VAT_TAX_NAME'];

  get formTitle(): string {
    return this.resourceManager.DmVatTaxResource.MyResource.DM_VAT_TAX;
  }
  columns: FtsColumn[] = [
    { FieldId: 'VAT_TAX_ID', Length: 20 },
    { FieldId: 'VAT_TAX_NAME' },
    { FieldId: 'VAT_TAX_RATE', Length: 5 },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmVatTaxService: DmVatTaxService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmVatTaxService,
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
  override ngOnDestroy(): void {
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

      param.FilterFields = ['VAT_TAX_ID', 'VAT_TAX_NAME', 'VAT_TAX_RATE'];
    }
    return param;
  }
}

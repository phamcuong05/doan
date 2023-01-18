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
import { DmBankService } from 'src/app/model/dictionary/dm-bank/dm-bank-service';

@Component({
  selector: 'dm-bank-selector',
  templateUrl: './dm-bank-selector.component.html',
  styleUrls: ['./dm-bank-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmBankSelectorComponent),
    },
  ],
})
export class DmBankSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_BANK';
  searchFields: string[] = ['BANK_ID', 'BANK_NAME'];
  get formTitle(): string {
    return this.resourceManager.DmBankResource.MyResource.DM_BANK;
  }
  columns: FtsColumn[] = [
    { FieldId: 'BANK_ID', Length: 20 },
    { FieldId: 'BANK_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmBankService: DmBankService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmBankService,
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

      param.FilterFields = ['BANK_ID', 'BANK_NAME'];
    }
    return param;
  }
}

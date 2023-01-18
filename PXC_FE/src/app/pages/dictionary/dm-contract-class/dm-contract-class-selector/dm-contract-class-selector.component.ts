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
import { DmContractClassService } from 'src/app/model/dictionary/dm-contract-class/dm-contract-class-service';

@Component({
  selector: 'dm-contract-class-selector',
  templateUrl: './dm-contract-class-selector.component.html',
  styleUrls: ['./dm-contract-class-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmContractClassSelectorComponent),
    },
  ],
})
export class DmContractClassSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_CONTRACT_CLASS';
  searchFields: string[] = ['CONTRACT_CLASS_ID', 'CONTRACT_CLASS_NAME'];
  get formTitle(): string {
    return this.resourceManager.DmContractClassResource.MyResource
      .DM_CONTRACT_CLASS;
  }
  columns: FtsColumn[] = [
    { FieldId: 'CONTRACT_CLASS_ID', Length: 20 },
    { FieldId: 'CONTRACT_CLASS_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmContractClassService: DmContractClassService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmContractClassService,
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

      param.FilterFields = ['CURRENCY_ID', 'CURRENCY_NAME'];
    }
    return param;
  }
}

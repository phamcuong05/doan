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
import { DmContractStatusService } from 'src/app/model/dictionary/dm-contract-status/dm-contract-status-service';

@Component({
  selector: 'dm-contract-status-selector',
  templateUrl: './dm-contract-status-selector.component.html',
  styleUrls: ['./dm-contract-status-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmContractStatusSelectorComponent),
    },
  ],
})
export class DmContractStatusSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_CONTRACT_STATUS';
  searchFields: string[] = ['CONTRACT_STATUS_ID', 'CONTRACT_STATUS_NAME'];
  get formTitle(): string {
    return this.resourceManager.DmContractStatusResource.MyResource
      .DM_CONTRACT_STATUS;
  }
  columns: FtsColumn[] = [
    { FieldId: 'CONTRACT_STATUS_ID', Length: 20 },
    { FieldId: 'CONTRACT_STATUS_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmContractStatusService: DmContractStatusService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmContractStatusService,
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

      param.FilterFields = ['CONTRACT_STATUS_ID', 'CONTRACT_STATUS_NAME'];
    }
    return param;
  }
}

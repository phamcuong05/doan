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
import { DmAgentService } from 'src/app/model/dictionary/dm-agent/dm-agent-service';

@Component({
  selector: 'dm-agent-selector',
  templateUrl: './dm-agent-selector.component.html',
  styleUrls: ['./dm-agent-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmAgentSelectorComponent),
    },
  ],
})
export class DmAgentSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_AGENT';
  searchFields: string[] = ['AGENT_ID', 'AGENT_NAME'];
  columns: FtsColumn[] = [
    { FieldId: 'AGENT_ID', Length: 15 },
    { FieldId: 'AGENT_NAME' },
  ] as FtsColumn[];
  get formTitle(): string {
    return this.resourceManager.DmAgentResource.MyResource.AGENT;
  }

  constructor(
    resourceManage: ResourceManager,
    dmAgentService: DmAgentService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmAgentService,
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

      param.FilterFields = ['AGENT_ID', 'AGENT_NAME'];
    }
    return param;
  }
}

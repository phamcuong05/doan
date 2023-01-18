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
import { DmItemOpService } from 'src/app/model/dictionary/dm-item-op/dm-item-op-service';

@Component({
  selector: 'dm-item-op-selector',
  templateUrl: './dm-item-op-selector.component.html',
  styleUrls: ['./dm-item-op-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmItemOpSelectorComponent),
    },
  ],
})
export class DmItemOpSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_ITEM_OP';
  searchFields: string[] = ['ITEM_OP_ID', 'ITEM_OP_NAME'];
  get formTitle(): string {
    return this.resourceManager.DmItemOpResource.MyResource.DM_ITEM_OP;
  }
  columns: FtsColumn[] = [
    { FieldId: 'ITEM_OP_ID', Length: 20 },
    { FieldId: 'ITEM_OP_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmItemOpService: DmItemOpService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmItemOpService,
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

      param.FilterFields = ['ITEM_OP_ID', 'ITEM_OP_NAME'];
    }
    return param;
  }
}

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
import { DmItemClassService } from 'src/app/model/dictionary/dm-item-class/dm-item-class-service';

@Component({
  selector: 'dm-item-class-selector',
  templateUrl: './dm-item-class-selector.component.html',
  styleUrls: ['./dm-item-class-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmItemClassSelectorComponent),
    },
  ],
})
export class DmItemClassSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_ITEM_CLASS';
  searchFields: string[] = ['ITEM_CLASS_ID', 'ITEM_CLASS_NAME'];
  get formTitle(): string {
    return 'Danh mục nhóm hàng hóa';
  }
  columns: FtsColumn[] = [
    { FieldId: 'ITEM_CLASS_ID', Length: 20 },
    { FieldId: 'ITEM_CLASS_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmItemClassService: DmItemClassService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmItemClassService,
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

      param.FilterFields = ['ITEM_CLASS_ID', 'ITEM_CLASS_NAME'];
    }
    return param;
  }
}
